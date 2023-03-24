using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerLevel.Common
{
    internal class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<Object> DoExecute { get; set; }
        public Func<Object, bool> DoCanExcute { get; set; }

        public bool CanExecute(object parameter)
        {
            if (DoCanExcute == null)
            {
                return false;
            }
            return DoCanExcute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            this.DoExecute?.Invoke(parameter);
        }
    }
}
