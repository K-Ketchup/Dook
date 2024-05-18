using CommunityToolkit.Maui;
using Dook.Services;
using Dook.ViewModel;
using Microsoft.Extensions.Logging;

namespace Dook
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                })
                .UseMauiMaps();

            //builder.Services.AddTransient<MainPage>();
            //builder.Services.AddTransient<ReviewService>();
            //builder.Services.AddTransient<DetailViewModel>();

        #if DEBUG
            builder.Logging.AddDebug();
        #endif

            return builder.Build();
        }
    }
}

