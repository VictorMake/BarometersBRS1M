using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using Newtonsoft.Json;
using BarometersBRS1M.Common;
using BarometersBRS1M.SettingBarometers;
using BarometersBRS1M.ViewModel;
using static BarometersBRS1M.GlobalUtility;
using static BaseForm.BaseFormDataSet;

namespace BarometersBRS1M
{
    public partial class FormComBarometers : Form
    {
        /// <summary>
        /// Контроллеры, представляющие результат замера с модели ComPort
        /// </summary>
        public Dictionary<string, CommPresenter> Presenters = new Dictionary<string, CommPresenter>();

        private BaseForm.ProjectManager BaseFormProjectManager { get; set; }
        private ManagerComPorts managerPorts;
        // для хранения настроек, а по существу другая реализация MVVC
        private IPartContext context;
        private MainViewModel mMainViewModel; // модель данных настроек
        private const string USER_CONTROL_BAROMETER = "Barometer"; // для создания имени контрола по умолчанию
        private int allControlsCount; // счётчик добавленных в панель контроллов барометра
        private CustomComboBox ComboBoxControlsToDelete;
        private string partBarometersSetting;

        public FormComBarometers(BaseForm.ProjectManager inBaseFormProjectManager)
        {
            InitializeComponent();
            BaseFormProjectManager = inBaseFormProjectManager;
        }

        private void FormComBarometers_Load(object sender, EventArgs e)
        {
            partBarometersSetting = Path.Combine(GlobalVariable.gПутьРесурсы, "PartParametersString.json");
            ComboBoxControlsToDelete = new CustomComboBox();
            ToolStripInstrument.Items.Add(ComboBoxControlsToDelete);
            ComboBoxControlsToDelete.Enabled = false;

            managerPorts = new ManagerComPorts(this);
            InitializeDataSetting();
            InitializePanelBarometers();
            RunAllComPort();
        }

        private void FormComBarometers_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBarometersSetting();// Вначале записать
            CloseCommPortModelAllPresenters();
        }

        /// <summary>
        /// Создать на основе MVVC структуру для хранения насроечных параметров,
        /// при создании производится считывание ранее произведенных настроек.
        /// </summary>
        private void InitializeDataSetting()
        {
            if (!CheckFile(partBarometersSetting)) return;

            Generation generation = new Generation();
            context = generation.BuildFakeSession(partBarometersSetting);

            IPartRepository partRepository = new PartRepository(context);
            IUnitOfWork unit = new UnitOfWork(context);
            mMainViewModel = new MainViewModel(unit, partRepository);
        }

        /// <summary>
        /// Первоначальное заполнение BarometerControlView в панель производится 
        /// в соответствии с сохранённым ранее списком.
        /// </summary>
        /// <remarks></remarks>
        private void InitializePanelBarometers()
        {
            tlpBarometers.Controls.Clear();

            foreach (DeviceBRS1MViewModel dev in mMainViewModel.PartWorkspace.CurrentPart.DeviceDetaills)
                AddNewBarometerControlView(dev);

            EndInitializeForm();
        }

        /// <summary>
        /// Завершить инициализацию при загрузке.
        /// </summary>
        private void EndInitializeForm()
        {
            //PartViewModel pvm = mMainViewModel.PartWorkspace.CurrentPart;
            //BindingSource devicesBindingSource = new BindingSource(this.components) { DataSource = pvm.DeviceDetaills };
            //ComboBoxControlsToDelete.DataSource = devicesBindingSource;
            //ComboBoxControlsToDelete.DisplayMember = "ParameterName";

            //ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.ValueMember = "Name";
            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.DisplayMember = "Name";//"Tag";
            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.SelectedIndexChanged += ComboBoxControlsToDelete_SelectedIndexChanged;

            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Text = string.Empty;
            tslDeleteBarometer.Enabled = false;
            foreach (UserControl _userControl in tlpBarometers.Controls)
                _userControl.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// Добавить новый контрол по заданному индексу на панель.
        /// Перегруженный.
        /// </summary>
        private void AddNewBarometerControlView()
        {
            mMainViewModel.PartWorkspace.CurrentPart.AddDeviceDetailCommand.Execute(null);

            DeviceBRS1MViewModel curBRS1M_VM = mMainViewModel.PartWorkspace.CurrentPart.CurrentDeviceDetail as DeviceBRS1MViewModel;
            // значения по умолчанию
            curBRS1M_VM.PortName = "COM1";
            curBRS1M_VM.BaudRate = 1200;
            curBRS1M_VM.Parity = Parity.None;
            curBRS1M_VM.StopBits = StopBits.One;
            curBRS1M_VM.DataBits = 8;
            curBRS1M_VM.ParameterName = USER_TEXT_CONTROL_BAROMETER;
            curBRS1M_VM.ClearOnOpen = false;
            curBRS1M_VM.ClearWithDTR = false;
            curBRS1M_VM.CurrentDataMode = DataMode.Hex;
            curBRS1M_VM.UnitInput = UnitOfMeasureInput.гПаВх;
            curBRS1M_VM.UnitOutput = UnitOfMeasureOutput.гПаВых;

            AddNewBarometerControlView(curBRS1M_VM);
        }

        /// <summary>
        /// Добавить новый контрол по заданному индексу на панель.
        /// Перегруженный.
        /// </summary>
        /// <param name="indexRow"></param>
        private void AddNewBarometerControlView(DeviceBRS1MViewModel deviceBRS1M)
        {
            ClosePortAllPresenters();
            tslDeleteBarometer.Enabled = false;
            allControlsCount += 1;

            BarometerControlView mBarometerControlView = new BarometerControlView
            {
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Fill,
                TabIndex = tlpBarometers.RowCount,
                Name = USER_CONTROL_BAROMETER + allControlsCount,
                Tag = (string)deviceBRS1M.ParameterName
            };

            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Items.Add(mBarometerControlView);
            AddBarometerControlToPanel(mBarometerControlView);
            HighlightingSelectedControl(mBarometerControlView);
            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Text = string.Empty;

            CreateNewCommPresenter(mBarometerControlView, deviceBRS1M);
        }

        /// <summary>
        /// Расположить контрол на пенели.
        /// </summary>
        /// <param name="inBarometerControlView"></param>
        /// <param name="indexRow"></param>
        private void AddBarometerControlToPanel(BarometerControlView inBarometerControlView)
        {
            tlpBarometers.SuspendLayout();
            tlpBarometers.RowCount = tlpBarometers.RowCount + 1;
            tlpBarometers.RowStyles.Add(new RowStyle(SizeType.AutoSize, inBarometerControlView.Height));
            tlpBarometers.Controls.Add(inBarometerControlView, 0, tlpBarometers.RowCount);// последний добавляемый должен быть всегда внизу
            tlpBarometers.ResumeLayout(false);

            tlpBarometers.Height = inBarometerControlView.Height * tlpBarometers.Controls.Count;
            tlpBarometers.ScrollControlIntoView(inBarometerControlView);
            tlpBarometers.Refresh();
        }

        /// <summary>
        /// Создать экземпляр Presenter и добавить в словарь.
        /// </summary>
        private void CreateNewCommPresenter(BarometerControlView inBarometerControlView, DeviceBRS1MViewModel deviceBRS1M)
        {
            // После того, как интерфейс готов, надо придумать как подпихнуть экземпляр интерфейса в Presenter. 
            // В данном случае, для простой задачи, можно сделать это через конструктор, 
            // в более сложных ситуациях Presenter может получать ссылку на конкретный экземпляр через специальную фабрику представлений 
            // или даже сам являться фабрикой, порождающий необходимые Представления в зависимости от ситуации. 
            CommPresenter oneCommPresenter = new CommPresenter(inBarometerControlView, managerPorts, deviceBRS1M);
            Presenters.Add(inBarometerControlView.Name, oneCommPresenter);
        }

        /// <summary>
        /// Удалить контрол по заданному индексу из панели.
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void RemoveAtBarometerControlView(int selectedIndex)
        {
            ClosePortAllPresenters();
            BarometerControlView mBarometerControlView = (BarometerControlView)ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Items[selectedIndex];
            Presenters.Remove(mBarometerControlView.Name);
            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Items.Remove(mBarometerControlView);
            // чтобы не удалять из панели конкретный элемент, так кака может сбиться соответствие индексов 
            // между ComboBoxControlsToDelete и tlpBarometers, произвести заполнение панели с нуля
            tlpBarometers.Controls.Clear();
            tlpBarometers.RowCount = 0;
            tlpBarometers.RowStyles.Clear();
            tlpBarometers.AutoScroll = false;
            tlpBarometers.Height = 100;

            for (int J = 0; J < mMainViewModel.PartWorkspace.CurrentPart.DeviceDetaills.Count; J++)
                AddBarometerControlToPanel((BarometerControlView)ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Items[J]);

            tlpBarometers.AutoScroll = true;
        }

        /// <summary>
        /// Подсветить выделенный в ComboBoxControlsToDelete контрол на панели.
        /// Сам ComboBoxControlsToDelete заполнен.
        /// </summary>
        /// <param name="inBarometerControlView"></param>
        private void HighlightingSelectedControl(BarometerControlView inBarometerControlView)
        {
            foreach (UserControl _userControl in tlpBarometers.Controls)
                _userControl.BackColor = SystemColors.Control;

            inBarometerControlView.BackColor = Color.LightBlue;

            if (ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.SelectedIndex != -1)
            {
                tslDeleteBarometer.Enabled = true;
                tlpBarometers.ScrollControlIntoView(inBarometerControlView);
            }
            else
            {
                tslDeleteBarometer.Enabled = false;
            }
        }

        /// <summary>
        /// Удалить контрол параметра в соотоветствии с выделенным в ComboBoxControlsToDelete.
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void DeleteBarometerControlView(int selectedIndex)
        {
            PartViewModel pvm = mMainViewModel.PartWorkspace.CurrentPart;
            pvm.CurrentDeviceDetail = pvm.DeviceDetaills[selectedIndex];

            if (pvm.DeleteDeviceDetailCommand.CanExecute(null))
            {
                pvm.DeleteDeviceDetailCommand.Execute(null);
                RemoveAtBarometerControlView(selectedIndex);
            }

            tslDeleteBarometer.Enabled = false;
            ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.Text = string.Empty;
        }

        /// <summary>
        /// Сохранить изменения во всех параметрах
        /// </summary>
        private void SaveBarometersSetting()
        {
            int i = 0;
            //foreach (ParameterDetailViewModel item in mMainViewModel.PartWorkspace.CurrentPart.ParameterDetails)
            //{
            //    ((TuningParameterViewModel)item).NameParameter = "Ерунда" + i;
            //    i++;
            //}

            foreach (CommPresenter itemCommPresenter in Presenters.Values)
            {
                DeviceBRS1MViewModel deviceBRS1M = (DeviceBRS1MViewModel)mMainViewModel.PartWorkspace.CurrentPart.DeviceDetaills[i];
                deviceBRS1M.ParameterName = (itemCommPresenter.GetCommPortModel.ParameterName == string.Empty) ? Guid.NewGuid().ToString() : itemCommPresenter.GetCommPortModel.ParameterName;
                deviceBRS1M.PortName = itemCommPresenter.GetCommPortModel.PortName;
                deviceBRS1M.Parity = itemCommPresenter.GetCommPortModel.Parity;
                deviceBRS1M.StopBits = itemCommPresenter.GetCommPortModel.StopBits;
                deviceBRS1M.DataBits = itemCommPresenter.GetCommPortModel.DataBits;
                deviceBRS1M.BaudRate = itemCommPresenter.GetCommPortModel.BaudRate;
                deviceBRS1M.ClearOnOpen = itemCommPresenter.GetCommPortModel.ClearOnOpen;
                deviceBRS1M.ClearWithDTR = itemCommPresenter.GetCommPortModel.ClearWithDTR;
                deviceBRS1M.CurrentDataMode = itemCommPresenter.GetCommPortModel.CurrentDataMode;
                deviceBRS1M.UnitInput = itemCommPresenter.GetCommPortModel.UnitInput;
                deviceBRS1M.UnitOutput = itemCommPresenter.GetCommPortModel.UnitOutput;

                i++;
            }

            mMainViewModel.SaveCommand.Execute(null);
            SaveDataSetTuningParameters();
        }

        /// <summary>
        /// Сохранить расчётные каналы в соответствии с именами параметров всех устройств
        /// сконфигурированных ComPort.
        /// </summary>
        private void SaveDataSetTuningParameters()
        {
            // не работают в сязанной таблице:
            //Manager.CalculatedDataTable.Clear();
            //Manager.CalculatedDataTable.DataSet.Tables["РасчетныеПараметры"].Rows.Clear();
            //foreach (DataRow rowDelete in Manager.CalculatedDataTable.DataSet.Tables["РасчетныеПараметры"].Rows)
            foreach (РасчетныеПараметрыRow rowDelete in BaseFormProjectManager.CalculatedDataTable.Rows)
                rowDelete.Delete();

            int i = 0;

            string ИмяПараметра;
            string ОписаниеПараметра;
            double ВычисленноеЗначениеВСИ = 0.0;
            double ВычисленноеПереведенноеЗначение = 0.0;
            string РазмерностьСИ = "нет";
            string РазмерностьВыходная = "нет";
            double НакопленноеЗначение = 0.0;
            short ИндексКаналаИзмерения = 0;
            double ДопускМинимум = 0.0;
            double ДопускМаксимум = 1100.0;
            double РазносУмин;
            double РазносУмакс;
            double АварийноеЗначениеМин = 0.0;
            double АварийноеЗначениеМакс = 0.0;
            bool Видимость = true;
            bool ВидимостьРегистратор = true;

            // в цикле по коллекции добавить параметры
            foreach (CommPresenter itemCommPresenter in Presenters.Values)
            {
                // Не допустить пустые и повторяющиеся имена
                ИмяПараметра = (itemCommPresenter.GetCommPortModel.ParameterName == string.Empty || BaseFormProjectManager.CalculatedDataTable.FindByИмяПараметра(itemCommPresenter.GetCommPortModel.ParameterName) != null) ?
                    Guid.NewGuid().ToString() : itemCommPresenter.GetCommPortModel.ParameterName;

                ОписаниеПараметра = itemCommPresenter.GetCommPortModel.PortName;
                РазносУмин = i * 5.0;
                РазносУмакс = РазносУмин + 50.0;

                BaseFormProjectManager.CalculatedDataTable.AddРасчетныеПараметрыRow(
                    ИмяПараметра,
                    ОписаниеПараметра,
                    ВычисленноеЗначениеВСИ,
                    ВычисленноеПереведенноеЗначение,
                    РазмерностьСИ,
                    РазмерностьВыходная,
                    НакопленноеЗначение,
                    ИндексКаналаИзмерения,
                    ДопускМинимум,
                    ДопускМаксимум,
                    РазносУмин,
                    РазносУмакс,
                    АварийноеЗначениеМин,
                    АварийноеЗначениеМакс,
                    Видимость,
                    ВидимостьРегистратор);

                ИндексКаналаИзмерения++;
                i++;
            }

            //_with1.FindByИмяПараметра(TuningParameter.НОМЕР_ГРЕБЕНКИ_А).ЦифровоеЗначение = double.Parse(txtНомерГребенки_А.Text);

            //_with1.FindByИмяПараметра(TuningParameter.ШИРИНА_МЕРНОГО_УЧАСТКА).ЦифровоеЗначение = double.Parse(txtШиринаМерногоУчастка.Text);
            BaseFormProjectManager.SaveTable();
        }

        /// <summary>
        /// Через презентера закрыть все порты моделей
        /// </summary>
        private void ClosePortAllPresenters()
        {
            foreach (CommPresenter itemPresenter in Presenters.Values)
                itemPresenter.CloseComPort();
        }

        /// <summary>
        /// Через закрытие презентера закрыть все порты
        /// </summary>
        private void CloseCommPortModelAllPresenters()
        {
            foreach (CommPresenter itemPresenter in Presenters.Values)
                itemPresenter.CloseCommPortModel();
        }

        /// <summary>
        /// Включение доступности кнопок.
        /// </summary>
        public void EnableNewDeleteButton()
        {
            tslAddBarometer.Enabled = true;
            tslDeleteBarometer.Enabled = true;
            ComboBoxControlsToDelete.Enabled = true;
        }

        private struct RepetitionComPort
        {
            public string PortName, ParameterName;

            public RepetitionComPort(string inPortName, string inParameterName)
            {
                PortName = inPortName;
                ParameterName = inParameterName;
            }
        }

        /// <summary>
        /// Проверить на отсутствие совпадения номеров портов
        /// </summary>
        private void CheckCorrectNumberComPort()
        {
            List<string> listAllComPort = new List<string>();
            List<RepetitionComPort> listRepetitionComPort = new List<RepetitionComPort>();

            foreach (CommPresenter itemPresenter in Presenters.Values)
            {
                string portName = itemPresenter.GetCommPortModel.PortName;

                if (listAllComPort.Contains(portName))
                    listRepetitionComPort.Add(new RepetitionComPort(portName, itemPresenter.GetCommPortModel.ParameterName));

                listAllComPort.Add(portName);
            }

            if (listRepetitionComPort.Count > 0)
            {
                string msg = "\nСледующие порты повторяются в конфигурации:\n";

                foreach (var itemPort in listRepetitionComPort)
                    msg += string.Format("{0} в приборе: {1}\n", itemPort.PortName, itemPort.ParameterName);

                msg += "Настройте правильно приборы или удалите лишние!\n";
                Log(LogMsgType.Error, msg);
            }
            else
            {
                Log(LogMsgType.Normal, "\n\nКонфигурация по номерам портов корректна.\n");
            }
        }

        /// <summary>
        /// Запустить опрос ещё не запущенных Com Port
        /// </summary>
        private void RunAllComPort()
        {
            CheckCorrectNumberComPort();
            foreach (CommPresenter itemPresenter in Presenters.Values)
                if (!itemPresenter.GetCommPortModel.IsOpen) { itemPresenter.ViewTerminal_OpenPortEvent(null, null); }
        }

        #region Обработчики событий
        private void ComboBoxControlsToDelete_SelectedIndexChanged(object sender, EventArgs e)
        { HighlightingSelectedControl((BarometerControlView)((ComboBox)sender).SelectedItem); }

        private void tslAddAddBarometer_Click(object sender, EventArgs e)
        { AddNewBarometerControlView(); }

        private void tslDeleteBarometer_Click(object sender, EventArgs e)
        {
            if (ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.SelectedIndex != -1)
                DeleteBarometerControlView(ComboBoxControlsToDelete.ComboBoxToDelete.ComboBoxControlsToDelete.SelectedIndex);
        }

        private void btnSerializeCollection_Click(object sender, EventArgs e)
        { TestDictionarySerialize(); }

        private void btnDeserializeCollection_Click(object sender, EventArgs e)
        { TestDictionaryDeserialize(); }

        private void tsSave_Click(object sender, EventArgs e)
        {
            SaveBarometersSetting();
            CheckCorrectNumberComPort();
        }
        private void tslRunAllComPort_Click(object sender, EventArgs e)
        { RunAllComPort(); }
        #endregion

        /// <summary> Логгирование данных в окно терминала. </summary>
        /// <param name="msgtype"> Тип сообщения для записи. </param>
        /// <param name="msg"> Строка содержащая сообщения для вывода. </param>
        public void Log(LogMsgType msgtype, string msg)
        {
            rtfTerminal.Invoke(new EventHandler(delegate
            {
                rtfTerminal.SelectedText = string.Empty;
                rtfTerminal.SelectionFont = new Font(rtfTerminal.SelectionFont, FontStyle.Bold);
                rtfTerminal.SelectionColor = LogMsgTypeColor[(int)msgtype];
                rtfTerminal.AppendText(msg);
                rtfTerminal.ScrollToCaret();
            }));
        }

        /// <summary>
        /// Проверка существования файла.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool CheckFile(string fileName)
        {
            bool success = true;

            // проверить FileName аргумент.
            if (fileName == null || fileName.Length == 0)
            {
                success = false;
                throw new ArgumentNullException($"Проверка файла: {fileName}, метод: {nameof(CheckFile)}, причина: отсутствует аргумент <{fileName}>");
            }

            // проверить существование файла.
            FileInfo fInfo = new FileInfo(fileName);

            // вызвать исключение при отсутствии 
            if (!fInfo.Exists)
            {
                success = false;
                throw new FileNotFoundException($"Проверка файла: {fileName}, метод: {nameof(CheckFile)}, причина: файл <{fileName}> не найден");
            }

            return success;
        }

        #region Json
        private string json;

        //private const string partBarometersSetting = "D:\\PartParametersString.json";
        private void TestDictionarySerialize()
        {
            Dictionary<string, DeviceBRS1M> deviceBRS1Ms = new Dictionary<string, DeviceBRS1M>();
            DeviceBRS1M d1 = new DeviceBRS1M
            {
                ParameterName = "Name Registration1",
                PortName = "COM3",
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                BaudRate = 1200,
                ClearOnOpen = false,
                ClearWithDTR = false,
                CurrentDataMode = DataMode.Hex,
                UnitInput = UnitOfMeasureInput.гПаВх,
                UnitOutput = UnitOfMeasureOutput.гПаВых
            };
            DeviceBRS1M d2 = new DeviceBRS1M
            {
                ParameterName = "Name Registration2",
                PortName = "COM3",
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                BaudRate = 1200,
                ClearOnOpen = false,
                ClearWithDTR = false,
                CurrentDataMode = DataMode.Hex,
                UnitInput = UnitOfMeasureInput.гПаВх,
                UnitOutput = UnitOfMeasureOutput.гПаВых
            };

            deviceBRS1Ms.Add(d1.ParameterName, d1);
            deviceBRS1Ms.Add(d2.ParameterName, d2);

            PartDevices allParameters = new PartDevices { All_BRS_1M = deviceBRS1Ms };

            json = JsonConvert.SerializeObject(allParameters, Formatting.Indented);
            //json = JsonConvert.SerializeObject(calculated)
            //Console.WriteLine(json)

            // серелизация JSON в строку, а затем запись строки в файл
            File.WriteAllText(partBarometersSetting, JsonConvert.SerializeObject(allParameters, Formatting.Indented));
        }

        private void TestDictionaryDeserialize()
        {
            // чтение файла в строку и десериализация JSON в Type
            PartDevices allParameters = JsonConvert.DeserializeObject<PartDevices>(File.ReadAllText(partBarometersSetting));
            if (allParameters.All_BRS_1M != null)
            {
                int I = 1;
                if (allParameters.All_BRS_1M.Count > 0)
                {
                    foreach (KeyValuePair<string, DeviceBRS1M> itemBRS1M in allParameters.All_BRS_1M)
                    {
                        Console.WriteLine(allParameters.All_BRS_1M["Name Registration" + I].ParameterName);
                        Console.WriteLine(itemBRS1M.Value.ParameterName);
                        I += 1;
                    }
                }
            }
        }
        #endregion

    }
}
