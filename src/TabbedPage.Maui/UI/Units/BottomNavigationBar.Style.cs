using System.ComponentModel;

namespace TabbedPage.Maui.UI.Units;

public partial class BottomNavigationBar : ContentView
{
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create (nameof (Padding), typeof (Thickness), typeof (BottomNavigationBar), new Thickness());

    public new Thickness Padding
    {
        get => (Thickness)GetValue (PaddingProperty);
        set => SetValue (PaddingProperty, value);
    }
}
