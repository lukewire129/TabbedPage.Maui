using Microsoft.Maui.Controls.Shapes;

namespace DemoApp.Animations.Extentions;

public static class BrushExtensions
{
    public static Task<bool> BrushTo(this VisualElement self, Brush fromColor, Brush toColor, Action<Brush> callback, uint length = 250u, Easing easing = null)
    {
        return BrushAnimation (self, "BrushTo", transform, callback, length, easing);
        Brush transform(double t)
        {
            Color _fromColor = ((SolidColorBrush)fromColor).Color;
            Color _toColor = ((SolidColorBrush)toColor).Color;
            return new SolidColorBrush(Color.FromRgba ((double)_fromColor.Red + t * (double)(_toColor.Red - _fromColor.Red), (double)_fromColor.Green + t * (double)(_toColor.Green - _fromColor.Green), (double)_fromColor.Blue + t * (double)(_toColor.Blue - _fromColor.Blue), (double)_fromColor.Alpha + t * (double)(_toColor.Alpha - _fromColor.Alpha)));
        }
    }

    public static void CancelAnimation(this Shape self)
    {
        self.AbortAnimation ("BrushTo");
    }

    private static Task<bool> BrushAnimation(VisualElement element, string name, Func<double, Brush> transform, Action<Brush> callback, uint length, Easing easing)
    {
        easing = easing ?? Easing.Linear;
        TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool> ();
        element.Animate (name, transform, callback, 16u, length, easing, delegate (Brush v, bool c)
        {
            taskCompletionSource.SetResult (c);
        });
        return taskCompletionSource.Task;
    }
}