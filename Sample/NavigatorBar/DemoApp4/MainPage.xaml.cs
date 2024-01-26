using System.Diagnostics;

namespace DemoApp4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent ();
            
            List<string> a = new List<string> ();
            for(int i=0; i<100; i++)
            {
                a.Add(i.ToString ());
            }
            items.ItemsSource = a;
        }

        private void items_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if(e.VerticalDelta > 0)
            {
                navi.Opacity = 0.5;
            }
            else
            {
                navi.Opacity = 1;
            }
        }
    }

}
