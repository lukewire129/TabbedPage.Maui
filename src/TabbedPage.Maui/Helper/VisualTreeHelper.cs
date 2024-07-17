namespace TabbedPage.Maui.Helper
{
    public static class VisualTreeHelper
    {
        public static T GetParent<T>(this Element element) where T : Element
        {
            if (element is T)
                return element as T;
            else
            {
                if (element.Parent != null)
                    return element.Parent.GetParent<T> ();

                return default;
            }
        }
    }
}
