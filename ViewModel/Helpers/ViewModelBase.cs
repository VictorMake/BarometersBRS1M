using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BarometersBRS1M.Helpers
{
    /// <summary>
    /// Абстрактная база консолидации общих функциональных возможностей всех моделей ViewModel
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Инициируется, если свойство этого объекта имеет новое значение
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Инициирует это событие PropertyChanged моделей ViewModel
        /// </summary>
        /// <param name="propertyName">Имя свойства, имеющего новое значение</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        { this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName)); }

        /// <summary>
        /// Инициирует это событие PropertyChanged моделей ViewModel
        /// </summary>
        /// <param name="e">Аргументы, детализирующие изменение</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        { this.PropertyChanged?.Invoke(this, e); }
    }
}
