using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace Luke.Tabs.UI.Units;

public partial class LukeTabs : TemplatedView
{
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create (nameof (Background), typeof (Brush), typeof (LukeTabs), Brush.Default);
    [TypeConverter (typeof (BrushTypeConverter))]
    public new Brush Background
    {
        get => (Brush)GetValue (BackgroundProperty);
        set => SetValue (BackgroundProperty, value);
    }
    public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create (nameof (CornerRadius), typeof (CornerRadius), typeof (LukeTabs), new CornerRadius (0,0,0,0), propertyChanged: (bindable, oldValue, newValue)=>
            {
                RoundRectangle RoundRectangle = new RoundRectangle ();
                RoundRectangle.CornerRadius = (CornerRadius)newValue;
                ((LukeTabs)bindable)._containerArea.StrokeShape = RoundRectangle;
            });

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue (CornerRadiusProperty);
        set { SetValue (CornerRadiusProperty, value); }
    }

    public static new readonly BindableProperty OpacityProperty = BindableProperty.Create (nameof (Opacity), typeof (double), typeof (LukeTabs), default (double));

    public new double Opacity
    {
        get => (double)GetValue (OpacityProperty);
        set => SetValue (OpacityProperty, value);
    }
}
