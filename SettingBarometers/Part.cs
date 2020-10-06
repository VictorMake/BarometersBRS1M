using System;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using BarometersBRS1M.SettingBarometers;
using static BarometersBRS1M.GlobalUtility;

namespace BarometersBRS1M
{
    /// <summary>
    /// Обозначает все устройства с последовательными портами
    /// </summary>
    public class Part
    {
        /// <summary>
        /// Устройства, относящиеся к этой части испытаний
        /// </summary>
        private ICollection<DeviceDetail> devices;

        /// <summary>
        /// Инициализирует новый экземпляр класса Part.
        /// </summary>
        public Part()
        {
            // ПРИМЕЧАНИЕ. Привязка не требуется, так как это однонаправленный переход
            devices = new ObservableCollection<DeviceDetail>();
        }

        /// <summary>
        ///  Получает или задает, флаг, что коллекция устройств была изменена по колличеству и требует сохранения изменений
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDirty { get; set; }

        //<JsonIgnore>
        // вместо аттрибута <JsonIgnore> можно применить функцию оканчивающуюся на имя выключаемого свойства
        //Public Function ShouldSerializeIsDirty() As Boolean
        //    Return False
        //End Function

        /// <summary>
        /// Получает или задает устройства этого испытания
        /// Привязка не выполняется, так как это однонаправленный переход
        /// </summary>
        public virtual ICollection<DeviceDetail> DeviceDetails
        {
            get { return devices; }
            set { devices = value; }
        }

        /// <summary>
        /// Путь к файлу конфигурации устройств
        /// </summary>
        private string puthPartSetting;

        /// <summary>
        /// Заполнить коллекцию DeviceDetails дессериализуя класс PartDevices,
        /// содержащий словари измеренных, настроечных и расчётных параметров
        /// </summary>
        /// <param name="path">Путь к библиотеке расчётной части Dll</param>
        public virtual void PopulateDeviceDetails(string path)
        {
            // проверить FileName аргумент.
            if (path == null || path.Length == 0)
                throw new ArgumentNullException($"Проверка параметра файл для всех устройств, метод: {nameof(PopulateDeviceDetails)}, причина: отсутствует аргумент <{path}>");

            // проверить существование файла.
            FileInfo fInfo = new FileInfo(path);

            if (!fInfo.Exists)
            {
                // вызвать исключение при отсутствии   
                //Throw New FileNotFoundException($"Проверка файла для расчётной части: {Name}, метод: {NameOf(PopulateDeviceDetails)}, причина: файл <{path}> не найден")
                PartDevices emptyParameters = new PartDevices();
                // серелизация JSON в строку, а затем запись строки в файл
                File.WriteAllText(path, JsonConvert.SerializeObject(emptyParameters, Formatting.Indented));
            }
            else
            {
                PartDevices allBRS1Ms = JsonConvert.DeserializeObject<PartDevices>(File.ReadAllText(path));

                foreach (DeviceBRS1M itemBRS1M in allBRS1Ms.All_BRS_1M.Values)
                    devices.Add(itemBRS1M);
            }

            puthPartSetting = path;
        }

        /// <summary>
        ///  Сохранить все устройства в конфигурации
        /// </summary>
        public virtual void SaveDeviceDetails()
        {
            Dictionary<string, DeviceBRS1M> BRS1Ms = new Dictionary<string, DeviceBRS1M>();
            bool needToSave = false;

            // просмотреть список параметров и добавить в специализированные коллекции
            foreach (DeviceDetail device in devices)
            {
                if (device.IsDirty)
                {
                    needToSave = true;
                    break;
                }
            }

            if (IsDirty || needToSave)
            {
                // просмотреть список параметров и добавить в специализированные коллекции
                // если имя параметра было оставлено пользователем по умолчанию,
                // то его во избежание коллизии уникальности переназначить имя как Guid
                foreach (DeviceDetail device in devices)
                {
                    DeviceBRS1M tuningParameter = device as DeviceBRS1M;
                    if (tuningParameter != null)
                    {
                        if (tuningParameter.ParameterName == USER_TEXT_CONTROL_BAROMETER || BRS1Ms.ContainsKey(tuningParameter.ParameterName))
                            tuningParameter.ParameterName = Guid.NewGuid().ToString();

                        BRS1Ms.Add(tuningParameter.ParameterName, tuningParameter);
                    }
                }

                PartDevices allBRS1Ms = new PartDevices(BRS1Ms);
                // серелизация JSON в строку, а затем запись строки в файл
                File.WriteAllText(puthPartSetting, JsonConvert.SerializeObject(allBRS1Ms, Formatting.Indented));

                // сбросить флаг IsDirty после сохранения изменений
                IsDirty = false;
                foreach (DeviceDetail device in devices)
                    device.IsDirty = false;
            }
        }
    }
}
