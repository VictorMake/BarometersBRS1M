using BaseForm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarometersBRS1M
{
    /// <summary>
    /// Реализация пользовательского расчётного класса
    /// </summary>
    /// <remarks></remarks>
    public class ClassCalculation : IClassCalculation
    {
        private ProjectManager mProjectManager;
        public BaseForm.ProjectManager Manager
        {
            get { return mProjectManager; }
            set { mProjectManager = value; }
        }

        /// <summary>
        /// Входные аргументы
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public InputParameter InputParam
        {
            get { return inParam; }
            set { inParam = value; }
        }

        /// <summary>
        /// Настроечные параметры
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public TuningParameter TuningParam
        {
            get { return tunParam; }
            set { tunParam = value; }
        }

        /// <summary>
        ///  Расчетные параметры
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public CalculatedParameter CalculatedParam
        {
            get { return calcParam; }
            set { calcParam = value; }
        }

        private InputParameter inParam; // входные аргументы
        private TuningParameter tunParam;// настроечные параметры
        private CalculatedParameter calcParam;// расчетные параметры

        // событие для выдачи ошибки в вызывающую программу
        //Delegate Sub DataErrorventHandler(ByVal sender As Object, ByVal e As DataErrorEventArgs)
        //Public Event DataError(ByVal sender As BaseForm.IClassCalculation, ByVal e As DataErrorEventArgs)
        public event EventHandler<DataErrorEventArgs> DataError;

        //public ClassCalculation(ProjectManager proManager) : this(proManager, null) { }

        /// <summary>
        /// Контроллеры, представляющие результат замера с модели ComPort
        /// </summary>
        public Dictionary<string, CommPresenter> refPresenters;

        public ClassCalculation(ProjectManager proManager, Dictionary<string, CommPresenter> inPresenters) : base()
        {
            this.Manager = proManager;
            inParam = new InputParameter();
            tunParam = new TuningParameter();
            calcParam = new CalculatedParameter();
            refPresenters = inPresenters;
        }

        /// <summary>
        /// Последовательное прохождение по этапам приведениия и вычисления.
        /// Здесь индивидуальные настройки для класса.
        /// </summary>
        /// <remarks></remarks>
        public void Calculate()
        {
            // Для Приведенных и Пересчитанных параметров входные единицы измерения
            // только в единицах СИ, выходные единицы измерения - любого типа.
            try
            {
                // *** Для расчёта барометров не используется ***
                //// мне здесь пока не надо получать от контролов
                //if (mProjectManager.NeedToRewrite)
                //    ПолучитьЗначенияНастроечныхПараметров();
                //***********************************************************

                // переводим в Си только измеренные пареметры
                //mProjectManager.ПереводВЕдиницыСИИзмеренныеПараметры();
                // получение абсолютных давлений
                //mProjectManager.УчетБазовыхВеличин();
                // весь подсчет производится исключительно в единицах СИ
                // извлекаем значения измеренных параметров
                //ИзвлечьЗначенияИзмеренныхПараметров();
                //***********************************************************

                ВычислитьРасчетныеПараметры();
                mProjectManager.СonversionToTuningUnitCalculationParameters();
            }
            catch (Exception ex)
            {
                // ошибка проглатывается
                //description = "Процедура: Подсчет"
                //' перенаправление встроенной ошибки
                //Dim fireDataErrorEventArgs As New DataErrorEventArgs(ex.Message, description)
                //RaiseEvent DataError(Me, fireDataErrorEventArgs) ' не ловит в конструкторе
                //'MessageBox.Show(fireDataErrorEventArgs, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            }
        }

        string description = $"Процедура: {nameof(ПолучитьЗначенияНастроечныхПараметров)}";

        /// <summary>
        /// Получить значения параметров, используемых как настраиваемые глобальные переменные.
        /// </summary>
        /// <remarks></remarks>
        public void ПолучитьЗначенияНастроечныхПараметров()
        {
            if (Manager.TuningDataTable == null)
                return;

            bool success = false;

            // Вначале проверяется наличие расчетных параметров в базе
            //arrНастроечныеПараметры
            foreach (string имяНастроечногоПараметра in tunParam.TuningDictionary.Keys.ToArray())
            {
                success = false;

                foreach (BaseFormDataSet.НастроечныеПараметрыRow rowНастроечныйПараметр in Manager.TuningDataTable.Rows)
                {
                    if (rowНастроечныйПараметр.ИмяПараметра == имяНастроечногоПараметра)
                    {
                        success = true;
                        break;
                    }
                }

                if (success == false)
                {
                    // перенаправление встроенной ошибки
                    DataError?.Invoke(this, new DataErrorEventArgs(string.Format("Настроечный параметр {0} в базе параметров не найден!", имяНастроечногоПараметра), description));
                    // не ловит в конструкторе
                    //MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    return;
                }
            }

            // проверить наличие в расчетном модуле переменных, соответствующих расчетным настроечным
            // и присвоение им значений
            success = true;
            try
            {
                foreach (BaseFormDataSet.НастроечныеПараметрыRow rowНастроечныйПараметр in Manager.TuningDataTable.Rows)
                {
                    if (tunParam.TuningDictionary.Keys.Contains(rowНастроечныйПараметр.ИмяПараметра))
                    {
                        // настроить какието внутренние переменные
                        switch (rowНастроечныйПараметр.ИмяПараметра)
                        {
                            //case TuningParameter.conA:
                            //    break;

                            // так не делать т.к. тоже самое в цикле далее: tuningParam.A.ЦифровоеЗначение = rowНастроечныйПараметр.ЦифровоеЗначение
                            // так можно: Dim Temp As Double = rowНастроечныйПараметр.ЦифровоеЗначение

                            //Case "GвМПитоПриводить"
                            //    'GвМПитоПриводить = rowНастроечныйПараметр.ЦифровоеЗначение
                            //    'n1ГПриводить = Convert.ToInt32(rowНастроечныйПараметр.ЛогическоеЗначение)
                            //    GвМПитоПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                            //    Exit Select
                            //Case "GвМПолеДавленийПриводить"
                            //    GвМПолеДавленийПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                            //    Exit Select
                            //Case "n1ГПриводить"
                            //    n1ГПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                            //    Exit Select
                            //Case "nИГ-03ГПриводить"
                            //    nИГ_03ГПриводить = rowНастроечныйПараметр.ЛогическоеЗначение

                            default:
                                break;
                        }
                    }
                    else
                    {
                        success = false;
                        // перенаправление встроенной ошибки
                        DataError?.Invoke(this, new DataErrorEventArgs(string.Format("Настроечный параметр {0} не имеет соответствующей переменной в модуле расчета!", rowНастроечныйПараметр.ИмяПараметра), description));
                        //не ловит в конструкторе
                        //MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    }
                }

                if (success == false)
                    return;

                // занести значения настроечных параметров
                var _with1 = Manager.TuningDataTable;
                foreach (string keysTuning in tunParam.TuningDictionary.Keys.ToArray())
                {
                    if (_with1.FindByИмяПараметра(keysTuning).ЛогикаИлиЧисло)
                    {
                        tunParam.TuningDictionary[keysTuning].ЛогикаИлиЧисло = true;
                        tunParam.TuningDictionary[keysTuning].ЛогическоеЗначение = _with1.FindByИмяПараметра(keysTuning).ЛогическоеЗначение;
                    }
                    else
                    {
                        tunParam.TuningDictionary[keysTuning].ЛогикаИлиЧисло = false;
                        tunParam.TuningDictionary[keysTuning].ЦифровоеЗначение = _with1.FindByИмяПараметра(keysTuning).ЦифровоеЗначение;
                    }
                }
            }
            catch (Exception ex)
            {
                // перенаправление встроенной ошибки
                DataError?.Invoke(this, new DataErrorEventArgs(ex.Message, description));
                // не ловит в конструкторе
                //MessageBox.Show(fireDataErrorEventArgs, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            }
        }

        /// <summary>
        /// Поиск всех параметров по пользовательскому запросу в DataSet.MeasurementDataTable
        /// (с одним входным параметром являющимся именем связи для реального измеряемого канала Сервера).
        /// </summary>
        /// <remarks></remarks>
        private void ИзвлечьЗначенияИзмеренныхПараметров()
        {
            try
            {
                var _with2 = mProjectManager.MeasurementDataTable;
                //'расчетные параметры
                //Tбокса = .FindByИмяПараметра(conTбокса).ЗначениеВСИ '	температура в боксе
                //Барометр = .FindByИмяПараметра(conБарометр).ЗначениеВСИ '	БРС1-М
                //'учет атмосферного давления - относительного давления воздуха
                //B = Барометр / кон735_6
                //ДавлениеВоздухаНаВходе = .FindByИмяПараметра(conДавлениеВоздухаНаВходе).ЗначениеВСИ + B

                // вместо последовательного извлечения применяется обход по коллекции
                // inputArg.ARG1 = .FindByИмяПараметра(InputArgument.conARG1).ЗначениеВСИ
                // ...
                // inputArg.ARG10 = .FindByИмяПараметра(InputArgument.conARG10).ЗначениеВСИ
                //inputArg.Барометр = .FindByИмяПараметра("Барометр").ЗначениеВСИ
                //inputArg.InputArgDictionary("Барометр") = 100

                foreach (string keysArg in inParam.InputArgDictionary.Keys.ToArray())
                {
                    double Si = _with2.FindByИмяПараметра(keysArg).ЗначениеВСИ;
                    inParam.InputArgDictionary[keysArg] = _with2.FindByИмяПараметра(keysArg).ЗначениеВСИ;
                }

                //' иттератор по коллекции как KeyValuePair objects.
                //For Each kvp As KeyValuePair(Of String, Double) In inputArg.InputArgDictionary
                //    'Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value)
                //    inputArg.InputArgDictionary(kvp.Key) = .FindByИмяПараметра(kvp.Key).ЗначениеВСИ
                //Next

                //For Each value As Double In inputArg.InputArgDictionary.Values
                //    Console.WriteLine("Value = {0}", value)
                //Next
            }
            catch (Exception ex)
            {
                //перенаправление встроенной ошибки
                DataError?.Invoke(this, new DataErrorEventArgs(ex.Message, $"Процедура: {nameof(ИзвлечьЗначенияИзмеренныхПараметров)}"));
            }
        }

        /// <summary>
        /// Поиск всех параметров по пользовательскому запросу в DataSet.РасчетныеПараметры
        /// (с одним входным параметром являющимся именем расчётной величины).
        /// </summary>
        /// <remarks></remarks>
        private void ВычислитьРасчетныеПараметры()
        {
            // Иммитатор()' отладка
            try
            {
                ВычислениеОсновных();

                // занести вычисленные значения
                // вместо последовательного извлечения применяется обход по коллекции
                // .CalculatedDataTable.FindByИмяПараметра(conCalc1).ВычисленноеЗначениеВСИ = Calc1
                // ...
                // .CalculatedDataTable.FindByИмяПараметра(conCalc10).ВычисленноеЗначениеВСИ = Calc10

                foreach (string keysCalc in calcParam.CalcDictionary.Keys.ToArray())
                    mProjectManager.CalculatedDataTable.FindByИмяПараметра(keysCalc).ВычисленноеЗначениеВСИ = calcParam.CalcDictionary[keysCalc];

                // расчетные вспомогательные ...
            }
            catch (Exception ex)
            {
                // перенаправление встроенной ошибки
                DataError?.Invoke(this, new DataErrorEventArgs(ex.Message, $"Процедура: {nameof(ВычислитьРасчетныеПараметры)}"));
            }
        }

        /// <summary>
        /// Основные вычисления, накопления, запись в базу для протокола.
        /// Индивидуальные для наследуемого класса.
        /// </summary>
        /// <remarks></remarks>
        public void ВычислениеОсновных()
        {
            foreach (CommPresenter itemCommPresenter in refPresenters.Values)
                calcParam.CalcDictionary[itemCommPresenter.GetCommPortModel.ParameterName] = itemCommPresenter.GetCommPortModel.PressureOutput;

            // входные и вычисляемые не равны, поэтому ни каких циклов по коллекциям не примняется.
            // поиск только по ключам.
            // тест:
            //calcParam.CalcDictionary("Calc1") = inputArg.InputArgDictionary("ARG1")
            //calcParam.CalcDictionary("Calc2") = inputArg.InputArgDictionary("ARG2")

            ////var _with4 = inParam;
            ////Gвпр = 30.31 * (1.5774 * ((6 * (1 - ((Барометр / 735.6 + (РстатРМК1 + РстатРМК2) / 2) / (Барометр / 735.6 + (РполнРМК1 + РполнРМК2) / 2))) ^ 0.2857)) ^ 0.5) * (1 - 0.1666 * 6 * (1 - ((Барометр / 735.6 + (РстатРМК1 + РстатРМК2) / 2) / (Барометр / 735.6 + (РполнРМК1 + РполнРМК2) / 2)) ^ 0.2857)) ^ 2.5
            //calcParam.Gвпр = 999.0;// tunParam.A.ЦифровоеЗначение * (1.5774 * Math.Pow((6 * (1 - Math.Pow(((_with4.Барометр / 735.6 + (_with4.РстатРМК1 + _with4.РстатРМК2) / 2) / (_with4.Барометр / 735.6 + (_with4.РполнРМК1 + _with4.РполнРМК2) / 2)), 0.2857))), 0.5)) * (Math.Pow((1 - 0.1666 * 6 * (1 - Math.Pow(((_with4.Барометр / 735.6 + (_with4.РстатРМК1 + _with4.РстатРМК2) / 2) / (_with4.Барометр / 735.6 + (_with4.РполнРМК1 + _with4.РполнРМК2) / 2)), 0.2857))), 2.5));

            ////PiK = ((Барометр / 735.6) + (0.99 * (РизмНаВходе1 + РизмНаВходе2) / 2)) / (Барометр / 735.6 + РизмПолнЗаКомпр1)
            //calcParam.PiK = 555.0;// (0.99 * (_with4.РизмПолнЗаКомпр1 + _with4.РизмПолнЗаКомпр2) / 2 + _with4.Барометр / 735.6) / ((_with4.РизмНаВходе1 + _with4.РизмНаВходе2) / 2 + _with4.Барометр / 735.6);
        }

        /// <summary>
        /// Заполнить коллекцию словаря расчётных параметров в соответствии 
        /// с таблицей расчётных параметров базы данных.
        /// </summary>
        /// <remarks></remarks>
        public void ЗаполнитьСловатьРасчётныеПараметры()
        {
            if (mProjectManager.CalculatedDataTable == null)
                return;

            foreach (BaseFormDataSet.РасчетныеПараметрыRow rowРасчетныйПараметр in mProjectManager.CalculatedDataTable.Rows)
                calcParam.CalcDictionary.Add(rowРасчетныйПараметр.ИмяПараметра, 0.0);
        }
    }
}