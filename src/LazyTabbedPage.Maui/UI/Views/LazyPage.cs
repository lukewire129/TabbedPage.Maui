using CommunityToolkit.Maui.Views;

namespace LazyTabbedPage.Maui.UI.Views;

public interface ILazyPage
{
    ValueTask LoadViewAsync(CancellationToken token);
}
public class LazyPage<TView> : LazyView, ILazyPage where TView : View, new()
{
    public override async ValueTask LoadViewAsync(CancellationToken token)
    {
        // simulate a long running task
        await Task.Delay (3000, token);

        // load the view
        Content = new TView { BindingContext = BindingContext };

        SetHasLazyViewLoaded (true);
    }
}
