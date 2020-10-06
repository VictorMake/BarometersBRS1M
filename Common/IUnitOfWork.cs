using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M.Common
{
    /// <summary>
    /// Инкапсулирует изменения в базовых данных
    /// Сервисный класс
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохранить все ожидающие изменения в этот UnitOfWork
        /// </summary>
        void Save();

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        /// при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        T CreateObject<T>() where T : class;

        /// <summary>
        /// Регистрирует добавление новой группы устройств
        /// </summary>
        /// <param name="part">Добавляемая группа устройств</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств уже добавлена в UnitOfWork</exception>
        void AddPart(Part part);

        /// <summary>
        /// Регистрирует добавление новых параметров
        /// </summary>
        /// <param name="part">группы устройств, для которого добавляются параметры</param>
        /// <param name="device">Добавляемые устройства</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группы устройств не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если параметры уже добавлены в UnitOfWork</exception>
        void AddParameterDetail(Part part, DeviceDetail device);

        /// <summary>
        /// Регистрирует удаление существующую группа устройств
        /// </summary>
        /// <param name="part">Удаляемая группа устройств</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группа устройств не отслеживается этим UnitOfWork</exception>
        void RemovePart(Part part);

        /// <summary>
        /// Регистрирует удаление существующих параметров
        /// </summary>
        /// <param name="part">группы устройств, чьи параметры удаляются</param>
        /// <param name="device">Удаляемые устройства</param>
        /// <exception cref="InvalidOperationException">Инициируется, если группы устройств не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если параметры не отслеживаются этим UnitOfWork</exception>
        void RemoveParameterDetail(Part part, DeviceDetail device);
    }
}
