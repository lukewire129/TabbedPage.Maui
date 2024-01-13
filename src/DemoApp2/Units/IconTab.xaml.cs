using AlohaKit.Animations;
using Luke.Tabs.UI.Units;
using DemoApp2.Animations;
namespace DemoApp2.Units;

public enum Type
{
    Home,
    Shop,
    Wishlist,
    Cart,
    Me,
    None,
}

public partial class IconTab : LukeTabsItem
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(IconTab), default(string));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type),
        typeof(Type),
        typeof(IconTab),
        default(Type));

    public int index { get; set; }

    public Type Type
    {
        get => (Type)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    StoryBoard iconSelectedSB;
    StoryBoard iconUnSelectedSB;
    public IconTab()
    {
        InitializeComponent();
        IconSelectedSB ();
        IconUnSelectedSB ();
    }

    public override void ChangeSelected(bool select)
    {
        this.IsSelected = select;

        if (IsSelected)
        {
            Select ();
        }
        else
        {
            UnSelect ();
        }
    }

    private void Select()
    {
        MainThread.InvokeOnMainThreadAsync (() =>
        {
            PART_Circle.IsVisible = true;
            PART_Text.IsVisible = false;
        });
        iconSelectedSB.Begin ();
    }
    private void UnSelect()
    {
        MainThread.InvokeOnMainThreadAsync (() =>
        {
            PART_Circle.IsVisible = false;
            PART_Text.IsVisible = true;
        });
        iconUnSelectedSB.Begin ();
    }
    private void IconSelectedSB()
    {
        MarginAnimation marginAnimation = new MarginAnimation ();
        marginAnimation.Target = PART_IconArea;
        marginAnimation.To = new Thickness (0, 20, 0, 0);
        marginAnimation.Duration = "500";

        MauiIconColorAnimation animation = new MauiIconColorAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = Colors.White;
        animation.Duration = "500";

        iconSelectedSB = new StoryBoard ();
        iconSelectedSB.Target = PART_Icon;
        iconSelectedSB.Animations.Add (animation);
        iconSelectedSB.Animations.Add (marginAnimation);
    }

    private void IconUnSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = PART_IconArea;
        MarginAnimation.To = new Thickness (0, 0, 0, 0);
        MarginAnimation.Duration = "500";

        MauiIconColorAnimation animation = new MauiIconColorAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = Color.FromArgb ("#b5bac6");
        animation.Duration = "500";

        iconUnSelectedSB = new StoryBoard ();
        iconUnSelectedSB.Target = PART_Icon;
        iconUnSelectedSB.Animations.Add (animation);
        iconUnSelectedSB.Animations.Add (MarginAnimation);
    }
}