using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace Luke.Tabs.UI.Units;

public partial class LukeTabs : TemplatedView
{
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create (nameof (Padding), typeof (Thickness), typeof (LukeTabs), new Thickness());
    [TypeConverter (typeof (BrushTypeConverter))]
    public new Thickness Padding
    {
        get => (Thickness)GetValue (PaddingProperty);
        set => SetValue (PaddingProperty, value);
    }
}
