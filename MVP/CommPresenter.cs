using BarometersBRS1M.ViewModel;
using System;

namespace BarometersBRS1M
{
    /// <summary>
    /// Presente заменяет Контроллер в классическом MVC
    /// </summary>
    public class CommPresenter
    {
        // реализация Presenter-а, эта сущность заменяет Контроллер в классическом MVC - она делает все тоже самое, что и Контроллер, плюс кое-что еще.
        // Presenter может иметь прямую ссылку на экземпляр Модели.В то же время Presenter должен иметь ссылку и на экземпляр или даже экземпляры Представления, 
        // которые будут отображать данные Модели.Однако здесь, в отличие от случая с Моделью, создание конкретного экземпляра конкретного класса Представления, может обернуться большими неудобствами.
        // При наличии тесной связи между Presenter-ом и Представлением будет сложно реализовать замену Представлений и использование нескольких Представлений для одного Presenter-а,
        // к тому же, это затруднит независимое тестирование Presenter-а...Да и в целом, наличие такой связи может привести к неправильной работе Presenter-а при изменении Представления, что совсем не хорошо.
        // Решить проблему зависимости Presenter-а от Представления можно с помощью паттерна Inversion of Control (он же Dependency Injection по Фаулеру).
        // Для этого мы создаем интерфейс Представления через который и будет осуществляться все взаимодействие с Представлениями.

        // Контроллеры представляет собой лишь элементы системы, 
        // в чьи непосредственные обязанности входит приём данных из запроса и передача их другим элементам системы.
        // Контроллер представляет собой связующее звено между двумя основными компонентами системы — Моделью (Model) и Представлением (View). 
        // Модель ничего «не знает» о Представлении, а Контроллер не содержит в себе какой-либо бизнес-логики.
        // Presenter действует над Моделью и Представлением. Он извлекает данные из хранилища (Модели) и форматирует их для отображения в Представлении.
        // Контроллер представляет класс, с которого собственно и начинается работа приложения. Этот класс обеспечивает связь между моделью и представлением. 
        // Получая вводимые пользователем данные, контроллер исходя из внутренней логики при необходимости обращается к модели и генерирует соответствующее представление.

        private CommPortModel commModel = new CommPortModel();
        private IView viewTerminal;
        private ManagerComPorts managerPorts;
        private DeviceBRS1MViewModel deviceBRS1M;
        public CommPortModel GetCommPortModel { get { return commModel; } }

        public CommPresenter(IView view, ManagerComPorts inManagerPorts, DeviceBRS1MViewModel inDeviceBRS1M)
        {
            managerPorts = inManagerPorts;
            deviceBRS1M = inDeviceBRS1M;
            // После того, как интерфейс готов, надо придумать как подпихнуть экземпляр интерфейса в Presenter.
            // В данном случае, для простой задачи, можно сделать это через конструктор, в более сложных ситуациях Presenter может получать ссылку
            // на конкретный экземпляр через специальную фабрику представлений или даже сам являться фабрикой, 
            // порождающий необходимые Представления в зависимости от ситуации. 
            viewTerminal = view;
            commModel = new CommPortModel();

            SubscribeToEvents();
            // Применить сохранённые пользовательские настройки
            InitializeControlValues();
        }

        /// <summary>
        /// Обновление Представления новыми значениями модели.
        /// По сути Binding(привязка) значений модели к Представлению.
        /// </summary>
        private void SubscribeToEvents()
        {
            viewTerminal.SetTextEvent += new EventHandler<EventArgs>(ViewTerminal_SetTextEvent);
            viewTerminal.SetHexEvent += ViewTerminal_SetHexEvent;
            viewTerminal.ChangedSettingsEvent += ViewTerminal_ChangedSettingsEvent;
            viewTerminal.OpenPortEvent += ViewTerminal_OpenPortEvent;
            viewTerminal.DtrEnableEvent += ViewTerminal_DtrEnableEvent;
            viewTerminal.RtsEnableEvent += ViewTerminal_RtsEnableEvent;
            viewTerminal.SendDataEvent += ViewTerminal_SendDataEvent;

            commModel.RaiseLogEvent += new EventHandler<LogEventArgs>(OnRaiseLogEvent);
            commModel.RaiseUpdatePinStateEvent += CommModel_RaiseUpdatePinStateEvent;
            commModel.DtrEnableEvent += CommModel_DtrEnableEvent;
            commModel.RtsEnableEvent += CommModel_RtsEnableEvent;
            commModel.BarometerEvent += CommModel_BarometerEvent;
        }

        /// <summary> Заполнить контролы формы и модель значениями пользовательских настроек. </summary>
        private void InitializeControlValues()
        {
            commModel.InitializeModelValues(deviceBRS1M);

            viewTerminal.ParameterName = commModel.ParameterName;
            viewTerminal.Parity = commModel.Parity;
            viewTerminal.StopBits = commModel.StopBits;
            viewTerminal.DataBits = commModel.DataBits;
            viewTerminal.BaudRate = commModel.BaudRate;
            viewTerminal.ClearOnOpen = commModel.ClearOnOpen;
            viewTerminal.ClearWithDTR = commModel.ClearWithDTR;
            viewTerminal.CurrentDataMode = commModel.CurrentDataMode;
            viewTerminal.UnitInput = commModel.UnitInput;
            viewTerminal.UnitOutput = commModel.UnitOutput;

            viewTerminal.InitializeIsSuccess = true;
            viewTerminal.InitializeControlValues(commModel.PortName, commModel.IsOpen, managerPorts);
        }

        /// <summary> Закрытие порта в модели </summary>
        internal void CloseComPort()
        {
            commModel.CloseComPort();
            //commModel.SaveSettings();// Сохранить пользовательские настройки.
            // изменить состояние контролов формы
            viewTerminal.EnableControls(commModel.IsOpen);
            //viewTerminal.CloseTerminal();
        }

        /// <summary> При выходе из приложения полная ликвидация </summary>
        internal void CloseCommPortModel()
        { commModel.CloseCommPortModel(); }

        #region Обработчики событий интерфейса IView
        private void ViewTerminal_SetTextEvent(object sender, EventArgs e)
        { commModel.CurrentDataMode = DataMode.Text; }
        private void ViewTerminal_SetHexEvent(object sender, EventArgs e)
        { commModel.CurrentDataMode = DataMode.Hex; }

        private void ViewTerminal_ChangedSettingsEvent(object sender, SettingsEventArgs e)
        {
            commModel.PortName = e.PortName;
            commModel.ParameterName = e.ParameterName;
            commModel.Parity = e.Parity;
            commModel.StopBits = e.StopBits;
            commModel.DataBits = e.DataBits;
            commModel.BaudRate = e.BaudRate;
            commModel.ClearOnOpen = e.ClearOnOpen;
            commModel.ClearWithDTR = e.ClearWithDTR;
            commModel.UnitInput = e.UnitInput;
            commModel.UnitOutput = e.UnitOutput;
        }

        public void ViewTerminal_OpenPortEvent(object sender, EventArgs e)
        {
            commModel.OpenPort();
            // изменить состояние контролов формы
            viewTerminal.EnableControls(commModel.IsOpen);

            // Если порт открыт, переключить фокус на бокс отправки
            if (commModel.IsOpen)
            {
                viewTerminal.TextSendDataFocus();
                if (viewTerminal.ClearOnOpen) viewTerminal.ClearTerminal();
            }
        }

        private void ViewTerminal_DtrEnableEvent(object sender, BooleanEventArgs e)
        { commModel.RtsEnable = e.Checked; }

        private void ViewTerminal_RtsEnableEvent(object sender, BooleanEventArgs e)
        { commModel.DtrEnable = e.Checked; }

        private void ViewTerminal_SendDataEvent(object sender, SendDataEventArgs e)
        { commModel.SendData(e.SendData); }
        #endregion

        #region Обработчики событий Модели
        private void OnRaiseLogEvent(object sender, LogEventArgs e)
        { viewTerminal.Log(e.LogType, e.Message); }

        private void CommModel_RaiseUpdatePinStateEvent(object sender, UpdatePinStateEventArgs e)
        { viewTerminal.UpdatePinState(e); }

        private void CommModel_DtrEnableEvent(object sender, BooleanEventArgs e)
        { commModel.DtrEnable = e.Checked; }

        private void CommModel_RtsEnableEvent(object sender, BooleanEventArgs e)
        { commModel.RtsEnable = e.Checked; }

        private void CommModel_BarometerEvent(object sender, BarometrEventArgs e)
        {
            viewTerminal.BarometerInput(e.BarometerInput);
            viewTerminal.BarometerOtput(e.BarometerOutput);
        }
        #endregion
    }
}
