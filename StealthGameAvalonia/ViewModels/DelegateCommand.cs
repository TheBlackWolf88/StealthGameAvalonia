using System;
using System.Windows.Input;

namespace StealthGameAvalonia.ViewModels
{
    public class DelegateCommand(DelegateCommand.ActionDelegate action, DelegateCommand.PredicateDelegate? canExecute = null) : ICommand
    {
        public delegate bool PredicateDelegate(object? obj);
        public delegate void ActionDelegate(object? obj);

        private readonly ActionDelegate _action = action ?? throw new ArgumentNullException("action");
        private readonly PredicateDelegate? _canExecute = canExecute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter))
            {
                _action(parameter);
            }
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
