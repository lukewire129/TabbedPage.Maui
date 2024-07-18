using TabbedPage.Maui.Themes;

namespace TabbedPage.Maui;

public static class BuilderExtentions
{
    public static MauiAppBuilder UseLazyTabbedPage(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers (handlers =>
        {
            var templateMAUIDictionary = new Generic ();
            if (!Application.Current.Resources.MergedDictionaries.Contains (templateMAUIDictionary))
                Application.Current.Resources.Add (templateMAUIDictionary);
        });
        return builder;
    }
}
