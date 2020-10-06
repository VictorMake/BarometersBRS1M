using System.Collections.Generic;

namespace BarometersBRS1M.Common
{
    /// <summary>
    /// Репозиторий для получения данных о группы устройств
    /// </summary>
    public interface IPartRepository
    {
        /// <summary>
        /// Все группы устройств текущего испытания
        /// </summary>
        /// <returns>Перечисляемый тип всех групп устройств</returns>  
        IEnumerable<Part> GetAllParts();
    }
}
