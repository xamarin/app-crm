using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinCRM.Models
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            Items.AddRange(items);
        }
    }
}

