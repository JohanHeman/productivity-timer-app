using Microsoft.Extensions.Logging;
using ProductivityTimer.Domain.Interfaces;
using ProductivityTimer.Infrastructure.Data;
using ProductivityTimer.Infrastructure.Services;
using ProductivityTimer.Infrastructure.Repositories;
using ProductivityTimer.Views;
using ProductivityTimer.ViewModels;
using Microsoft.Extensions.Configuration;
using ProductivityTimer.Application.Services;
using ProductivityTimer.Application.Interfaces;
using ProductivityTimer.Application.Facade;
using Plugin.Maui.Audio;
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

            builder.Services.AddSingleton(AudioManager.Current); // use the existing audio manager
            builder.Services.AddSingleton<DatabaseInitializer>();
            builder.Services.AddSingleton<IDailyHabitRepository, DailyHabitRepository>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
            builder.Services.AddSingleton<IWorkSessionRepository, WorkSessionRepository>();
            builder.Services.AddSingleton<IDailyHabitService, DailyHabitService>();


            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<WorkPageViewModel>();
            builder.Services.AddTransient<WorkPage>();
            builder.Services.AddTransient<HistoryPageViewModel>();
            builder.Services.AddTransient<ITimerService, TimerService>();
            builder.Services.AddTransient<IWorkFacade, WorkFacade>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<QuoteApplicationService>();
            builder.Services.AddTransient<IStatisticService, StatisticService>();
            builder.Services.AddTransient<DailyHabitPageViewModel>();
            builder.Services.AddTransient<DailyHabitPage>();


            // set up the httpclient for injection at Infrastructure/Services/QuoteAPIService
            builder.Services.AddHttpClient<IQuoteService, QuoteService>(client =>
            {
                client.BaseAddress = new Uri("https://api.api-ninjas.com/");
                var config = new ConfigurationBuilder().AddUserSecrets<App>().Build();
                client.DefaultRequestHeaders.Add("X-Api-Key", config["ApiNinjas:ApiKey"]);
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();

        }
    }
}
