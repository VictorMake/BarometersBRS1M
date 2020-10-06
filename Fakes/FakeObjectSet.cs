using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace BarometersBRS1M
{
    /// <summary>
    /// Реализация IObjectSet на основании данных в памяти
    /// </summary>
    /// <typeparam name="TEntity">Тип данных, сохраняемых в наборе</typeparam>
    public class FakeObjectSet<TEntity> : IObjectSet<TEntity> where TEntity : class
    {
        /// <summary>
        /// Базовые данные этого набора
        /// </summary>
        private HashSet<TEntity> data;

        /// <summary>
        /// Версия IQueryable базовых данных
        /// </summary>
        private IQueryable query;

        /// <summary>
        /// Инициализирует новый экземпляр класса FakeObjectSet.
        /// Экземпляр не содержит данных.
        /// </summary>
        public FakeObjectSet()
        {
            this.data = new HashSet<TEntity>();
            this.query = this.data.AsQueryable();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса FakeObjectSet.
        /// Экземпляр содержит предоставленные сведения о данных.
        /// </summary>
        /// <param name="inData">Данные, включаемые в набор</param>
        public FakeObjectSet(IEnumerable<TEntity> inData)
        {
            if (inData == null)
                throw new ArgumentNullException(nameof(inData));

            this.data = new HashSet<TEntity>(inData);
            this.query = this.data.AsQueryable();
        }

        /// <summary>
        /// Implements
        /// Возвращает тип элементов в этом наборе
        /// </summary>
        Type IQueryable.ElementType => this.query.ElementType;

        /// <summary>
        /// Implements
        /// Возвращает дерево выражений для этого набора
        /// </summary>
        private Expression IQueryable_Expression
        { get { return this.query.Expression; } }

        Expression IQueryable.Expression
        { get { return IQueryable_Expression; } }

        /// <summary>
        /// Implements
        /// Возвращает поставщика запросов для этого набора
        /// </summary>
        IQueryProvider IQueryable.Provider
        { get { return this.query.Provider; } }

        /// <summary>
        /// Implements
        /// Добавляет новый элемент в этот набор
        /// </summary>
        /// <param name="entity">Добавляемый элемент</param>
        public void AddObject(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.data.Add(entity);
        }

        /// <summary>
        /// Implements
        /// Удаляет новый элемент из этого набора
        /// </summary>
        /// <param name="entity">Удаляемый элемент</param>
        public void DeleteObject(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.data.Remove(entity);
        }

        /// <summary>
        /// Implements
        /// Прикрепляет новый элемент к этому набору
        /// </summary>
        /// <param name="entity">Прикрепляемый элемент</param>
        public void Attach(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.data.Add(entity);
        }

        /// <summary>
        /// Implements
        /// Отделяет новый элемент от этого набора
        /// </summary>
        /// <param name="entity">Отделяемый элемент</param>
        public void Detach(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.data.Remove(entity);
        }

        /// <summary>
        /// Implements
        /// Возвращает типизированный перечислитель для этого набора
        /// </summary>
        /// <returns>Возвращает перечислитель для всех элементов в этом наборе</returns>
        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        { return this.data.GetEnumerator(); }

        /// <summary>
        /// Implements
        /// Возвращает типизированный перечислитель для этого набора
        /// </summary>
        /// <returns>Возвращает перечислитель для всех элементов в этом наборе</returns>
        IEnumerator IEnumerable.GetEnumerator()
        { return this.data.GetEnumerator(); }
    }
}
