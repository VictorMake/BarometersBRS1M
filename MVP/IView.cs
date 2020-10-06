using System;
using System.IO.Ports;

namespace BarometersBRS1M
{
    // Интерфейс Представления, будет состоять из свойств для вывода посчитанных значений, ввода новых данных и событий оповещающих Presenter, что данные введены. 
    public interface IView
    {
        #region Events
        event EventHandler<EventArgs> SetTextEvent;
        event EventHandler<EventArgs> SetHexEvent;
        event EventHandler<SettingsEventArgs> ChangedSettingsEvent;
        event EventHandler<EventArgs> OpenPortEvent;
        event EventHandler<BooleanEventArgs> DtrEnableEvent;
        event EventHandler<BooleanEventArgs> RtsEnableEvent;
        event EventHandler<SendDataEventArgs> SendDataEvent;
        #endregion

        #region Property
        // Несут состояние (binding) свойств контролов
        string PortName { get; set; }
        string ParameterName { get; set; }
        bool TextData { get; set; }
        bool HexData { get; set; }
        Parity Parity { get; set; }
        StopBits StopBits { get; set; }
        int DataBits { get; set; }
        int BaudRate { get; set; }
        bool ClearOnOpen { get; set; }
        bool ClearWithDTR { get; set; }
        bool DtrEnable { get; set; }
        bool RtsEnable { get; set; }
        DataMode CurrentDataMode { get; set; }
        /// <summary>
        /// Предотвратить вызов событий до конца инициализации
        /// </summary>
        bool InitializeIsSuccess { get; set; }
        UnitOfMeasureInput UnitInput { get; set; }
        UnitOfMeasureOutput UnitOutput { get; set; }
        #endregion

        #region Methods
        void InitializeControlValues(string PortName, bool IsPortOpen, ManagerComPorts ManagerPorts);// Установить значения контролов формы по умолчанию.
        void Log(LogMsgType msgtype, string msg);// Логгирование данных в окно терминала.
        void UpdatePinState(UpdatePinStateEventArgs e);// Обновить состояние пинов
        void SendData();// Отправить пользвательские данные введённые в поле для отправки.
        void BarometerInput(string inBarometerInput);// Обновить значение барометра входное
        void BarometerOtput(string inBarometerOutput);// Обновить значение барометра пересчитанное
        void EnableControls(bool IsPortOpen);// Включить/отключить контролы на основании открытости порта.
        void ClearTerminal();// Очистка поля вывода
        void TextSendDataFocus();// Фокус на поле отправки данных
        //void CloseTerminal();
        #endregion
    }
}
