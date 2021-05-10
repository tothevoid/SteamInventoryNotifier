using System;
using System.Windows.Input;

namespace SteamInventoryNotifier.Model
{
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> action;

        public BaseCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
