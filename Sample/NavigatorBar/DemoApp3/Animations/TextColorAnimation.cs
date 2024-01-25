using AlohaKit.Animations;

namespace DemoApp3.Animations;

public class TextColorAnimation: AnimationBase
{
    public static readonly BindableProperty ToColorProperty = BindableProperty.Create ("ToColor", typeof (Color), typeof (TextColorAnimation), Colors.Transparent, BindingMode.TwoWay);

    public Color ToColor
    {
        get
        {
            return (Color)GetValue (ToColorProperty);
        }
        set
        {
            SetValue (ToColorProperty, value);
        }
    }

    protected override Task BeginAnimation()
    {
        if (base.Target == null)
        {
            throw new NullReferenceException ("Null Target property.");
        }

        Color fromColor = ((Label)base.Target).TextColor;
        return Task.Run (delegate
        {
            Device.BeginInvokeOnMainThread (async delegate
            {
                await base.Target.ColorTo (fromColor, ToColor, delegate (Color c)
                {
                    ((Label)base.Target).TextColor = c;
                }, Convert.ToUInt32 (base.Duration));
            });
        });
    }
}