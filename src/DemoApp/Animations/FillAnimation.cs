using AlohaKit.Animations;
using DemoApp.Animations.Extentions;
using Microsoft.Maui.Controls.Shapes;

namespace DemoApp.Animations;

public class FillAnimation: AnimationBase
{
    public static readonly BindableProperty ToColorProperty =
        BindableProperty.Create (nameof (ToColor), typeof (Brush), typeof (FillAnimation), new SolidColorBrush(Colors.Transparent),
            BindingMode.TwoWay, null);

    public Brush ToColor
    {
        get { return (Brush)GetValue (ToColorProperty); }
        set { SetValue (ToColorProperty, value); }
    }
    protected override Task BeginAnimation()
    {
        if (Target == null)
        {
            throw new NullReferenceException ("Null Target property.");
        }

        var fromColor = ((Shape)Target).Fill;

        return Task.Run (() =>
        {
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            MainThread.BeginInvokeOnMainThread (async () =>
            {
                await Target.BrushTo (fromColor, ToColor, c => ((Shape)Target).Fill = c, Convert.ToUInt32 (Duration));
            });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
        });
    }
}