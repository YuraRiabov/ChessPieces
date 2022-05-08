using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Commands
{
    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public override bool CanExecute(object? parameter) => _canExecute((T)parameter);
        public override void Execute(object? parameter) => _execute((T)parameter);
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
    }
}
