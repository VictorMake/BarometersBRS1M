using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using BarometersBRS1M.ViewModel;
using System.Timers;

namespace BarometersBRS1M
{
    /// <summary>
    /// Модель предоставляет знания: данные и методы работы с этими данными, реагирует на запросы, изменяя своё состояние. 
    /// Не содержит информации, как эти знания можно визуализировать.
    /// </summary>
    public class CommPortModel : IDisposable
    {
        #region PublicEvents
        public event EventHandler<UpdatePinStateEventArgs> RaiseUpdatePinStateEvent;
        public event EventHandler<LogEventArgs> RaiseLogEvent;
        public event EventHandler<BooleanEventArgs> DtrEnableEvent;
        public event EventHandler<BooleanEventArgs> RtsEnableEvent;
        public event EventHandler<BarometrEventArgs> BarometerEvent;
        #endregion

        #region Public Properties
        /// <summary>
        /// Возвращает состояние линии обнаружения несущей для порта.
        /// </summary>
        public bool CDHolding { get { return ComPort.CDHolding; } }
        /// <summary>
        /// Возвращает состояние линии готовности к приему.
        /// </summary>
        public bool CtsHolding { get { return ComPort.CtsHolding; } }
        /// <summary>
        /// Возвращает или задает состояние сигнала готовности данных (DSR).
        /// </summary>
        public bool DsrHolding { get { return ComPort.DsrHolding; } }
        /// <summary>
        /// Возвращает или задает значение, включающее поддержку сигнала готовности терминала
        /// (DTR) в сеансе последовательной связи.
        /// </summary>
        public bool DtrEnable { set => ComPort.DtrEnable = value; }
        /// <summary>
        ///  Возвращает или задает значение, показывающее, включен ли сигнал запроса передачи
        ///  (RTS) в сеансе последовательной связи.
        /// </summary>
        public bool RtsEnable { set => ComPort.RtsEnable = value; }
        /// <summary>
        ///  Возвращает значение, указывающее открытое или закрытое состояние объекта System.IO.Ports.SerialPort.
        /// </summary>
        public bool IsOpen { get { return ComPort.IsOpen; } }
        /// <summary>
        /// Возвращает или задает последовательный порт, в частности, любой из доступных портов COM.
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// Возвращает или задает имя параметра ртображающего значение БРС в регистраторе
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// Возвращает или задает протокол контроля четности.
        /// </summary>
        public Parity Parity { get; set; }
        /// <summary>
        /// Возвращает или задает стандартное число стоповых битов в байте.
        /// </summary>
        public StopBits StopBits { get; set; }
        /// <summary>
        /// Возвращает или задает стандартное число битов данных в байте.
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// Возвращает или задает скорость передачи для последовательного порта (в бодах).
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// Очищать поле вывода при открытии
        /// </summary>
        public bool ClearOnOpen { get; set; }
        /// <summary>
        /// Очищать поле вывода при сигнале готовности терминала
        /// (DTR) в сеансе последовательной связи.
        /// </summary>
        public bool ClearWithDTR { get; set; }
        /// <summary>
        /// Отображение информации в текстовом или 16-ном виде
        /// </summary>
        public DataMode CurrentDataMode { get; set; }
        /// <summary>
        /// Возвращает или задает входную единицу измерения установленной на БРС
        /// </summary>
        public UnitOfMeasureInput UnitInput { get; set; }
        /// <summary>
        /// Возвращает или задает выходную расчётную единицу измерения отображаемую параметром в регистраторе
        /// </summary>
        public UnitOfMeasureOutput UnitOutput { get; set; }

        /// <summary>
        /// Пересчитанное замеренное давление БРС
        /// </summary>
        public double PressureOutput { get { return pressureOutputValue; } }
        #endregion


        private const int TIMER_INTERVAL = 1000;
        private readonly Timer aTimer;

        // основной контрол коммуникуции через RS-232 порт
        private SerialPort ComPort = new SerialPort();

        /// <summary>
        /// Конструктор для установки свойств порта по умолчанию
        /// Для приема данных нам нужно создать обработчик события EventHandler для "SerialDataReceivedEventHandler"
        /// </summary>
        public CommPortModel()
        {
            aTimer = new Timer(TIMER_INTERVAL);
            aTimer.Elapsed += OnTimedEvent;
            AddEventHandler();
        }

        /// <summary>
        /// назначить обработчики событий приёма данных и изменения линий порта
        /// </summary>
        private void AddEventHandler()
        {
            aTimer.Enabled = true;
            // подписывание на обработчик приёма данных для БРС работает нестабильно при очистки ресурсов его порта
            // поэтому применил обработку данных по таймеру
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            ComPort.PinChanged += new SerialPinChangedEventHandler(Comport_PinChanged);
        }

        /// <summary>
        /// удалить обработчики событий приёма данных и изменения линий порта
        /// </summary>
        private void RemoveEventHandler()
        {
            aTimer.Enabled = false;
            //ComPort.DataReceived -= new SerialDataReceivedEventHandler(Port_DataReceived);
            ComPort.PinChanged -= new SerialPinChangedEventHandler(Comport_PinChanged);
        }

        /// <summary>
        /// Обработчик изменения линий порта.
        /// Обновить состояние пинов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Comport_PinChanged(object sender, SerialPinChangedEventArgs e)
        { OnRaiseCustomEvent(new UpdatePinStateEventArgs(CDHolding, CtsHolding, DsrHolding)); }

        /// <summary>
        /// Обернуть событие вызова в защищенный виртуальный метод который
        /// позволяет производным классам переопределять вызова событий
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRaiseCustomEvent(UpdatePinStateEventArgs e)
        {
            // Сделать временную копию события, чтобы избежать возможности
            // состязания если последний абонента отписывается 
            // сразу после проверки на нуль и до вызова самого события события.
            RaiseUpdatePinStateEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Обернуть событие вызова в защищенный виртуальный метод который
        /// позволяет производным классам переопределять вызова событий
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRaiseLogEvent(LogEventArgs e)
        {
            // Сделать временную копию события, чтобы избежать возможности
            // состязания если последний абонента отписывается 
            // сразу после проверки на нуль и до вызова самого события события.
            RaiseLogEvent?.Invoke(this, e);
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Если порт был закрыт, ни чего не делать
            if (ComPort == null) return;
            if (!ComPort.IsOpen) return;

            Port_DataReceived(ComPort, null);
        }
        /// <summary>
        /// Обработчик получения данных из порта
        /// Этот метод будет вызываться, когда имеются данные ожидающие в порту буфера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Если порт был закрыт, ни чего не делать
            if (ComPort == null) return;
            if (!ComPort.IsOpen) return;

            try
            {
                // Определить какой режим (string or binary) установил пользователь
                if (CurrentDataMode == DataMode.Text)
                {
                    // Прочитать все данные ожидающие в порту буфера
                    string data = ComPort.ReadExisting();
                    // Вывести текст для пользователя на терминал
                    OnRaiseLogEvent(new LogEventArgs(LogMsgType.Incoming, data));
                }
                else
                {
                    //// Получить количество байтов ожидания в порту буфера
                    //// Создать байтовый буфер массив для хранения входящих данных
                    byte[] buffer = new byte[ComPort.BytesToRead];
                    // Прочитать данные из порта и хранить его в наш буфер
                    ComPort.Read(buffer, 0, buffer.Length);
                    // Показать пользователю входящих данных в шестнадцатеричном формате
                    // для Барометра закоментировал и заменил
                    //OnRaiseLogEvent(new LogEventArgs(LogMsgType.Incoming, ByteArrayToHexString(buffer)));

                    buffer.ToList().ForEach(b => recievedData.Enqueue(b));
                    SendConvertedQueueByteToFormatNumber();
                }

                //Thread.Sleep(1000); не использовать - тормозит поток
            }
            catch (Exception ex)
            {
                OnRaiseLogEvent(new LogEventArgs(LogMsgType.Error, $"Ошибка считывания буфера порта!{Environment.NewLine}{ex.Message}{Environment.NewLine}"));
            }
        }

        /// <summary> Преобразует массив байтов в форматированную строку шестнадцатеричных цифр (пример: E4 CA B2)</summary>
        /// <param name="data"> Массив байтов, которые будут переведены в строку шестнадцатеричных цифр. </param>
        /// <returns> Возвращает нормально отформатированную строку шестнадцатеричных цифр с пробелами. </returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// Открывает новое соединение последовательного порта.
        /// </summary>
        public void OpenPort()
        {
            bool error = false;

            // Если порт открыт, закрыть его
            if (ComPort.IsOpen) ComPort.Close();
            else
            {
                //AddEventHandler();
                // Установить настройки порта
                ComPort.PortName = PortName;
                ComPort.Parity = Parity;
                ComPort.StopBits = StopBits;
                ComPort.DataBits = DataBits;
                ComPort.BaudRate = BaudRate;
                //ComPort.Handshake = Handshake.None;
                ////' Set the read/write timeouts
                // Срок ожидания чтения первоначально устанавливается равным 500 миллисекундам 
                //ComPort.ReadTimeout = 2000;
                //ComPort.WriteTimeout = 500;
                try
                {
                    ComPort.Open();// Открыть порт
                }
                // catch (InvalidOperationException ex)
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error)
                {
                    OnRaiseLogEvent(new LogEventArgs(LogMsgType.Error, "COM Port Недоступен\nНе удалось открыть COM порт. Скорее всего, он уже находится в использовании, был удален или недоступен.\n"));
                }
                else
                {
                    // Показать исходные линии порта
                    OnRaiseCustomEvent(new UpdatePinStateEventArgs(CDHolding, CtsHolding, DsrHolding));
                    OnDtrEnableEventEvent(new BooleanEventArgs(ComPort.DtrEnable));
                    OnRtsEnableEvent(new BooleanEventArgs(ComPort.RtsEnable));
                    OnRaiseLogEvent(new LogEventArgs(LogMsgType.Normal, string.Format("Порт открыт в {0}\n", DateTime.Now)));
                }
            }
        }

        /// <summary>
        /// Простое закрытие порта
        /// </summary>
        public void CloseComPort()
        {
            if (ComPort != null)
            {
                if (ComPort.IsOpen)
                {
                    ComPort.ReadExisting();
                    ComPort.Close();
                }
            }
        }

        #region CloseCommPortModel
        /// <summary>
        /// При выходе из приложения полная ликвидация
        /// </summary>
        public void CloseCommPortModel()
        {
            //ComPort = null;// самое бестолковое (ресурсы не освобождаются), не зависает, но может при повтором загрузке показать занятые порты
            if (ComPort != null)
            {
                RemoveEventHandler();
                // The COM port exists.
                if (ComPort.IsOpen)
                {
                    //comPort.DataReceived -= new SerialDataReceivedEventHandler(comPort_DataReceived);
                    // Wait for the transmit buffer to empty.
                    //while ((comPort.BytesToRead > 0))
                    //{
                    //}
                    ComPort.ReadExisting();
                    //ComPort = null;
                }
                Dispose();
            }

            // Изредка зависает
            //using (ComPort)
            //{
            //    if (ComPort != null)
            //    {
            //        //RemoveEventHandler();
            //        // The COM port exists.
            //        if (ComPort.IsOpen)
            //        {
            //            //comPort.DataReceived -= new SerialDataReceivedEventHandler(comPort_DataReceived);
            //            // Wait for the transmit buffer to empty.
            //            //while ((comPort.BytesToRead > 0))
            //            //{
            //            //}
            //            ComPort.ReadExisting();
            //            ComPort.Close();
            //        }
            //    }
            //}

            // Работает с зависанием
            //if (ComPort != null)
            //{
            //    try
            //    {
            //    }
            //    finally
            //    {
            //        if (ComPort != null)
            //            ComPort.Dispose();
            //    }
            //}
        }

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                //ComPort.Dispose();// тоже иногда виснет
                //ComPort = null;// виснет т.к. уже ликвидирован ComPort.Dispose();
            }

            // Free any unmanaged objects here.
            //
            ComPort.Dispose();
            disposed = true;
        }

        ~CommPortModel()
        {
            Dispose(false);
        }
        #endregion

        protected virtual void OnDtrEnableEventEvent(BooleanEventArgs e)
        { DtrEnableEvent?.Invoke(this, e); }

        protected virtual void OnRtsEnableEvent(BooleanEventArgs e)
        { RtsEnableEvent?.Invoke(this, e); }

        /// <summary> Отправить данные пользователя, введенные в поле "отправить".</summary>
        public void SendData(string inSendData)
        {
            if (CurrentDataMode == DataMode.Text)
            {
                // Отправить текст пользователя прямо в порт
                ComPort.Write(inSendData);
                // Показать в окне терминала текст пользователя
                OnRaiseLogEvent(new LogEventArgs(LogMsgType.Outgoing, inSendData + "\n"));
            }
            else
            {
                try
                {
                    // Преобразовать строку пользователя шестнадцатеричных цифр (пример: B4 CA E2) в байтовый массив
                    byte[] data = HexStringToByteArray(inSendData);
                    // Отправить бинарные данные в порт
                    ComPort.Write(data, 0, data.Length);
                    // Показать шестнадцатеричные цифры в окне терминала
                    OnRaiseLogEvent(new LogEventArgs(LogMsgType.Outgoing, ByteArrayToHexString(data) + "\n"));
                }
                catch (FormatException)
                {
                    // Информировать пользователя, если шестнадцатеричная строка была не правильно отформатирована
                    OnRaiseLogEvent(new LogEventArgs(LogMsgType.Error, $"Не правильно отформатирована шестнадцатеричная строка: {inSendData}\n"));
                }
            }
        }

        /// <summary> Преобразовать строку шестнадцатеричных цифр (пример: E4 CA B2) в массив байтов. </summary>
        /// <param name="s"> Строки, содержащие шестнадцатеричные цифры (с или без пробелов). </param>
        /// <returns> Возвращает массив байтов. </returns>
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");// удалить любые пробелы из строки
            // создать байтовый массив длиной делим на 2(Hex 2 символов)
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                //преобразовать каждый набор из 2-х символов в байт и добавляем в массив
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        //private Settings mSettings = Settings.Default;

        /// <summary> Заполнить контролы формы и модель значениями пользовательских настроек. </summary>
        public void InitializeModelValues(DeviceBRS1MViewModel deviceBRS1M)
        {
            // Загрузить пользовательские настройки
            //mSettings.Reload();

            //PortName = mSettings.PortName;
            //Parity = mSettings.Parity;
            //ParameterName = mSettings.ParameterName;
            //StopBits = mSettings.StopBits;
            //DataBits = mSettings.DataBits;
            //BaudRate = mSettings.BaudRate;
            //ClearOnOpen = mSettings.ClearOnOpen;
            //ClearWithDTR = mSettings.ClearWithDTR;
            //CurrentDataMode = mSettings.DataMode;
            //UnitInput = mSettings.UnitInput;
            //UnitOutput = mSettings.UnitOutput;

            PortName = deviceBRS1M.PortName;
            Parity = deviceBRS1M.Parity;
            ParameterName = deviceBRS1M.ParameterName;
            StopBits = deviceBRS1M.StopBits;
            DataBits = deviceBRS1M.DataBits;
            BaudRate = deviceBRS1M.BaudRate;
            ClearOnOpen = deviceBRS1M.ClearOnOpen;
            ClearWithDTR = deviceBRS1M.ClearWithDTR;
            CurrentDataMode = deviceBRS1M.CurrentDataMode;
            UnitInput = deviceBRS1M.UnitInput;
            UnitOutput = deviceBRS1M.UnitOutput;
        }

        ///// <summary> Сохранить пользовательские настройки. </summary>
        //public void SaveSettings()
        //{
        //    //mSettings.PortName = PortName;
        //    //mSettings.ParameterName = ParameterName;
        //    //mSettings.Parity = Parity;
        //    //mSettings.StopBits = StopBits;
        //    //mSettings.DataBits = DataBits;
        //    //mSettings.BaudRate = BaudRate;
        //    //mSettings.ClearOnOpen = ClearOnOpen;
        //    //mSettings.ClearWithDTR = ClearWithDTR;
        //    //mSettings.DataMode = CurrentDataMode;
        //    //mSettings.UnitInput = UnitInput;
        //    //mSettings.UnitOutput = UnitOutput;
        //    //mSettings.Save();
        //}

        #region Барометр

        const int byteToRead = 14;
        const int byteToDisplay = 7;

        const byte byteAA = 170;// символ AA
        byte[] displayBuffer = new byte[byteToDisplay];
        private Queue<byte> recievedData = new Queue<byte>();

        void SendConvertedQueueByteToFormatNumber()
        {
            // определить есть данные в очереди
            if (recievedData.Count >= byteToRead)
            {
                bool detected = false;
                int j = 0;

                foreach (byte b in Enumerable.Range(0, byteToRead).Select(i => recievedData.Dequeue()).ToArray())
                {
                    if (detected == false && b == byteAA) detected = true;

                    if (detected)
                    {
                        displayBuffer[j] = b;
                        if (j == byteToDisplay - 1) break;
                        j++;
                    }
                }

                if (detected)
                {
                    // показать данные барометра
                    OnBarometerEvent(new BarometrEventArgs(ByteToValueBRS(displayBuffer),
                                                            SolvePresure(actualBar, UnitInput, UnitOutput).ToString("F", CultureInfo.InvariantCulture)));
                }

                recievedData.Clear();
            }
        }

        protected virtual void OnBarometerEvent(BarometrEventArgs e)
        { BarometerEvent?.Invoke(this, e); }

        double actualBar;// показания прибора БРС
        double pressureOutputValue;// пересчитанные значения

        private string ByteToValueBRS(byte[] comByte)
        {
            string hexString = string.Empty;
            // первый символ пропустить
            for (int i = 1; i < byteToDisplay; i++)
                hexString += int.Parse(comByte[i].ToString("X2"));

            actualBar = int.Parse(hexString, NumberStyles.Integer) / 100.0;
            return actualBar.ToString("F", CultureInfo.InvariantCulture);
        }

        private double SolvePresure(double pessureInput, UnitOfMeasureInput едИзмВход, UnitOfMeasureOutput едИзмВыход)
        {
            if ((int)едИзмВход == (int)едИзмВыход)
            {
                pressureOutputValue = pessureInput;
            }
            else
            {
                if (едИзмВход == UnitOfMeasureInput.гПаВх)
                {
                    switch (едИзмВыход)
                    {
                        case UnitOfMeasureOutput.ммРтСтВых:
                            pressureOutputValue = pessureInput * 100.0 / 133.322;
                            break;
                        case UnitOfMeasureOutput.атм:
                            pressureOutputValue = pessureInput * 100.0 / 101325.0;
                            break;
                        case UnitOfMeasureOutput.ммВодСт:
                            pressureOutputValue = pessureInput * 100.0 / 9.80665;
                            break;
                        case UnitOfMeasureOutput.бар:
                            pressureOutputValue = pessureInput * 100.0 / 100000.0;
                            break;
                        case UnitOfMeasureOutput.кгсм2:
                            pressureOutputValue = pessureInput * 100.0 / 98066.5;
                            break;
                    }
                }
                else//ммРтСтВх
                {
                    switch (едИзмВыход)
                    {
                        case UnitOfMeasureOutput.гПаВых:
                            pressureOutputValue = pessureInput * 133.322 / 100.0;
                            break;
                        case UnitOfMeasureOutput.атм:
                            pressureOutputValue = pessureInput / 760.0;
                            break;
                        case UnitOfMeasureOutput.ммВодСт:
                            pressureOutputValue = pessureInput * 133.322 / 9.80665;
                            break;
                        case UnitOfMeasureOutput.бар:
                            pressureOutputValue = pessureInput * 133.322 / 100000.0;
                            break;
                        case UnitOfMeasureOutput.кгсм2:
                            pressureOutputValue = pessureInput * 133.322 / 98066.5;
                            break;
                    }
                }
            }

            return pressureOutputValue;
        }

        /*
         More on Transferring Text
        The example application sends and receives data —
        including numbers — as codes that represent text
        characters. For example, to send “1,” the port transmits
        the byte 31h (00110001), which is the ASCII code for
        the character 1. To send “111,” the port transmits 31h
        three times; once for each character in the number.
        Treating data as text is the obvious choice for
        transferring strings or files that contain text. To transfer
        numeric values, one option is to send the bytes as text
        in ASCII hex format.
        Any byte value can be expressed as a pair of
        hexadecimal (base 16) characters where the letters
        A–F represent values from 10 to 15.
        For example, consider the decimal number 225.
        Expressed as a binary value, it’s (2^7) + (2^6) +(2^5)
        +(2^0), or: 11100001. In hexadecimal, it’s E1. The ASCII
        codes for the characters “E” and “1” expressed in hexadecimal
        are: 45 31. So the binary representation of E1h
        in ASCII hex consists of these two bytes: 01000101
        00110001.
        A serial link using ASCII hex format sends the
        decimal value 225 by transmitting the two bytes above.
        A computer that receives ASCII hex values can convert
        the characters to numeric values or use the data as-is.
        An advantage to ASCII hex is being able to represent
        any byte value using just 16 ASCII codes. The values
        30h–39h represent the characters 0–9, and 41h–46h
        represent the characters A–F. Code that allows lower-case
        letters uses 61h–66h to represent a–f.
        All of the other values are available for alternate
        uses, such as software flow-control codes or an
        end-of-file indicator. Because the ASCII hex codes are all
        less than 80h, a serial link transmitting these values can
        save a little time by ignoring the high bit and transmitting
        seven-bit values.
        */
        //private byte ConvertAsciiHexToByte(string asciiHexToConvert)
        //{
        //    byte convertedValue = 0;
        //    convertedValue = Convert.ToByte(asciiHexToConvert, 16);
        //    return convertedValue;
        //}
        //private string ConvertByteToAsciiHex(byte byteToConvert)
        //{
        //    string convertedValue = null;
        //    //convertedValue = Hex$(byteToConvert);
        //    convertedValue = Conversion.Hex(byteToConvert);

        //    string hex = "142CBD";
        //    // this returns 1322173
        //    int intValue1 = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        //    string prefixedHex = "0x142CBD";
        //    // this works, and returns 1322173
        //    int intValue2 = Convert.ToInt32(prefixedHex, 16);

        //    return convertedValue;
        //}

        #endregion

    }
}
