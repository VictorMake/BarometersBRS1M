using System;
using System.IO.Ports;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// Модель ViewModel отдельного прибора DeviceBRS1M
    /// </summary>
    public class DeviceBRS1MViewModel : DeviceDetaillViewModel
    {
        /// <summary>
        /// Объект DeviceBRS1M для резервирования этой модели ViewModel
        /// </summary>
        private DeviceBRS1M _DeviceBRS1M;

        /// <summary>
        /// Инициализирует новый экземпляр класса DeviceBRS1MViewModel.
        /// </summary>
        /// <param name="inDeviceBRS1M">Базовый базовое устройство, на котором должна быть основана эта модель ViewModel</param>
        public DeviceBRS1MViewModel(DeviceBRS1M inDeviceBRS1M)
        { _DeviceBRS1M = inDeviceBRS1M ?? throw new ArgumentNullException(nameof(inDeviceBRS1M)); }

        /// <summary>
        /// Базовое устройство, на котором основана эта модель ViewModel
        /// </summary>
        public override DeviceDetail Model
        { get { return _DeviceBRS1M; } }

        /// <summary>
        /// Возвращает или задает последовательный порт, в частности, любой из доступных портов COM.
        /// </summary>
        public string PortName
        {
            get { return _DeviceBRS1M.PortName; }
            set
            {
                _DeviceBRS1M.PortName = value;
                //OnPropertyChanged(nameof(PortName));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }

        /// <summary>
        /// Получает или задает имя Настроечного параметра
        /// </summary>
        public string ParameterName
        {
            get { return _DeviceBRS1M.ParameterName; }
            set
            {
                _DeviceBRS1M.ParameterName = value;
                //OnPropertyChanged(nameof(ParameterName));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает протокол контроля четности.
        /// </summary>
        public Parity Parity
        {
            get { return _DeviceBRS1M.Parity; }
            set
            {
                _DeviceBRS1M.Parity = value;
                //OnPropertyChanged(nameof(Parity));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает стандартное число стоповых битов в байте.
        /// </summary>
        public StopBits StopBits
        {
            get { return _DeviceBRS1M.StopBits; }
            set
            {
                _DeviceBRS1M.StopBits = value;
                //OnPropertyChanged(nameof(StopBits));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает стандартное число битов данных в байте.
        /// </summary>
        public int DataBits
        {
            get { return _DeviceBRS1M.DataBits; }
            set
            {
                _DeviceBRS1M.DataBits = value;
                //OnPropertyChanged(nameof(DataBits));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает скорость передачи для последовательного порта (в бодах).
        /// </summary>
        public int BaudRate
        {
            get { return _DeviceBRS1M.BaudRate; }
            set
            {
                _DeviceBRS1M.BaudRate = value;
                //OnPropertyChanged(nameof(BaudRate));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Очищать поле вывода при открытии
        /// </summary>
        public bool ClearOnOpen
        {
            get { return _DeviceBRS1M.ClearOnOpen; }
            set
            {
                _DeviceBRS1M.ClearOnOpen = value;
                //OnPropertyChanged(nameof(ClearOnOpen));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Очищать поле вывода при сигнале готовности терминала
        /// (DTR) в сеансе последовательной связи.
        /// </summary>
        public bool ClearWithDTR
        {
            get { return _DeviceBRS1M.ClearWithDTR; }
            set
            {
                _DeviceBRS1M.ClearWithDTR = value;
                //OnPropertyChanged(nameof(ClearWithDTR));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Отображение информации в текстовом или 16-ном виде
        /// </summary>
        public DataMode CurrentDataMode
        {
            get { return _DeviceBRS1M.CurrentDataMode; }
            set
            {
                _DeviceBRS1M.CurrentDataMode = value;
                //OnPropertyChanged(nameof(CurrentDataMode));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает входную единицу измерения установленной на БРС
        /// </summary>
        public UnitOfMeasureInput UnitInput
        {
            get { return _DeviceBRS1M.UnitInput; }
            set
            {
                _DeviceBRS1M.UnitInput = value;
                //OnPropertyChanged(nameof(UnitInput));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
        /// <summary>
        /// Возвращает или задает выходную расчётную единицу измерения отображаемую параметром в регистраторе
        /// </summary>
        public UnitOfMeasureOutput UnitOutput
        {
            get { return _DeviceBRS1M.UnitOutput; }
            set
            {
                _DeviceBRS1M.UnitOutput = value;
                //OnPropertyChanged(nameof(UnitOutput));
                OnPropertyChanged();
                _DeviceBRS1M.IsDirty = true;
            }
        }
    }
}
