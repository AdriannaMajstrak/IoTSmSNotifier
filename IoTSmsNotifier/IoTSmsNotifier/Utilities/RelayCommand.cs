using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoTSmsNotifier.Utilities
{
    public class RelayCommand : ICommand
    {
        Action methodNoParam;

        public RelayCommand(Action method)
        {
            this.methodNoParam = method;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            methodNoParam();
        }
    }
}
