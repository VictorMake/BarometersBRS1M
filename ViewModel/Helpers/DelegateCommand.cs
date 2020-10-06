using System;
using System.Windows.Input;

namespace BarometersBRS1M.ViewModel.Helpers
{
    /// <summary>
    /// Реализация ICommand, основанная на делегатах
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Действие, которое должно быть выполнено при выполнении этой команды
        /// </summary>
        private Action<object> executionAction;

        /// <summary>
        /// Предикат, который должен быть определен, если команда допустима для выполнения
        /// </summary>
        private Predicate<object> canExecutePredicate;

        /// <summary>
        /// Инициализирует новый экземпляр класса DelegateCommand.
        /// Команда будет всегда допустима для выполнения.
        /// </summary>
        /// <param name="execute">Делегат, вызываемый при выполнении</param>
        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса DelegateCommand.
        /// </summary>
        /// <param name="execute">Делегат, вызываемый при выполнении</param>
        /// <param name="canExecute">Предикат, который должен быть определен, если команда допустима для выполнения</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            executionAction = execute ?? throw new ArgumentNullException(nameof(execute));
            canExecutePredicate = canExecute;
        }

        /// <summary>
        /// Инициируется, если изменяется CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Выполняет делегата, резервирующего эту команду DelegateCommand
        /// </summary>
        /// <param name="parameter">параметр, передаваемый в предикат</param>
        /// <returns>Значение true, если команда допустима для выполнения</returns>
        public bool CanExecute(object parameter) => canExecutePredicate == null ? true : canExecutePredicate(parameter);

        /// <summary>
        /// Выполняет делегата, резервирующего эту команду DelegateCommand
        /// </summary>
        /// <param name="parameter">параметр, передаваемый для делегата</param>
        /// <exception cref="InvalidOperationException">Инициируется, если CanExecute возвращает значение false</exception>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                throw new InvalidOperationException($"Команда не корректна для исполнения, при проверке метода {nameof(CanExecute)} перед попыткой выполнить.");

            executionAction(parameter);
        }
    }
}
