using Luke.Tabs.Helper;

namespace Luke.Tabs.UI.Units;

public class LukeTabsItem : ContentView
{
    public static readonly BindableProperty IsSelectedProperty =
          BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(LukeTabsItem), false,
              propertyChanged: (bindable, oldValue, newValue) =>
              {
                  if (oldValue == newValue)
                      return;
                  var item = bindable as LukeTabsItem;
                  item?.UpdateCurrent();
              });
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set { SetValue(IsSelectedProperty, value); }
    }
    public int Index { get;set; }
    LukeTabs _lukeTabs;
    public LukeTabsItem()
    {
        var textGesturedTap = new TapGestureRecognizer();
        textGesturedTap.Tapped += (s, e) =>
        {
            if (IsSelected == true)
                return;
            IsSelected = !IsSelected;
        };
        GestureRecognizers.Add(textGesturedTap);
        Loaded += (s, e) =>
        {
            if (IsSelected == false)
                return;

            UpdateCurrent();
        };
    }
    public virtual void ChangeSelected(bool select)
    {
        this.IsSelected = select;
    }
    public void UpdateCurrent()
    {
        if (_lukeTabs == null)
            _lukeTabs = this.GetParent<LukeTabs>();

        _lukeTabs?.UpdateSelectedItem(this, IsSelected);
    }
}
