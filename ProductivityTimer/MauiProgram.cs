using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;
using ProductivityTimer.Infrastructure.Repositories;
using ProductivityTimer.Views;
using ProductivityTimer.ViewModels;

namespace ProductivityTimer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<SQLIteConnectionFactory>();
            builder.Services.AddSingleton<DatabaseInitializer>();
            builder.Services.AddSingleton<IDailyHabitRepository, DailyHabitRepository>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
            builder.Services.AddSingleton<IWorkSessionRepository, WorkSessionRepository>();

            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
