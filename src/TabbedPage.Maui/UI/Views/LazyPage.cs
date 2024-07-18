namespace TabbedPage.Maui.UI.Views;

public interface ILazyPage
{
    ValueTask LoadViewAsync(CancellationToken token);
}
public class LazyPage<TView> : ZLazyView, ILazyPage where TView : View, new()
{
    public override async ValueTask LoadViewAsync(CancellationToken token)
    {
        await Task.Delay (300);
        // load the view
        Content = new TView { BindingContext = BindingContext };
    }
}

public abstract class ZLazyView : ContentView 
{ 

    /// <summary>
    /// Use this method to do the initialization of the <see cref="View"/> and change the status HasViewLoaded value here.
    /// </summary>
    /// <returns><see cref="ValueTask"/></returns>
    public abstract ValueTask LoadViewAsync(CancellationToken token = default);

    /// <inheritdoc/>
    protected override void OnBindingContextChanged()
    {
        if (Content is not null && Content is not ActivityIndicator)
        {
            Content.BindingContext = BindingContext;
        }
    }
}
