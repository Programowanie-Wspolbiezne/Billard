using System.Windows.Input;

namespace Presentation
{
    internal class Start(MainWindowVM viewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        readonly MainWindowVM vm = viewModel;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            vm.RestartGame();
        }
    }
}
