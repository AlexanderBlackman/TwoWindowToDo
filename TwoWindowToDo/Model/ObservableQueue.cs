using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoWindowToDo.Model
{

//    /// <summary>
//	/// Queue that notifies when it's collection changes.
//	/// </summary>
//	/// <typeparam name="T">Element type.</typeparam>
//	public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged
//    {
//        /// <summary>
//        /// Occurs when the collection changes.
//        /// </summary>
//        public event NotifyCollectionChangedEventHandler CollectionChanged;

//        /// <summary>
//        /// Adds an object to the end of the <see cref="ObservableQueue{T}"/>.
//        /// </summary>
//        /// <param name="item">The object to add to the <see cref="ObservableQueue{T}"/>. The value can be null for reference types.</param>
//        public new void Enqueue(T item)
//        {
//            base.Enqueue(item);
//            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
//        }

//        /// <summary>
//        /// Removes and returns the object at the beginning of the <see cref="ObservableQueue{T}"/>.
//        /// </summary>
//        /// <returns>The object that is removed from the beginning of the <see cref="ObservableQueue{T}"/>.</returns>
//        /// <exception cref="InvalidOperationException">Thrown when the <see cref="ObservableQueue{T}"/> is empty.</exception>
//        public new T Dequeue()
//        {
//            T item = base.Dequeue();
//            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
//            return item;
//        }

//        /// <summary>
//        /// Removes all objects from the <see cref="ObservableQueue{T}"/>.
//        /// </summary>
//        public new void Clear()
//        {
//            base.Clear();
//            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//        }

//        /// <summary>
//        /// Removes the object at the beginning of the <see cref="ObservableQueue{T}"/>, and copies it to the result parameter.
//        /// </summary>
//        /// <param name="item">The removed object.</param>
//        /// <returns>true if the object is successfully removed; false if the <see cref="ObservableQueue{T}"/> is empty.</returns>
//        /// <exception cref="ArgumentNullException">Thrown when key is null.</exception>
//        public new bool TryDequeue([MaybeNullWhen(false)] out T item)
//        {
//            bool result = base.TryDequeue(out item);

//            if (result)
//                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));

//            return result;
//        }

//    }
//}


///< summary >
///A queue that raises events when items are added or removed
///</summary>
///<typeparam name="TItem"></typeparam>
public class ObservableQueue<TItem> : Queue<TItem>, INotifyCollectionChanged, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    new public void Enqueue(TItem item)
    {
        base.Enqueue(item);
        OnPropertyChanged();
        OnCollectionChanged(NotifyCollectionChangedAction.Add, item, this.Count - 1);
    }

    new public TItem Dequeue()
    {
        TItem removedItem = base.Dequeue();
        OnPropertyChanged();
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, 0);
        return removedItem;
    }

    new public bool TryDequeue(out TItem? result)
    {
        if (base.TryDequeue(out result))
        {
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, result, 0);
            return true;
        }
        return false;
    }

    new public void Clear()
    {
        base.Clear();
        OnPropertyChanged();
        OnCollectionChangedReset();
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, TItem item, int index)
      => this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index));

    private void OnCollectionChangedReset()
      => this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

    private void OnPropertyChanged() => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Count)));
}
}
