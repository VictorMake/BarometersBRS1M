using System.IO.Ports;

namespace BarometersBRS1M.SettingBarometers
{
    /// <summary>
    /// Базовый класс, обозначающий какие-либо устройства работающие с COM Port
    /// </summary>
    public abstract class DeviceDetail
    {
        /// <summary>
        /// Возвращает или задает последовательный порт, в частности, любой из доступных портов COM.
        /// </summary>
        public virtual string PortName { get; set; }
        /// <summary>
        /// Возвращает или задает протокол контроля четности.
        /// </summary>
        public virtual Parity Parity { get; set; }
        /// <summary>
        /// Возвращает или задает стандартное число стоповых битов в байте.
        /// </summary>
        public virtual StopBits StopBits { get; set; }
        /// <summary>
        /// Возвращает или задает стандартное число битов данных в байте.
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// Возвращает или задает скорость передачи для последовательного порта (в бодах).
        /// </summary>
        public int BaudRate { get; set; }

        //<JsonIgnore>
        // вместо аттрибута <JsonIgnore> можно применить функцию оканчивающуюся на имя выключаемого свойства
        public bool ShouldSerializeIsDirty() { return false; }

        /// <summary>
        ///  Получает или задает, флаг, что какое-то свойство данного класса было изменено и требует сохранения изменений
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDirty { get; set; }
    }
}
