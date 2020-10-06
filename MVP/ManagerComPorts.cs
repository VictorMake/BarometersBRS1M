using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace BarometersBRS1M
{
    /// <summary>
    /// Предоставляет сведения обо всех портах в системе всем терминалам
    /// </summary>
    public class ManagerComPorts
    {
        private FormComBarometers mFormBarometers;

        public ManagerComPorts(FormComBarometers inFormComBarometers)
        { mFormBarometers = inFormComBarometers; }

        /// <summary>
        /// Для конкретного терминала обновить его ComboBox список доступных портов.
        /// </summary>
        /// <param name="cmbPortName"></param>
        /// <param name="portOpen"></param>
        public void RefreshComPortList(ComboBox cmbPortName, bool portOpen)
        {
            // Если порты найдены, то программа выдаст сообщение
            if (IsPortOpen())
            {
                mFormBarometers.Log(LogMsgType.Outgoing, $"Сканирование портов{Environment.NewLine}Найдены открытые порты.{Environment.NewLine}");

                // Определить изменился ли списое имен портов с последней проверки
                string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, portOpen);

                // Если было какое либо обновление, то обновить контрол показывая пользователю лист имён портов
                if (!String.IsNullOrEmpty(selected))
                {
                    cmbPortName.Items.Clear();
                    cmbPortName.Items.AddRange(OrderedPortNames());
                    cmbPortName.SelectedItem = selected;
                }
            }
            else
            {
                cmbPortName.Items.Clear();
                mFormBarometers.Log(LogMsgType.Warning, $"Сканирование портов{Environment.NewLine}Открытые порты не найдены!{Environment.NewLine}");
            }
        }

        /// <summary>
        /// Проверить обновился список с прошлого раза и в зависимости предыдущего состояния открытости 
        /// выбрать из прежнего или списка новых добавленных портов.
        /// </summary>
        /// <param name="previousPortNames"></param>
        /// <param name="currentSelection"></param>
        /// <param name="portOpen"></param>
        /// <returns></returns>
        public string RefreshComPortList(IEnumerable<string> previousPortNames, string currentSelection, bool portOpen)
        {
            // Создать новый возвращаемый отчёт для заполнения
            string selected = null;
            // Перезаписать лист портов в смонтированных в текущий момент в операционной системе (отсортированных по имени)
            string[] ports = SerialPort.GetPortNames();
            // Вначале определить если были изменения (любые добавления или удаления)
            bool updated = previousPortNames.Except(ports).Count() > 0 || ports.Except(previousPortNames).Count() > 0;

            // Если были изменения, тогда выделить подходящий по умолчанию порт
            if (updated)
            {
                // Использовать корректный отсортированный набор имён портов
                ports = OrderedPortNames();
                // Найти новые порты если один или более были добавлены
                string newest = SerialPort.GetPortNames().Except(previousPortNames).OrderBy(a => a).LastOrDefault();

                // Если порт был уже открыт... (смотреть примечание логики и причин в Notes.txt)
                if (portOpen)
                {
                    if (ports.Contains(currentSelection)) selected = currentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(currentSelection)) selected = currentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // Если были изменения в списке портов, возвратить рекомендованный выделенный по умолчанию
            return selected;
        }

        /// <summary>
        /// Список портов в отсортированном порядке.
        /// </summary>
        /// <returns></returns>
        private string[] OrderedPortNames()
        {
            // Просто вернуть удачные распарсерные строки в целые значения
            // Сортировать имена serial port по числовому значению (если возможно)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out int num) ? num : 0).ToArray();
        }

        bool chekedIsPortOpenСomplete = false;// проверка совершена
        bool available = false;

        /// <summary>
        /// Имеется ли хотя бы один открытый порт?
        /// </summary>
        /// <returns></returns>
        public bool IsPortOpen()
        {
            if (chekedIsPortOpenСomplete) return available;

            //List<String> portListNames = new List<String>();
            // Создаем переменную для возврата состояния получения портов.
            // true в случае успешного получения 
            // false если порты не найдены.
            //bool available = false;

            // Представляет ресурс последовательного порта.
            SerialPort port = default(SerialPort);

            // Выполняем проход по массиву имен последовательных портов для текущего компьютера
            // которые возвращает функция SerialPort.GetPortNames().
            foreach (string portName in SerialPort.GetPortNames())
            {
                // В результате этого вызова при обработке массива в переменной «str» будет содержаться имя COM-порта локальной машины. 
                // Перед проверкой конкретного COM-порта нужно сначала создать объект класса SerialPort с указанием имени порта.
                // Для проверки состояния порта, необходимо воспользоваться методом «SerialPort.Open», данный метод открывает новое соединение последовательного порта.
                // После открытия нового соединения необходимо воспользоваться свойством «SerialPort.IsOpen», возвращающее значение,
                // указывающее состояние объекта SerialPort — открыт или закрыт. После проверки состояния необходимо закрыть соединение, вызвав метод «Close».
                try
                {
                    port = new SerialPort(portName);
                    // Открываем новое соединение последовательного порта.
                    port.Open();

                    // Выполняем проверку полученного порта true, если последовательный порт открыт, в противном случае — false.
                    // Значение по умолчанию — false.
                    if (port.IsOpen)
                    {
                        // Если порт открыт то добавляем его в listBox
                        // tbComPort.Items.Add(str)
                        // tbComPort.Text = tbComPort.Text + portName + Environment.NewLine;
                        //portListNames.Add(portName);

                        // Уничтожаем внутренний объект System.IO.Stream.
                        port.Close();

                        // возвращаем состояние получения портов
                        available = true;
                    }
                    // Ловим все ошибки и отображаем, что открытых портов не найдено               
                }
                catch (Exception ex)
                {
                    // случае если COM-порт уже используется другим процессом, то при попытке его открыть вы получите исключение: 
                    // Невозможно открыть com порт. 
                    // Чтобы определить, кто именно держит порт, нужно воспользоваться утилитой для мониторинга ресурсов процессов.
                    mFormBarometers.Log(LogMsgType.Error, $"Ошибка при сканировании!{Environment.NewLine}{ex.Message}{Environment.NewLine}");
                }
            }

            chekedIsPortOpenСomplete = true;
            // portNames = portListNames.ToArray();
            // Array.Sort(portNames);
            // возвращаем состояние получения портов
            return available;
        }
    }
}
