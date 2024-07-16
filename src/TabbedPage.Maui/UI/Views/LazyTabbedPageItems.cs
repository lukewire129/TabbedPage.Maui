using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TabbedPage.Maui.UI.Views;

public class LazyTabbedPageItems : Element, IList<ContentView>, INotifyCollectionChanged
{
    readonly ObservableCollection<ContentView> _items;

    public LazyTabbedPageItems(IEnumerable<ContentView> items)
    {
        _items = new ObservableCollection<ContentView> (items) ?? throw new ArgumentNullException (nameof (items));
        _items.CollectionChanged += OnItemsChanged;
    }
    public LazyTabbedPageItems() : this (Enumerable.Empty<ContentView> ())
    {

    }

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add { _items.CollectionChanged += value; }
        remove { _items.CollectionChanged -= value; }
    }

    public ContentView this[int index]
    {
        get => _items.Count > index ? _items[index] : null;
        set => _items[index] = value;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(ContentView item)
    {
        _items.Add (item);
    }

    public void Clear()
    {
        _items.Clear ();
    }

    public bool Contains(ContentView item)
    {
        return _items.Contains (item);
    }

    public void CopyTo(ContentView[] array, int arrayIndex)
    {
        _items.CopyTo (array, arrayIndex);
    }

    public IEnumerator<ContentView> GetEnumerator()
    {
        return _items.GetEnumerator ();
    }

    public int IndexOf(ContentView item)
    {
        return _items.IndexOf (item);
    }

    public void Insert(int index, ContentView item)
    {
        _items.Insert (index, item);
    }

    public bool Remove(ContentView item)
    {
        return _items.Remove (item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt (index);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged ();

        object bc = BindingContext;

        foreach (BindableObject item in _items)
            SetInheritedBindingContext (item, bc);
    }

    void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        if (notifyCollectionChangedEventArgs.NewItems == null)
            return;

        object bc = BindingContext;

        foreach (BindableObject item in notifyCollectionChangedEventArgs.NewItems)
            SetInheritedBindingContext (item, bc);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator ();
    }
}
