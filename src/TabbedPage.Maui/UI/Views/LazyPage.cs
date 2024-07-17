using CommunityToolkit.Maui.Views;

namespace TabbedPage.Maui.UI.Views;

public interface ILazyPage
{
    ValueTask LoadViewAsync(CancellationToken token);
}
public class LazyPage<TView> : LazyView, ILazyPage where TView : View, new()
{
    public override async ValueTask LoadViewAsync(CancellationToken token)
    {
        await Task.Delay (300);
        // load the view
        Content = new TView { BindingContext = BindingContext };

        SetHasLazyViewLoaded (true);
    }
}
