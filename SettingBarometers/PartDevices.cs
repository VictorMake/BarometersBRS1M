using System.Collections.Generic;

namespace BarometersBRS1M.SettingBarometers
{
    /// <summary>
    /// Вспомогательный класс, агрегирующий настройки устройств
    /// для JSON сериализации и десериализации Part.DeviceDetails при сохранении и восстановлении настроек группы устройств.
    /// </summary>
    public class PartDevices
    {
        public PartDevices()
        { All_BRS_1M = new Dictionary<string, DeviceBRS1M>(); }

        public PartDevices(Dictionary<string, DeviceBRS1M> inTuning) : base()
        { All_BRS_1M = inTuning; }

        /// <summary>
        /// Представляет устройства BRS1M при сериализации и десериализации
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DeviceBRS1M> All_BRS_1M { get; set; }
    }
}
