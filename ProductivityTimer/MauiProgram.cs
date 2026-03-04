using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;
using ProductivityTimer.Infrastructure.Repositories;

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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
