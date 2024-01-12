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
    Border _containerArea;
    Grid _container;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _containerArea = GetTemplateChild ("PART_Area") as Border;
        _container = GetTemplateChild("PART_Grd") as Grid;
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
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void SendItemTapped(TappedEventArgs args)
    {

    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal void UpdateSelectedItem(LukeTabsItem selectedItem, bool isSelected)
    {
        if (SelectedItem == selectedItem)
            return;

        UnSelectItems(Items);
        selectedItem.ChangeSelected(isSelected);

        SelectedItem = selectedItem;

        //_circle.TranslateTo (selectedItem.Index * 80, 0);
    }

    void UnSelectItems(LukeTabsItems items)
    {
        foreach (var child in items)
        {
            child.ChangeSelected(false);
        }
    }
}
