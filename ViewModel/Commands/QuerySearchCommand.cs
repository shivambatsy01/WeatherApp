using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

//bind this command to run button -> commands will have executable functions from ViewModel.WeatherVM class
namespace WeatherApp.ViewModel.Commands
{
    public class QuerySearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public QuerySearchCommand(WeatherVM vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            if(string.IsNullOrWhiteSpace(query))
                    return false;
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
