using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProductivityTimer.ViewModels
{
    public class WorkPageViewModel
    {

        public WorkPageViewModel()
        {
            NavigateHomeCommand = new Command(async () => await GoToHomeAsync());
        }

        private async Task GoToHomeAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        public ICommand NavigateHomeCommand { get; }
    }
}
