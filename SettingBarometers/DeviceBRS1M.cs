using static BarometersBRS1M.GlobalUtility;

namespace BarometersBRS1M.SettingBarometers
{
    /// <summary>
    /// Представляет Настроечные параметры для прибора БРС-1М
    /// </summary>
    public class DeviceBRS1M : DeviceDetail
    {
        public DeviceBRS1M()
        { ParameterName = USER_TEXT_CONTROL_BAROMETER; } // "Введите имя параметра"
       
        /// <summary>
        /// Получает или задает имя Настроечного параметра
        /// </summary>
        public virtual string ParameterName { get; set; }
        /// <summary>
        /// Очищать поле вывода при открытии
        /// </summary>
        public virtual bool ClearOnOpen { get; set; }
        /// <summary>
        /// Очищать поле вывода при сигнале готовности терминала
        /// (DTR) в сеансе последовательной связи.
        /// </summary>
        public virtual bool ClearWithDTR { get; set; }
        /// <summary>
        /// Отображение информации в текстовом или 16-ном виде
        /// </summary>
        public virtual DataMode CurrentDataMode { get; set; }
        /// <summary>
        /// Возвращает или задает входную единицу измерения установленной на БРС
        /// </summary>
        public virtual UnitOfMeasureInput UnitInput { get; set; }
        /// <summary>
        /// Возвращает или задает выходную расчётную единицу измерения отображаемую параметром в регистраторе
        /// </summary>
        public virtual UnitOfMeasureOutput UnitOutput { get; set; }
    }
}
