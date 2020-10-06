using System.ComponentModel;
using System.Drawing;
using static BarometersBRS1M.GlobalUtility;

namespace BarometersBRS1M
{
    public enum DataMode { Text, Hex }
    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };

    public enum UnitOfMeasureInput
    {
        [Description(cгПаВх)]
        гПаВх = 1,
        [Description(cммРтСтВх)]
        ммРтСтВх = 2
    }

    public enum UnitOfMeasureOutput
    {
        [Description(cгПаВых)]
        гПаВых = 1,
        [Description(cммРтСтВых)]
        ммРтСтВых = 2,
        [Description(cатм)]
        атм = 3,
        [Description(cммВодСт)]
        ммВодСт = 4,
        [Description(cбар)]
        бар = 5,
        [Description(cкгсм2)]
        кгсм2 = 6
    }

    public static class GlobalUtility
    {
        public const string cгПаВх = "гПаВх";                   // 1
        public const string cммРтСтВх = "ммРтСтВх МГ->РУД67";   // 2

        public const string cгПаВых = "гПаВых";         // 1
        public const string cммРтСтВых = "ммРтСтВых";   // 2
        public const string cатм = "атм";               // 3
        public const string cммВодСт = "ммВодСт";       // 4
        public const string cбар = "бар";               // 5
        public const string cкгсм2 = "кгсм2";           // 6

        // Различные цвета для логгирования информации
        public static Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };
        public const string USER_TEXT_CONTROL_BAROMETER = "Введите имя параметра";// "Барометр"; // для создания имени контрола по умолчанию
    }
}
