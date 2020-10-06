using System.Collections.Generic;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M
{
    /// <summary>
    /// Вспомогательный метод создания фиктивных данных.
    /// </summary>
    public class Generation
    {
        //private string typeOfCurrentPart = string.Empty;
        /// <summary>
        /// Выполняет построение FakeUnitOfWork с помощью реалистичных заполняемых данных
        /// </summary>
        /// <returns>Контекст, заполняемый данными</returns>
        public FakePartContext BuildFakeSession(string inPathSetting)
        {
            List<Part> partList = new List<Part>();
            Part itemPart = new Part { DeviceDetails = new List<DeviceDetail>() };
            itemPart.PopulateDeviceDetails(inPathSetting);
            partList.Add(itemPart);

            return new FakePartContext(partList);
        }
    }
}
