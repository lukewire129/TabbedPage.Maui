using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Luke.Tabs.UI.Units;

public class LukeTabsItems : Element, IList<LukeTabsItem>, INotifyCollectionChanged
{
    readonly ObservableCollection<LukeTabsItem> _items;

    public LukeTabsItems(IEnumerable<LukeTabsItem> items)
    {
        _items = new ObservableCollection<LukeTabsItem>(items) ?? throw new ArgumentNullException(nameof(items));
        _items.CollectionChanged += OnItemsChanged;
    }
    public LukeTabsItems() : this(Enumerable.Empty<LukeTabsItem>())
    {

    }

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add { _items.CollectionChanged += value; }
        remove { _items.CollectionChanged -= value; }
    }

    public LukeTabsItem this[int index]
    {
        get => _items.Count > index ? _items[index] : null;
        set => _items[index] = value;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(LukeTabsItem item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(LukeTabsItem item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(LukeTabsItem[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<LukeTabsItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(LukeTabsItem item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, LukeTabsItem item)
    {
        _items.Insert(index, item);
    }

    public bool Remove(LukeTabsItem item)
    {
        return _items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        object bc = BindingContext;

        foreach (BindableObject item in _items)
            SetInheritedBindingContext(item, bc);
    }

    void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        if (notifyCollectionChangedEventArgs.NewItems == null)
            return;

        object bc = BindingContext;

        foreach (BindableObject item in notifyCollectionChangedEventArgs.NewItems)
            SetInheritedBindingContext(item, bc);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
