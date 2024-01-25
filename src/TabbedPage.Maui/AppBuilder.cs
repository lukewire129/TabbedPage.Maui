using TabbedPage.Maui.Themes;

namespace TabbedPage.Maui;

public static class AppBuilder
{
    public static MauiAppBuilder UseCustomTabbedPage(this MauiAppBuilder builder)
    {
        var templateMAUIDictionary = new Generic ();
        if (!Application.Current.Resources.MergedDictionaries.Contains (templateMAUIDictionary))
            Application.Current.Resources.Add (templateMAUIDictionary);
        return builder;
    }
}
