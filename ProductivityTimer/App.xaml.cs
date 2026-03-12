using Microsoft.Extensions.DependencyInjection;
using ProductivityTimer.Infrastructure.Data;

namespace ProductivityTimer
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly DatabaseInitializer _databaseInitializer;
        public App(DatabaseInitializer databaseInitializer)
        {
            InitializeComponent();
            _databaseInitializer = databaseInitializer;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync("//MainPage");

            base.OnStart();
            try
            { // setting up database tables at startup
                await _databaseInitializer.InitializeAsync();
            }
            catch (Exception ex)
            {
                if (App.Current?.MainPage is not null)
                {
                    await App.Current.MainPage.DisplayAlertAsync("Database Error", ex.Message, "OK");
                }
            }
        }
    }
}