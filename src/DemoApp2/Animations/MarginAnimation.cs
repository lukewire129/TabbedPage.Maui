using AlohaKit.Animations;

namespace DemoApp2.Animations;

public class MarginAnimation : AnimationBase
{
    public static readonly BindableProperty ToColorProperty =
        BindableProperty.Create (nameof (To), typeof (Thickness), typeof (MarginAnimation), new Thickness(0,0,0,0),
            BindingMode.TwoWay, null);

    public Thickness To
    {
        get { return (Thickness)GetValue (ToColorProperty); }
        set { SetValue (ToColorProperty, value); }
    }

    public Thickness From;
    protected override Task BeginAnimation()
    {
        if (Target == null)
        {
            throw new NullReferenceException ("Null Target property.");
        }

        From = ((View)Target).Margin;

        return Task.Run (() =>
        {
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            MainThread.BeginInvokeOnMainThread (async () =>
            {
                Target.Animate ("MarginPoint", TopMargin (From), 16, Convert.ToUInt32 (Duration));
            });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
        });
    }
    internal Animation TopMargin(Thickness from)
    {
        View view = ((View)Target);
        var animation = new Animation ()
            .WithConcurrent ((f) => view.Margin = new Thickness (To.Left, f, 0, 0), start: from.Top, end: To.Top, beginAt: 0.0, finishAt: 0.5);
        return animation;
    }
}