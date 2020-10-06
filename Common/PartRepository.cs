using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace BarometersBRS1M.Common
{
    /// <summary>
    /// Репозиторий для получения данных о групп устройств из ObjectSet
    /// </summary>
    public class PartRepository : IPartRepository
    {
        /// <summary>
        /// Базовый ObjectSet, из которого извлекаются данные
        /// </summary>
        private IObjectSet<Part> objectSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса PartRepository.
        /// </summary>
        /// <param name="context">Контекст, из которого извлекаются данные</param>
        public PartRepository(IPartContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            objectSet = context.Parts;
        }

        /// <summary>
        /// Все группы устройств для текущего испытания
        /// </summary>
        /// <returns>Перечисляемый тип всех групп устройств</returns>  
        public IEnumerable<Part> GetAllParts()
        {
            // ПРИМЕЧАНИЕ. Некоторые моменты, рассмотренные в ходе реализации методов доступа к данным:
            //    -  ToList используется для обеспечения инициации любых исключений, связанных с доступом к данным
            //       во время выполнения этого метода, а не во время перечисления данных.
            //    - Возвращение IEnumerable вместо IQueryable обеспечивает полный контроль репозитория
            //       над получением данных из хранилища; возвращение IQueryable позволит пользователям
            //       добавлять дополнительные операторы и изменять запрос, отправляемый в хранилище.
            return objectSet.ToList();
        }
    }
}
