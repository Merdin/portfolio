using System;
using System.Windows.Input;

namespace PitchPerfectHearingTest.Commands
{
    public class ChangeViewCommand : ICommand
    {
        private Action<object> _execute;

        public ChangeViewCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public ChangeViewCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}


