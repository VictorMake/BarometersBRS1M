using System;
using BarometersBRS1M.Helpers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// ViewModel отдельной группы устройств без связей
    /// Объект PartViewModel должен использоваться, если должны отображаться или изменяться связи
    /// </summary>
    public class BasicPartViewModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса BasicPartViewModel.
        /// </summary>
        /// <param name="part">Базовая группа устройств, на котором должна быть основана эта модель ViewModel</param>
        public BasicPartViewModel(Part part)
        { this.Model = part ?? throw new ArgumentNullException(nameof(part)); }

        private Part privateModel;
        /// <summary>
        /// Возвращает базовую группа устройств, на котором основана эта модель ViewModel
        /// </summary>
        public Part Model
        {
            get { return privateModel; }
            private set { privateModel = value; }
        }

        ///// <summary>
        ///// Возвращает или задает имя этой группы устройств
        ///// </summary>
        //public string PartName
        //{
        //    get { return this.Model.Name; }

        //    set
        //    {
        //        this.Model.Name = value;
        //        this.OnPropertyChanged(NameOf(this.PartName));
        //    }
        //}

        ///// <summary>
        ///// Возвращает или задает дополнительное описание этой группы устройств
        ///// </summary>
        //public string Description
        //{
        //    get { return this.Model.Description; }

        //    set
        //    {
        //        this.Model.Description = value;
        //        this.OnPropertyChanged(NameOf(this.Description));
        //    }
        //}


        ///// <summary>
        ///// При ссылке на эту группа устройств возвращает отображаемый текст
        ///// </summary>
        //public string DisplayName
        //{
        //    get { return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.Model.Name, this.Model.Description); }
        //}
    }
}
