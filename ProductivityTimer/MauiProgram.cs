using Microsoft.Extensions.Logging;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;

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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
