using AlohaKit.Animations;
using MauiIcons.Core;

namespace DemoApp2.Animations;

public class MauiIconColorAnimation : AnimationBase
{
    public static readonly BindableProperty ToColorProperty = BindableProperty.Create ("ToColor", typeof (Color), typeof (MauiIconColorAnimation), Colors.Transparent, BindingMode.TwoWay);

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

        Color fromColor = ((MauiIcon)base.Target).IconColor;
        return Task.Run (delegate
        {
            Device.BeginInvokeOnMainThread (async delegate
            {
                await base.Target.ColorTo (fromColor, ToColor, delegate (Color c)
                {
                    ((MauiIcon)base.Target).IconColor = c;
                }, Convert.ToUInt32 (base.Duration));
            });
        });
    }
}