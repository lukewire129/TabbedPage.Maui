using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using TabbedPage.Maui.UI.Units;

namespace TabbedPage.Maui.UI.Views
{
    [ContentProperty (nameof (Items))]
    public partial class LukeTabbedPage : ContentPage, IContentView
    {
        public static readonly BindableProperty SelectedItemProperty =
           BindableProperty.Create (nameof (SelectedItem), typeof (ContentView), typeof (LukeTabbedPage), null, BindingMode.TwoWay);

        public static readonly BindableProperty NavigatorBarStyleProperty =
                BindableProperty.Create (nameof (NavigatorBarStyle), typeof (View), typeof (LukeTabbedPage), default (View), propertyChanged : (bindalble, oldValue, newValue)=>
                {
                    var parent = ((LukeTabbedPage)bindalble);
                    if(oldValue != null)
                    {
                        ((BottomNavigationBar)oldValue).SelectedIndex = null;
                    }
                    parent.Navicv.Content = (View)newValue;
                    ((BottomNavigationBar)newValue).SelectedIndex += (idx) =>
                    {
                        parent.SelectedItem = parent.Items[idx];
                    };
                });

        private Grid _grd;
        private ContentView maincv;
        private ContentView Navicv;
        public LukeTabbedPage()
        {
            _grd = new ();
            maincv = new ();
            Navicv = new ();
            _grd.Children.Add (maincv);
            _grd.Children.Add (Navicv);
            this.Loaded += (s, e) =>
            {
                this.Content = _grd;
            };
        }

        public View NavigatorBarStyle
        {
            get => (View)GetValue (NavigatorBarStyleProperty);
            set { SetValue (NavigatorBarStyleProperty, value); }
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create (nameof (Items), typeof (LukeTabbedPageItems), typeof (LukeTabbedPage), default (LukeTabbedPageItems), propertyChanged: OnChildrenChanged);

        public LukeTabbedPageItems Items
        {
            get => (LukeTabbedPageItems)GetValue (ItemsProperty);
            set => SetValue (ItemsProperty, value);
        }

        static void OnChildrenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var page = (LukeTabbedPage)bindable;
            page.ChildrenChanged (null, null);
        }
        void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Items.IsReadOnly)
                return;

            //OnPagesChanged (e);

            if (CurrentPage == null || Items.IndexOf (CurrentPage) == -1)
                CurrentPage = Items.FirstOrDefault ();
        }

        public event NotifyCollectionChangedEventHandler PagesChanged;
        protected virtual void OnPagesChanged(NotifyCollectionChangedEventArgs e)
            => PagesChanged?.Invoke (this, e);

        ContentView _current;
        bool _hasAppeared;
        private protected bool HasAppeared => _hasAppeared;
        public ContentView CurrentPage
        {
            get { return _current; }
            set
            {
                if (_current == value)
                    return;

                OnPropertyChanging ();
                _current = value;
                OnPropertyChanged ();
                OnCurrentPageChanged ();
                this.maincv.Content = _current;
            }
        }
        public event EventHandler CurrentPageChanged;
        protected virtual void OnCurrentPageChanged()
        {
            EventHandler changed = CurrentPageChanged;
            if (changed != null)
                changed (this, EventArgs.Empty);
        }
        public ContentView SelectedItem
        {
            get { return (ContentView)GetValue (SelectedItemProperty); }
            set { SetValue (SelectedItemProperty, value); }
        }


        void UpdateCurrentPage()
        {
            CurrentPage = SelectedItem;
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == SelectedItemProperty.PropertyName)
            {
                UpdateCurrentPage ();
            }

            base.OnPropertyChanged (propertyName);
        }
    }
}