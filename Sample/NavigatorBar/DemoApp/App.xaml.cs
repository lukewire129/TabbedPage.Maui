using TabbedPage.Maui.Themes;

namespace DemoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent ();
            var templateMAUIDictionary = new Generic ();
            if (!Application.Current.Resources.MergedDictionaries.Contains (templateMAUIDictionary))
                Application.Current.Resources.Add (templateMAUIDictionary);
            MainPage = new MainPage ();
        }
    }
}
