using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArmyPlanner
{
    public class RelayCommand(Action action) : ICommand
    {
        private readonly Action _action = action ?? throw new ArgumentNullException(nameof(action));

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _action();
    }
}
