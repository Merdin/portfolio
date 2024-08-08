using System;
using System.Windows.Input;

namespace PitchPerfectHearingTest.Commands
{
    public class ExecuteEventCommand : ICommand
    {
        private Action<object> _execute;

        public ExecuteEventCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public ExecuteEventCommand()
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
