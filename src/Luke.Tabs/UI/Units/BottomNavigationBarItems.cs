using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Luke.Tabs.UI.Units;

public class BottomNavigationBarItems : Element, IList<BottomNavigationBarItem>, INotifyCollectionChanged
{
    readonly ObservableCollection<BottomNavigationBarItem> _items;

    public BottomNavigationBarItems(IEnumerable<BottomNavigationBarItem> items)
    {
        _items = new ObservableCollection<BottomNavigationBarItem>(items) ?? throw new ArgumentNullException(nameof(items));
        _items.CollectionChanged += OnItemsChanged;
    }
    public BottomNavigationBarItems() : this(Enumerable.Empty<BottomNavigationBarItem>())
    {

    }

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add { _items.CollectionChanged += value; }
        remove { _items.CollectionChanged -= value; }
    }

    public BottomNavigationBarItem this[int index]
    {
        get => _items.Count > index ? _items[index] : null;
        set => _items[index] = value;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(BottomNavigationBarItem item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(BottomNavigationBarItem item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(BottomNavigationBarItem[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<BottomNavigationBarItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(BottomNavigationBarItem item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, BottomNavigationBarItem item)
    {
        _items.Insert(index, item);
    }

    public bool Remove(BottomNavigationBarItem item)
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
