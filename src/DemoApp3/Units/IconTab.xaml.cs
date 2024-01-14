using AlohaKit.Animations;
using Luke.Tabs.UI.Units;
using DemoApp3.Animations;
using AlohaKit.Animations.Triggers;
namespace DemoApp3.Units;

public enum Type
{
    Home,
    Shop,
    Wishlist,
    Cart,
    Me,
    None,
}

public partial class IconTab : BottomNavigationBarItem
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

    StoryBoard SelectedSB;
    StoryBoard UnSelectedSB;
    public IconTab()
    {
        InitializeComponent();
        SelectedSBInit ();
        UnSelectedSBInit ();
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
        SelectedSB.Begin ();
    }
    private void UnSelect()
    {
        UnSelectedSB.Begin ();
    }
    private void SelectedSBInit()
    {
        MauiIconColorAnimation animation = new MauiIconColorAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = Color.FromArgb ("#387bea");
        animation.Duration = "500";

        MauiIconSizeAnimation mauiIconSizeAnimation = new MauiIconSizeAnimation ();
        mauiIconSizeAnimation.Target = PART_Icon;
        mauiIconSizeAnimation.ToScale = 1.5;
        mauiIconSizeAnimation.Duration = "300";

        TextColorAnimation textColorAnimation = new TextColorAnimation ();
        textColorAnimation.Target = PART_Text;
        textColorAnimation.ToColor = Color.FromArgb ("#387bea");
        textColorAnimation.Duration = "500";

        SelectedSB = new StoryBoard ();
        SelectedSB.Animations.Add (animation);
        SelectedSB.Animations.Add (textColorAnimation);
        SelectedSB.Animations.Add (mauiIconSizeAnimation);
    }

    private void UnSelectedSBInit()
    {
        MauiIconColorAnimation animation = new MauiIconColorAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = Color.FromArgb ("#b5bac6");
        animation.Duration = "500";
        MauiIconSizeAnimation mauiIconSizeAnimation = new MauiIconSizeAnimation ();
        mauiIconSizeAnimation.Target = PART_Icon;
        mauiIconSizeAnimation.ToScale = 1.0;
        mauiIconSizeAnimation.Duration = "300";

        TextColorAnimation textColorAnimation = new TextColorAnimation ();
        textColorAnimation.Target = PART_Text;
        textColorAnimation.ToColor = Color.FromArgb ("#b5bac6");
        textColorAnimation.Duration = "500";

        UnSelectedSB = new StoryBoard ();
        UnSelectedSB.Animations.Add (animation);
        UnSelectedSB.Animations.Add (textColorAnimation);
        UnSelectedSB.Animations.Add (mauiIconSizeAnimation);
    }
}