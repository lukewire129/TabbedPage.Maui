using AlohaKit.Animations;
using MauiIcons.Core;
using Microsoft.Maui.Controls;

namespace DemoApp3.Animations;

public class MauiIconSizeAnimation : AnimationBase
{
    public static readonly BindableProperty ToColorProperty = BindableProperty.Create ("ToColor", typeof (double), typeof (MauiIconSizeAnimation), 0.0, BindingMode.TwoWay);

    public double ToScale
    {
        get
        {
            return (double)GetValue (ToColorProperty);
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

        double fromColor = ((MauiIcon)base.Target).Scale;
        return Task.Run (delegate
        {
            Device.BeginInvokeOnMainThread (async delegate
            {
                await base.Target.ScaleTo (ToScale, Convert.ToUInt32 (base.Duration));
            });
        });
    }
}