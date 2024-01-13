using System.ComponentModel;

namespace Luke.Tabs.UI.Units;

[ContentProperty(nameof(Items))]
public partial class LukeTabs : TemplatedView
{

    
    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(LukeTabsItems), typeof(LukeTabs), default(LukeTabsItems), propertyChanged: OnItemsChanged);

    static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as LukeTabs)?.UpdateItems();
    }

    public LukeTabsItems Items
    {
        get => (LukeTabsItems)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(LukeTabsItem), typeof(LukeTabs), null);

    public LukeTabsItem SelectedItem
    {
        get => (LukeTabsItem)GetValue(SelectedItemProperty);
        set { SetValue(SelectedItemProperty, value); }
    }

    public static readonly BindableProperty IndicatorProperty =
            BindableProperty.Create (nameof (Indicator), typeof (View), typeof (LukeTabs), propertyChanged:(bindable, oldValue, newValue)=>
            {
                (bindable as LukeTabs)?.UpdateIndicator ();
            });

    public View Indicator
    {
        get => (View)GetValue (IndicatorProperty);
        set { SetValue (IndicatorProperty, value); }
    }
    public static readonly BindableProperty TabsBackgroundProperty =
            BindableProperty.Create (nameof (TabsBackground), typeof (View), typeof (LukeTabs), propertyChanged: (bindable, oldValue, newValue) =>
            {
                (bindable as LukeTabs)?.UpdateBackground ();
            });

    public View TabsBackground
    {
        get => (View)GetValue (TabsBackgroundProperty);
        set { SetValue (TabsBackgroundProperty, value); }
    }

    Grid _container;

    ContentView _tabsBackground;
    ContentView _indicator;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _tabsBackground = GetTemplateChild ("PART_TabsBackground") as ContentView;
        _indicator = GetTemplateChild ("PART_Indicator") as ContentView;
        _container = GetTemplateChild("PART_Grd") as Grid;
    }
    void UpdateIndicator()
    {
        if (Indicator == null)
            return;
        this._indicator.Content = Indicator;
    }
    void UpdateBackground()
    {
        if (TabsBackground == null)
            return;
        this._tabsBackground.Content = TabsBackground;
    }

    void UpdateItems()
    {
        if (Items == null || Items.Count == 0)
            return;

        List<ColumnDefinition> def = new List<ColumnDefinition>();
        for (int i = 0; i < Items.Count; i++)
        {
            def.Add(new ColumnDefinition(GridLength.Star));
        }
        _container.ColumnDefinitions = new ColumnDefinitionCollection(def.ToArray());
        int j = 0;
        foreach (var child in Items)
        {
            if (!_container.Children.Contains(child))
            {
                child.Index = j;
                Grid.SetColumn(child, j++);
                _container.Children.Add(child);
            }
        }
        if (this._indicator.Content == null)
            return;
        IndicatorUpdate ();
    }

    void IndicatorUpdate()
    {
        if(Indicator == null) return;   
        this._indicator.Content = Indicator;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void SendItemTapped(TappedEventArgs args)
    {

    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void UpdateSelectedItem(LukeTabsItem selectedItem, bool isSelected)
    {
        if (SelectedItem == selectedItem)
            return;

        UnSelectItems(Items);
        selectedItem.ChangeSelected(isSelected);

        SelectedItem = selectedItem;

        if(_indicator.Content is LukeTabIndicator indicator)
        {
            indicator.SelectionItem (selectedItem.Index);            
        }
    }

    void UnSelectItems(LukeTabsItems items)
    {
        foreach (var child in items)
        {
            child.ChangeSelected(false);
        }
    }
}
