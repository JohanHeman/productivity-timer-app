using Microsoft.Extensions.DependencyInjection;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;

namespace ProductivityTimer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }


    }
}