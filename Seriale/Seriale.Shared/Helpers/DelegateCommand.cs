using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Seriale.Helpers
{
    public class DelegateCommand<T> : ICommand
    {
        readonly Action<T> callback;

        public DelegateCommand(Action<T> callback)
        {
            this.callback = callback;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (callback != null) { callback((T)parameter); }
        }
    }
}
