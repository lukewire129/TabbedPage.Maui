﻿using Microsoft.Extensions.Logging;
using MauiIcons.Material.Outlined;
using TabbedPage.Maui;
namespace DemoApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder ();
            builder
                .UseMauiApp<App> ()
                .UseMaterialOutlinedMauiIcons()
                .UseLazyTabbedPage()
                .ConfigureFonts (fonts =>
                {
                    fonts.AddFont ("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont ("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build ();
        }
    }
}
