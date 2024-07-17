using TabbedPage.Maui.UI.Units;

namespace DemoApp4.Units;

public enum Type
{
    Home,
    Shop,
    Group,
    Cart,
    Me,
    None,
}

public partial class IconTab : BottomNavigationBarItem
{
    public static readonly BindableProperty TypeProperty = BindableProperty.Create (nameof (Type),
    typeof (Type),
    typeof (IconTab),
    default (Type));

    public int index { get; set; }

    public Type Type
    {
        get => (Type)GetValue (TypeProperty);
        set => SetValue (TypeProperty, value);
    }
    public IconTab()
    {
        InitializeComponent ();
    }
}
