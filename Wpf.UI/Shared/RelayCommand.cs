using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Wpf.UI.Shared
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        // This is the constructor that was missing (takes 1 or 2 arguments)
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        // Manual event implementation
        public event EventHandler? CanExecuteChanged;

        // Call this method whenever you want to refresh the Button's enabled state
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
