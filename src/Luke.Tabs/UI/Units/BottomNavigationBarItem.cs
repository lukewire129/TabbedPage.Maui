using Luke.Tabs.Helper;

namespace Luke.Tabs.UI.Units;

public class BottomNavigationBarItem : ContentView
{
    public static readonly BindableProperty IsSelectedProperty =
          BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(BottomNavigationBarItem), false,
              propertyChanged: (bindable, oldValue, newValue) =>
              {
                  if (oldValue == newValue)
                      return;
                  var item = bindable as BottomNavigationBarItem;
                  item?.UpdateCurrent();
              });
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set { SetValue(IsSelectedProperty, value); }
    }
    public int Index { get;set; }
    BottomNavigationBar _bar;
    public BottomNavigationBarItem()
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
        if (_bar == null)
            _bar = this.GetParent<BottomNavigationBar> ();

        _bar?.UpdateSelectedItem(this, IsSelected);
    }
}
