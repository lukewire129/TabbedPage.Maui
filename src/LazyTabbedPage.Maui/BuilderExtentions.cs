using CommunityToolkit.Maui;
using LazyTabbedPage.Maui.Themes;

namespace LazyTabbedPage.Maui;

public static class BuilderExtentions
{
    public static MauiAppBuilder UseLazyTabbedPage(this MauiAppBuilder builder)
    {
        builder.UseMauiCommunityToolkit ()
               .ConfigureMauiHandlers (handlers =>
        {
            var templateMAUIDictionary = new Generic ();
            if (!Application.Current.Resources.MergedDictionaries.Contains (templateMAUIDictionary))
                Application.Current.Resources.Add (templateMAUIDictionary);
        });
        return builder;
    }
}
