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

            var app = builder.Build(); 
            var dbinitializer = app.Services.GetRequiredService<DatabaseInitializer>(); // gets the required services for the app 
            dbinitializer?.InitializeAsync().GetAwaiter().GetResult(); // runs the method to create tables
            return app;

        }
    }
}
