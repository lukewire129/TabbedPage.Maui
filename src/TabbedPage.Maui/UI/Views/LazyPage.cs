using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabbedPage.Maui.UI.Views
{
    public class CustomLazyView<TView> : LazyView where TView : View, new()
    {
        public override async ValueTask LoadViewAsync(CancellationToken token)
        {
            // display a loading indicator
            Content = new ActivityIndicator { IsRunning = true }.Center ();

            // simulate a long running task
            await Task.Delay (3000, token);

            // load the view
            Content = new TView { BindingContext = BindingContext };

            SetHasLazyViewLoaded (true);
        }
    }
}
