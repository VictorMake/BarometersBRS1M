using System;
using BarometersBRS1M.Helpers;
using BarometersBRS1M.SettingBarometers;

namespace BarometersBRS1M.ViewModel
{
    /// <summary>
    /// Общие функциональные возможности для моделей ViewModel отдельных данных DeviceDetaill
    /// От него наследуется устройство DeviceBRS1MViewModel.
    /// </summary>
    public abstract class DeviceDetaillViewModel : ViewModelBase
    {
        /// <summary>
        /// Возвращает базовые данные DeviceDetail, на которых основана эта модель ViewModel
        /// </summary>
        public abstract DeviceDetail Model { get; }

        /// <summary>
        /// Конструирует модель просмотра для представления предоставленных данных DeviceDetaill
        /// </summary>
        /// <param name="device">Данные, для которых должны быть сконструирована модель ViewModel</param>
        /// <returns>Сконструированная модель ViewModel или null, если она не может быть построена</returns>
        public static DeviceDetaillViewModel BuildViewModel(DeviceDetail device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            DeviceBRS1M tuning = device as DeviceBRS1M;
            if (tuning != null) return new DeviceBRS1MViewModel(tuning);

            return null;
        }
    }
}
