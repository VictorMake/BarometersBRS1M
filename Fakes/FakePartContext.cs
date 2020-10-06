using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M
{
    /// <summary>
    /// Реализация IPartContext в памяти
    /// </summary>
    public class FakePartContext : IPartContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса FakePartContext.
        /// Контекст содержит пустые исходные данные.
        /// </summary>
        public FakePartContext()
        {
            privateParts = new FakeObjectSet<Part>();
            privateDeviceDetails = new FakeObjectSet<DeviceDetail>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса FakePartContext.
        /// Контекст содержит предоставленные исходные данные.
        /// </summary>
        /// <param name="parts">группы устройств, включаемые в контекст</param>
        public FakePartContext(IEnumerable<Part> parts)
        {
            if (parts == null)
                throw new ArgumentNullException(nameof(parts));

            privateParts = new FakeObjectSet<Part>(parts);

            // Получить устройства, производные от предоставленных данных о групп устройств
            privateDeviceDetails = new FakeObjectSet<DeviceDetail>();
            foreach (Part part in parts)
                foreach (DeviceDetail det in part.DeviceDetails)
                    privateDeviceDetails.AddObject(det);
        }

        /// <summary>
        /// Инициируется при вызове ()
        /// </summary>
        public event EventHandler<EventArgs> SaveCalledEvent;

        /// <summary>
        /// Инициируется при вызове ()
        /// </summary>
        public event EventHandler<EventArgs> DisposeCalledEvent;

        /// <summary>
        /// Возвращает все части групп устройств (хотя она одна), отслеживаемых в этом контексте
        /// </summary>
        private IObjectSet<Part> privateParts;

        public IObjectSet<Part> Parts
        { get { return privateParts; } }

        /// <summary>
        /// Возвращает все устройства, отслеживаемые в этом контексте
        /// </summary>
        private IObjectSet<DeviceDetail> privateDeviceDetails;
        public IObjectSet<DeviceDetail> DeviceDetails
        { get { return privateDeviceDetails; } }

        /// <summary>
        /// Сохранить все отложенные изменения в этом контексте
        /// </summary>
        public void Save()
        {
            SavePartParameterDetails();
            OnSaveCalled(EventArgs.Empty);
        }

        /// <summary>
        /// Освободить все ресурсы, используемые в этом контексте
        /// </summary>
        public void Dispose()
        { OnDisposeCalled(EventArgs.Empty); }

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        ///       при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        public T CreateObject<T>() where T : class
        { return Activator.CreateInstance<T>(); }

        /// <summary>
        /// Проверяет, выполняется ли в этом контексте отслеживание указанного объекта
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <returns>Если объект отслеживается - true, в противном случае - false</returns>
        public bool IsObjectTracked(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            bool contained = false;

            if (entity is Part)
                contained = Parts.Contains((Part)entity);
            else if (entity is DeviceDetail)
                contained = DeviceDetails.Contains((DeviceDetail)entity);

            return contained;
        }

        /// <summary>
        /// Вызывает событие SaveCalled
        /// </summary>
        /// <param name="e">Аргументы для события</param>
        private void OnSaveCalled(EventArgs e)
        { SaveCalledEvent?.Invoke(this, e); }

        /// <summary>
        /// Вызывает событие DisposeCalled
        /// </summary>
        /// <param name="e">Аргументы для события</param>
        private void OnDisposeCalled(EventArgs e)
        { DisposeCalledEvent?.Invoke(this, e); }

        /// <summary>
        /// Сохранить все расчётные части
        /// </summary>
        private void SavePartParameterDetails()
        { foreach (Part part in Parts) part.SaveDeviceDetails(); }

    }
}
