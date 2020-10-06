using System;
using System.IO;
using System.Windows.Forms;
using System.Resources;
using BaseForm;
using static BarometersBRS1M.GlobalVariable;

/*
' 1. имя проекта "BarometersBRS1M" класса DLL, а имя визуально наследуемой формы везде есть FrmMain
' 2. assembly name  и root namespace - BarometersBRS1M на странице свойств проекта также совпадает
' 3. FrmMain.Tag = BarometersBRS1M должен иметь имя проекта

'--- для теста из TestBaseInherit.vbproj там настоить в основной форме --------
' 1.        Dim ClassName As String = System.Configuration.ConfigurationManager.AppSettings("DiagramClassName")
' 2.        КлассГрафик.Manager.PathSettingMdb = "D:\Project\RegistrationTCPFrameWork46\RegistrationTCP\bin\x86\Debug\Store\МодулиРасчета\BarometersBRS1M.mdb"

' 3. в app.config изменить следующие ключи для запуска именно тестируемую DLL
' 4. <add key="CalculationAssemblyFilename" value="D:\Project\RegistrationTCPFrameWork46\RegistrationTCP\bin\x86\Debug\Store\МодулиРасчета\BarometersBRS1M.dll"/>
' 5. <add key="DiagramClassName" value="BarometersBRS1M.FrmMain"/>

' 6. в папке МодулиРасчета создать базу "ИмяПроекта.mdb" и настроить настроечные, измеренные. расчётные каналы (устаноывить единицы измерения в нет)
' 7. создать ИмяПроекта.xml и в нём описать назначение модуля
' 8. после компиляции положить DLL в папку МодулиРасчета, подключить каналы в конфигураторе и проверить работу на константах
 */

/*
 * Для удаления и добавления портов нужно Форму Регистротор закрыть
 * При закрытии Регистратора выгружаются все подключенные расчетные модули
 * Должно быть 2 режима записи конфигурации:
 * 1) Json и база данных - когда форма Регистратора не загружена
 * кнопки дабавить и удалить порты доступны
 * 2) Json - когда форма Регистратора загружена и расчётный модуль показывается вместе с ней
 * кнопки дабавить и удалить порты НЕ доступны
 * 
1. Расчётный модуль загружается, считывает конфигурацию и подключает порты на опрос
2. Каналы в конфигурационной базе уже записаны с прошлой конфигурации
3. При запуске Регистратора производится добавление каналов в опрашиваемые каналы (погрешность 10000)
    (подключает порты на опрос - возможен сбой при частом перезапуске) - опрос портов уже должен быть запущен
4. При остановке Регистратора появляется возможность визуализировать и настроить форму расчётного модуля
    Пользователю запрещено удалять или добавлять Порты
5. При запуске Регистратора производится скрытие формы - в этом событии нужно записать конфигурацию в Json, 
    запустить все не запущенные Порты.
    ************************************
6. Для удаления и добавления портов нужно Форму Регистротор закрыть
7. Модифицировать Порты (по доступным кнопкам добавления и удаления) и произвести запись
6. Регистратор заново загружает расчётный модуль и подключает каналы
    (и подключает порты на опрос - возможен сбой при частом перезапуске) - опрос портов уже должен быть запущен
*/

[assembly: NeutralResourcesLanguage("ru-RU")]
[assembly: CLSCompliant(true)]
namespace BarometersBRS1M
{
    public partial class FrmMain : FrmBase
    {
        private ClassCalculation myClassCalculation;// локальная переменная для свойства ClassCalculation

        /// <summary>
        /// Ссылка на реализацию пользовательского расчётного класса
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public override IClassCalculation ClassCalculation { get; set; }

        private string mOwnCatalogue;// = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), this.Name);
        /// <summary>
        /// Рабочий каталог для модуля расчета
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public override string OwnCatalogue
        {
            get { return mOwnCatalogue; }
            set
            {
                mOwnCatalogue = value;
                if (!Directory.Exists(mOwnCatalogue))
                { MessageBox.Show(this, string.Format("Рабочий каталог {0} для модуля расчета отсутствует. Необходимо его скопировать.", OwnCatalogue), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private bool mIsDLLVisible = false;
        /// <summary>
        /// Видима DLL или нет, т.е. имеет ли другие окна или она только вычисляет
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public override bool IsDllVisible
        { get { return mIsDLLVisible; } }

        private FormComBarometers varFormComBarometers;

        public override void GetValueTuningParameters()
        {
            // *** Для расчёта барометров не используется ***
            //if (myClassCalculation != null)
            //    myClassCalculation.ПолучитьЗначенияНастроечныхПараметров();
            //***********************************************************
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            mOwnCatalogue = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""), (string)this.Tag);

            // выполняется после MyBase_Load
            mIsDLLVisible = false;
            // это свойство определяет поведение расчетного модуля
            //If mIsDLLVisible Then TestLoadChaildForm()

            OwnCatalogue = Path.Combine(base.Manager.PathCatalog, (string)this.Tag);
            gПутьРесурсы = OwnCatalogue;

            base.FrmBaseLoad();
            varFormComBarometers = new FormComBarometers(Manager) { MdiParent = this };
            varFormComBarometers.Show();

            Manager.LoadConfiguration();// из XML
            // идет вслед за Me.Manager.СчитатьНастройки()
            myClassCalculation = new ClassCalculation(Manager, varFormComBarometers.Presenters);
            ClassCalculation = myClassCalculation;
            Manager.FillCombo();
            // если какое-то сообщение будет до загрузки сеток, то перерисовка их будет вызывать исключения, поэтому
            // myClassCalculation.ПолучитьЗначенияНастроечныхПараметров() идет после Me.Manager.FillCombo()

            // *** Для расчёта барометров не используется ***
            //myClassCalculation.ПолучитьЗначенияНастроечныхПараметров();
            //***********************************************************

            myClassCalculation.ЗаполнитьСловатьРасчётныеПараметры();
            //If Not varIsDLLVisible Then Me.Hide()
            myClassCalculation.DataError += MyClassCalculation_DataError;
        }

        /// <summary>
        /// Переадресация включения доступности кнопок к дочерней форме
        /// </summary>
        public void EnableNewDeleteButton()
        { varFormComBarometers.EnableNewDeleteButton(); }

        private void MyClassCalculation_DataError(object sender, DataErrorEventArgs e)
        {
            // sender.Manager.ПроверкаСоответствияПройдена - узнать что-то
            // если произошла ошибка в классе или ошибка была специально сгенерирована то вывести сообщение
            string TITLE = "Подсчет для модуля " + Text;
            MessageBox.Show($"Ошибка в подсчете ClassCalculation.dll:{ Environment.NewLine}{e.Message}{Environment.NewLine}{e.Description}",
                            TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
