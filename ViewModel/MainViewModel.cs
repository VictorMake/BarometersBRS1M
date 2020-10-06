using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BarometersBRS1M.Helpers;
using BarometersBRS1M.Common;
using BarometersBRS1M.ViewModel.Helpers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// Модель ViewModel для доступа ко всем данным для модели
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// UnitOfWork для координации изменений
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса MainViewModel.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork для координации изменений</param>
        /// <param name="partRepository">Репозиторий для запросов данных о групп устройств</param>
        public MainViewModel(IUnitOfWork unitOfWork, IPartRepository partRepository)
        {
            if (partRepository == null) throw new ArgumentNullException(nameof(partRepository));

            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            // Построение структур данных для заполнения рабочего пространства
            ObservableCollection<PartViewModel> allParts = new ObservableCollection<PartViewModel>();

            foreach (Part part in partRepository.GetAllParts())
                allParts.Add(new PartViewModel(part, this.unitOfWork));

            PartWorkspace = new PartWorkspaceViewModel(allParts, unitOfWork);
            SaveCommand = new DelegateCommand(o => Save());
        }

        private ICommand privateSaveCommand;
        /// <summary>
        /// Возвращает команду сохранения всех изменений, выполненных в UnitOfWork текущих сеансов
        /// </summary>
        public ICommand SaveCommand
        {
            get { return privateSaveCommand; }
            private set { privateSaveCommand = value; }
        }

        private PartWorkspaceViewModel privatePartWorkspace;
        /// <summary>
        /// Возвращает рабочее пространство для управления группами устройств
        /// </summary>
        public PartWorkspaceViewModel PartWorkspace
        {
            get { return privatePartWorkspace; }
            private set { privatePartWorkspace = value; }
        }

        /// <summary>
        /// Сохраняет все изменения, выполненные в UnitOfWork текущих сеансов
        /// </summary>
        private void Save()
        { unitOfWork.Save(); }
    }
}
