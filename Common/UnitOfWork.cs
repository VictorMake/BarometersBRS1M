using System;
using System.Globalization;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M.Common
{
    /// <summary>
    /// Инкапсулирует изменения в базовых данных, хранящихся в PartContext
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Изменения в отслеживании базового нижележащего контекста
        /// </summary>
        private IPartContext underlyingContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса UnitOfWork.
        /// Изменения, зарегистрированные в UnitOfWork, записываются в предоставленном контексте
        /// </summary>
        /// <param name="context">Базовый контекст для данного UnitOfWork</param>
        public UnitOfWork(IPartContext context)
        { underlyingContext = context ?? throw new ArgumentNullException(nameof(context)); }

        /// <summary>
        /// Сохранить все ожидающие изменения в этот UnitOfWork
        /// </summary>
        public void Save() { underlyingContext.Save(); }

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        ///       при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        public T CreateObject<T>() where T : class
        { return underlyingContext.CreateObject<T>(); }

        /// <summary>
        /// Регистрирует добавление новой группы устройств
        /// </summary>
        /// <param name="part">Добавляемая группа устройств</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств уже добавлена в UnitOfWork</exception>
        public void AddPart(Part part)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            CheckEntityDoesNotBelongToUnitOfWork(part);
            underlyingContext.Parts.AddObject(part);
        }

        /// <summary>
        /// Регистрирует добавление новых устройств
        /// </summary>
        /// <param name="part">Группа устройств, для которого добавляются параметр</param>
        /// <param name="device">Добавляемое устройство</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если устройство уже добавлен в UnitOfWork</exception>
        public void AddParameterDetail(Part part, DeviceDetail device)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            if (device == null)
                throw new ArgumentNullException(nameof(device));

            CheckEntityDoesNotBelongToUnitOfWork(device);
            CheckEntityBelongsToUnitOfWork(part);

            underlyingContext.DeviceDetails.AddObject(device);
            part.DeviceDetails.Add(device);
            part.IsDirty = true;
        }

        /// <summary>
        /// Регистрирует удаление существующей группы устройств
        /// </summary>
        /// <param name="part">Удаляемая группа устройств</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств не отслеживается этим UnitOfWork</exception>
        public void RemovePart(Part part)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            CheckEntityBelongsToUnitOfWork(part);
            underlyingContext.Parts.DeleteObject(part);
        }

        /// <summary>
        /// Регистрирует удаление существующих параметров
        /// </summary>
        /// <param name="part">группа устройств, чьи параметры удаляются</param>
        /// <param name="device">Удаляемое устройство</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если устройства не отслеживаются этим UnitOfWork</exception>
        public void RemoveParameterDetail(Part part, DeviceDetail device)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            if (device == null)
                throw new ArgumentNullException(nameof(device));

            CheckEntityBelongsToUnitOfWork(device);
            CheckEntityBelongsToUnitOfWork(part);
            if (!part.DeviceDetails.Contains(device))
                throw new InvalidOperationException("Представленный DeviceDetail не относятся к поставленной группе устройств.");

            part.DeviceDetails.Remove(device);
            underlyingContext.DeviceDetails.DeleteObject(device);
            part.IsDirty = true;
        }

        /// <summary>
        /// Подтверждает, что указанная сущность отслеживается в этом UnitOfWork
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <exception cref="InvalidOperationException">Инициируется, если объект не отслеживается этим UnitOfWork</exception>
        private void CheckEntityBelongsToUnitOfWork(object entity)
        {
            if (!underlyingContext.IsObjectTracked(entity))
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Представленный {0} не является частью этого Блока Работы", entity.GetType().Name));
        }

        /// <summary>
        /// Подтверждает, что указанная сущность не отслеживается в этом UnitOfWork
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <exception cref="InvalidOperationException">Инициируется, если объект отслеживается этим UnitOfWork</exception>
        private void CheckEntityDoesNotBelongToUnitOfWork(object entity)
        {
            if (underlyingContext.IsObjectTracked(entity))
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Представленный {0} уже является частью этого Блока Работы.", entity.GetType().Name));
        }
    }
}
