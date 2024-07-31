using System.ComponentModel;

namespace TabbedPage.Maui.UI.Units;

[ContentProperty (nameof (Items))]
public partial class BottomNavigationBar : ContentView
{
    public static readonly BindableProperty ItemsProperty = BindableProperty.Create (nameof (Items), typeof (BottomNavigationBarItems), typeof (BottomNavigationBar), default (BottomNavigationBarItems), propertyChanged: OnItemsChanged);

    static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as BottomNavigationBar)?.UpdateItems ();
    }

    public BottomNavigationBarItems Items
    {
        get => (BottomNavigationBarItems)GetValue (ItemsProperty);
        set => SetValue (ItemsProperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create (nameof (SelectedItem), typeof (BottomNavigationBarItem), typeof (BottomNavigationBar), null);

    public BottomNavigationBarItem SelectedItem
    {
        get => (BottomNavigationBarItem)GetValue (SelectedItemProperty);
        set { SetValue (SelectedItemProperty, value); }
    }

    public static readonly BindableProperty IndicatorProperty =
            BindableProperty.Create (nameof (Indicator), typeof (View), typeof (BottomNavigationBar), propertyChanged: (bindable, oldValue, newValue) =>
            {
                (bindable as BottomNavigationBar)?.UpdateIndicator ();
            });

    public View Indicator
    {
        get => (View)GetValue (IndicatorProperty);
        set { SetValue (IndicatorProperty, value); }
    }
    public static readonly BindableProperty TabsBackgroundProperty =
            BindableProperty.Create (nameof (TabsBackground), typeof (View), typeof (BottomNavigationBar), propertyChanged: (bindable, oldValue, newValue) =>
            {
                (bindable as BottomNavigationBar)?.UpdateBackground ();
            });

    public View TabsBackground
    {
        get => (View)GetValue (TabsBackgroundProperty);
        set { SetValue (TabsBackgroundProperty, value); }
    }

    Grid _container;

    ContentView _tabsBackground;
    ContentView _indicator;

    public BottomNavigationBar()
    {
        InitializeComponent ();

        _tabsBackground = PART_TabsBackground;
        _indicator = PART_Indicator;
        _container = PART_Grd;

        this.Loaded += BottomNavigationBar_Loaded;
    }

    private void BottomNavigationBar_Loaded(object? sender, EventArgs e)
    {
        UpdateIndicator ();
        UpdateBackground ();
        UpdateItems ();
    }

    void UpdateIndicator()
    {
        if (Indicator == null)
            return;
        if (this._indicator == null)
            return;
        this._indicator.Content = Indicator;
    }
    void UpdateBackground()
    {
        if (TabsBackground == null)
            return;
        if (this._tabsBackground == null)
            return;
        this._tabsBackground.Content = TabsBackground;
    }

    void UpdateItems()
    {
        if (Items == null || Items.Count == 0)
            return;
        if (this._container == null)
            return;
        List<ColumnDefinition> def = new List<ColumnDefinition> ();
        for (int i = 0; i < Items.Count; i++)
        {
            def.Add (new ColumnDefinition (GridLength.Star));
        }
        _container.ColumnDefinitions = new ColumnDefinitionCollection (def.ToArray ());
        int j = 0;
        foreach (var child in Items)
        {
            if (!_container.Children.Contains (child))
            {
                child.Index = j;
                Grid.SetColumn (child, j++);
                _container.Children.Add (child);
            }
        }
        if (this._indicator == null)
            return;
        IndicatorUpdate ();
    }

    void IndicatorUpdate()
    {
        if (Indicator == null)
            return;
        this._indicator.Content = Indicator;
    }

    [EditorBrowsable (EditorBrowsableState.Never)]
    internal void SendItemTapped(TappedEventArgs args)
    {

    }
    public Action<int> SelectedIndex { get; set; }
    [EditorBrowsable (EditorBrowsableState.Never)]
    public void UpdateSelectedItem(BottomNavigationBarItem selectedItem, bool isSelected)
    {
        if (SelectedItem == selectedItem)
            return;

        UnSelectItems (Items);
        selectedItem.ChangeSelected (isSelected);

        SelectedItem = selectedItem;

        if (_indicator.Content is BottomNavigationBarIndicator indicator)
        {
            indicator.SelectionItem (selectedItem.Index);
        }
        SelectedIndex?.Invoke (selectedItem.Index);
    }

    void UnSelectItems(BottomNavigationBarItems items)
    {
        foreach (var child in items)
        {
            child.ChangeSelected (false);
        }
    }
}