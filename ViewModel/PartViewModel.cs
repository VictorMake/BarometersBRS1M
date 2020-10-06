using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BarometersBRS1M.Common;
using BarometersBRS1M.SettingBarometers;
using BarometersBRS1M.ViewModel.Helpers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// Модель ViewModel отдельного <см. cref="Part"/>
    /// </summary>
    public class PartViewModel : BasicPartViewModel
    {

        /// <summary>
        /// Выбранные в настоящее время устройства
        /// </summary>
        private DeviceDetaillViewModel privateCurrentDeviceDetaill;

        /// <summary>
        /// UnitOfWork для управления изменениями
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса PartViewModel.
        /// </summary>
        /// <param name="part">Базовая группа устройств, на котором должна быть основана эта модель ViewModel</param>
        /// <param name="unitOfWork">UnitOfWork для управления изменениями</param>
        public PartViewModel(Part part, IUnitOfWork unitOfWork) : base(part)
        {
            if (part == null) throw new ArgumentNullException(nameof(part));

            this.unitOfWork = unitOfWork;

            // Построение структур данных для параметров
            DeviceDetaills = new ObservableCollection<DeviceDetaillViewModel>();

            foreach (DeviceDetail device in part.DeviceDetails)
            {
                DeviceDetaillViewModel vm = DeviceDetaillViewModel.BuildViewModel(device);
                if (vm != null) DeviceDetaills.Add(vm);
            }

            AddDeviceDetailCommand = new DelegateCommand(o => AddDeviceDetail<DeviceBRS1M>());
            DeleteDeviceDetailCommand = new DelegateCommand(o => DeleteCurrentDeviceDetail(), o => CurrentDeviceDetail != null);
        }

        private ICommand privateAddDeviceDetailCommand;
        /// <summary>
        /// Возвращает команду для добавления нового устройства
        /// </summary>
        public ICommand AddDeviceDetailCommand
        {
            get { return privateAddDeviceDetailCommand; }
            private set { privateAddDeviceDetailCommand = value; }
        }

        private ICommand privateDeleteDeviceDetailCommand;
        /// <summary>
        /// Возвращает команду для удаления текущего устройства
        /// </summary>
        public ICommand DeleteDeviceDetailCommand
        {
            get { return privateDeleteDeviceDetailCommand; }
            private set { privateDeleteDeviceDetailCommand = value; }
        }

        /// <summary>
        /// Возвращает или задает выбранное в настоящее время устройство
        /// </summary>
        public DeviceDetaillViewModel CurrentDeviceDetail
        {
            get { return privateCurrentDeviceDetaill; }
            set
            {
                privateCurrentDeviceDetaill = value;
                //OnPropertyChanged(nameof(CurrentDeviceDetail));
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DeviceDetaillViewModel> privateParameterDetails;
        /// <summary>
        /// Возвращает устройства для этой группа устройств
        /// </summary>
        public ObservableCollection<DeviceDetaillViewModel> DeviceDetaills
        {
            get { return privateParameterDetails; }
            private set { privateParameterDetails = value; }
        }

        /// <summary>
        /// Обрабатывает добавление новых устройств для этой группа устройств
        /// </summary>
        /// <typeparam name="T">Тип добавляемых устройств</typeparam>
        private void AddDeviceDetail<T>() where T : DeviceDetail
        {
            // вначале создать устройство
            DeviceDetail device = unitOfWork.CreateObject<T>();
            unitOfWork.AddParameterDetail(Model, device);

            // затем сконструировать модель просмотра
            DeviceDetaillViewModel vm = DeviceDetaillViewModel.BuildViewModel(device);
            DeviceDetaills.Add(vm);
            CurrentDeviceDetail = vm;
        }

        /// <summary>
        /// Обрабатывает удаление текущего устройства
        /// </summary>
        private void DeleteCurrentDeviceDetail()
        {
            unitOfWork.RemoveParameterDetail(Model, CurrentDeviceDetail.Model);
            DeviceDetaills.Remove(CurrentDeviceDetail);
            CurrentDeviceDetail = null;
        }
    }
}