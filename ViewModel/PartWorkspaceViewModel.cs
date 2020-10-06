using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BarometersBRS1M.Common;
using BarometersBRS1M.Helpers;
using BarometersBRS1M.ViewModel.Helpers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// Модель ViewModel для управления группами устройств
    /// </summary>
    public class PartWorkspaceViewModel : ViewModelBase
    {
        /// <summary>
        /// Группа устройств, выделенная в настоящее время в рабочей области
        /// </summary>
        private PartViewModel privateCurrentPart;

        /// <summary>
        /// UnitOfWork для управления изменениями
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса PartWorkspaceViewModel.
        /// </summary>
        /// <param name="parts">Управляемые группы устройств</param>
        /// <param name="unitOfWork">UnitOfWork для управления изменениями</param>
        public PartWorkspaceViewModel(ObservableCollection<PartViewModel> parts, IUnitOfWork unitOfWork)
        {
            if (parts == null) throw new ArgumentNullException(nameof(parts));

            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            //this.AllParts = parts;
            CurrentPart = parts.Count > 0 ? parts[0] : null;

            //// Реагирование на изменения вне этой модели ViewModel
            //this.AllParts.CollectionChanged += (sender, e) =>
            //{
            //    if (e.OldItems != null && e.OldItems.Contains(this.CurrentPart))
            //    {
            //        this.CurrentPart = null;
            //    }
            //};

            AddPartCommand = new DelegateCommand(o => AddPart());
            DeletePartCommand = new DelegateCommand(o => DeleteCurrentPart(), o => CurrentPart != null);
        }


        private ICommand privateAddPartCommand;
        /// <summary>
        /// Возвращает команду для добавления новой группы устройств
        /// </summary>
        public ICommand AddPartCommand
        {
            get { return privateAddPartCommand; }
            private set { privateAddPartCommand = value; }
        }

        private ICommand privateDeletePartCommand;
        /// <summary>
        /// Возвращает команду для удаления текущей группы устройств
        /// </summary>
        public ICommand DeletePartCommand
        {
            get { return privateDeletePartCommand; }
            private set { privateDeletePartCommand = value; }
        }

        //private ObservableCollection<PartViewModel> privateAllParts;
        ///// <summary>
        ///// Возвращает все группы устройств модели
        ///// </summary>
        //public ObservableCollection<PartViewModel> AllParts
        //{
        //    get { return privateAllParts; }
        //    private set { privateAllParts = value; }
        //}

        /// <summary>
        /// Возвращает или задает группу устройств, выделенной в настоящее время в рабочей области
        /// </summary>
        public PartViewModel CurrentPart
        {
            get { return privateCurrentPart; }
            set
            {
                privateCurrentPart = value;
                //OnPropertyChanged(nameof(CurrentPart));
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Обрабатывает добавление новой группы устройств в рабочее пространство и модель
        /// </summary>
        private void AddPart()
        {
            Part _part = unitOfWork.CreateObject<Part>();
            unitOfWork.AddPart(_part);

            // затем создать модель для части
            PartViewModel vm = new PartViewModel(_part, unitOfWork);
            //this.AllParts.Add(vm);
            CurrentPart = vm;
        }

        /// <summary>
        /// Это публичный фабричный метод.
        /// Добавил дополнительно.
        /// </summary>
        /// <returns></returns>
        public PartViewModel CreateNewPart()
        {
            Part _part = unitOfWork.CreateObject<Part>();
            unitOfWork.AddPart(_part);

            // затем создать модель для части
            //this.AllParts.Add(vm);
            PartViewModel vm = new PartViewModel(_part, unitOfWork);
            CurrentPart = vm;
            return vm;
        }

        /// <summary>
        /// Обрабатывает удаление текущей группы устройств
        /// </summary>
        private void DeleteCurrentPart()
        {
            unitOfWork.RemovePart(CurrentPart.Model);
            //this.AllParts.Remove(this.CurrentPart);
            CurrentPart = null;
        }
    }
}