using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TabbedPage.Maui.Themes.Units;

namespace TabbedPage.Maui.UI.Views
{
    [ContentProperty (nameof (Children))]
    public partial class LukeTabbedView<T> : TemplatedPage where T : ContentView
    {
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create ("SelectedItem", typeof (object), typeof (LukeTabbedView<>), null, BindingMode.TwoWay);
        readonly ObservableCollection<ContentView> _children;
        [EditorBrowsable (EditorBrowsableState.Never)]
        public ObservableCollection<ContentView> InternalChildren { get; } = new ObservableCollection<ContentView> ();

        public LukeTabbedView()
        {
            _children = new ObservableCollection<ContentView> (InternalChildren);
            InternalChildren.CollectionChanged += OnChildrenChanged;
        }

        void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Children.IsReadOnly)
                return;

            var i = 0;

            OnPagesChanged (e);

            if (CurrentPage == null || Children.IndexOf (CurrentPage) == -1)
                CurrentPage = Children.FirstOrDefault ();
        }

        public IList<ContentView> Children
        {
            get { return _children; }
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
            }
        }
        public event EventHandler CurrentPageChanged;
        protected virtual void OnCurrentPageChanged()
        {
            EventHandler changed = CurrentPageChanged;
            if (changed != null)
                changed (this, EventArgs.Empty);
        }
        public object SelectedItem
        {
            get { return GetValue (SelectedItemProperty); }
            set { SetValue (SelectedItemProperty, value); }
        }
        void UpdateCurrentPage()
        {
            CurrentPage = (T)SelectedItem;
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