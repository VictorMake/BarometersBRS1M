using System;
using System.Data.Objects;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M
{
    /// <summary>
    /// Контекст данных, содержащий данные для модели PartTracker
    /// </summary>
    public interface IPartContext : IDisposable
    {
        /// <summary>
        /// Получает группы устройств в контексте данных
        /// </summary>
        IObjectSet<Part> Parts { get; }

        ///// <summary>
        ///// Получает DeviceDetails в контексте данных
        ///// </summary>
        IObjectSet<DeviceDetail> DeviceDetails { get; }

        /// <summary>
        /// Сохранить все ожидающие изменения в контекст данных
        /// </summary>
        void Save();

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        ///       при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        T CreateObject<T>() where T : class;

        /// <summary>
        /// Проверяет, выполняется ли в этом контексте данных отслеживание предоставленного объекта
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <returns>Если объект отслеживается - true, в противном случае - false</returns>
        bool IsObjectTracked(object obj);
    }
}
