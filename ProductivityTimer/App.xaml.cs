using Microsoft.Extensions.DependencyInjection;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;

namespace ProductivityTimer
{
    public partial class App : Application
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
            base.OnStart();
            try
            {
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