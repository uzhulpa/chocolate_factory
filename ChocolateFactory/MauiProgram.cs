using ChocolateFactory.Data;
using ChocolateFactory.Pages;
using ChocolateFactory.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ChocolateFactory
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
                    fonts.AddFont("Nunito-Regular.ttf", "NunitoRegular");
                    fonts.AddFont("Nunito-Bold.ttf", "NunitoBold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<XmlDatabaseManager>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<GiftsViewModel>()
                .AddSingleton<GiftsPage>()
                .AddTransient<ManageMenuItemsViewModel>()
                .AddTransient<ManageMenuItemPage>();
            return builder.Build();
        }
    }
}
