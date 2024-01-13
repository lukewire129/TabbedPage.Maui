using AlohaKit.Animations;
using DemoApp.Animations;
using Luke.Tabs.UI.Units;

namespace DemoApp.Units;

public enum Type
{
    Microsoft,
    Apple,
    Google,
    Facebook,
    Instagram,
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
    StoryBoard textSelectedSB;
    StoryBoard textUnSelectedSB;
    
    public IconTab()
    {
        InitializeComponent();

        IconSelectedSB ();
        IconUnSelectedSB ();
        TextSelectedSB ();
        TextUnSelectedSB ();
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
        iconSelectedSB.Begin ();
        textSelectedSB.Begin ();

    }
    private void UnSelect()
    {
        iconUnSelectedSB.Begin ();
        textUnSelectedSB.Begin ();
    }
    private void IconSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = PART_Icon;
        MarginAnimation.To = new Thickness(15, -65, 0, 0);
        MarginAnimation.Duration = "500";

        FillAnimation animation = new FillAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = new SolidColorBrush (Color.FromArgb ("#333333"));
        animation.Duration = "500";

        iconSelectedSB = new StoryBoard ();
        iconSelectedSB.Target = PART_Icon;
        iconSelectedSB.Animations.Add (animation);
        iconSelectedSB.Animations.Add (MarginAnimation);
    }

    private void IconUnSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = PART_Icon;
        MarginAnimation.To = new Thickness (15, 0, 0, 0);
        MarginAnimation.Duration = "500";

        FillAnimation animation = new FillAnimation ();
        animation.Target = PART_Icon;
        animation.ToColor = new SolidColorBrush (Color.FromArgb ("#44333333"));
        animation.Duration = "500";

        iconUnSelectedSB = new StoryBoard ();
        iconUnSelectedSB.Target = PART_Icon;
        iconUnSelectedSB.Animations.Add (animation);
        iconUnSelectedSB.Animations.Add (MarginAnimation);
    }

    private void TextSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = PART_Text;
        MarginAnimation.To = new Thickness (0, 65, 0, 0);
        MarginAnimation.Duration = "500";

        TextColorAnimation animation = new TextColorAnimation ();
        animation.Target = PART_Text;
        animation.ToColor =Color.FromArgb ("#333333");
        animation.Duration = "500";

        textSelectedSB = new StoryBoard ();
        textSelectedSB.Target = PART_Text;
        textSelectedSB.Animations.Add (animation);
        textSelectedSB.Animations.Add (MarginAnimation);
    }

    private void TextUnSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = PART_Text;
        MarginAnimation.To = new Thickness (0, 80, 0, 0);
        MarginAnimation.Duration = "500";

        TextColorAnimation animation = new TextColorAnimation ();
        animation.Target = PART_Text;
        animation.ToColor = Color.FromArgb ("#00000000");
        animation.Duration = "500";

        textUnSelectedSB = new StoryBoard ();
        textUnSelectedSB.Target = PART_Text;
        textUnSelectedSB.Animations.Add (animation);
        textUnSelectedSB.Animations.Add (MarginAnimation);
    }
}