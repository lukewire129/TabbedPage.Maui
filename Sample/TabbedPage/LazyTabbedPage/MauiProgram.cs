﻿using MauiIcons.Material.Outlined;
using Microsoft.Extensions.Logging;
using TabbedPage.Maui;

namespace SampleLazyTabbedPage
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder ();
            builder
                .UseMauiApp<App> ()
                .UseLazyTabbedPage ()
                .UseMaterialOutlinedMauiIcons ()
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