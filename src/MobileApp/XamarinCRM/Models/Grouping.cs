using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinCRM.Models
{
    public class Grouping<T,K> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(IEnumerable<T> items, K key)
        {
            Key = key;
            Items.AddRange(items);
        }
    }
}

