using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using XamarinCRM.Models;
using System.Reflection;
using System.Linq;

namespace XamarinCRM.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var i in items)
            {
                collection.Add(i);
            }
        }

        /// <summary>
        /// Adds a range of IEnumerable<T> to an ObservableCollection<Grouping<T,K>, grouped by a propertyName of type K.
        /// </summary>
        /// <param name="collection">An ObservableCollection<Grouping<T,K>>.</param>
        /// <param name="items">IEnumerable<T></param>
        /// <param name="propertyName">The property name on to group by. MUST be a valid property name (case-sensitive) on type T and MUST be of type K. Throws an ArgumentException if not.</param>
        /// /// <typeparam name="T">The type of items in the items collection of the Grouping.</typeparam>
        /// <typeparam name="K">The type of the Grouping key.</typeparam>
        public static void AddRange<T,K>(this ObservableCollection<Grouping<T,K>> collection, IEnumerable<T> items, string propertyName)
        {
            if (typeof(T).GetRuntimeProperties().All(propertyInfo => propertyInfo.Name != propertyName))
            {
                throw new ArgumentException(String.Format("Type '{0}' does not have a property named '{1}'", typeof(T).Name, propertyName));
            }

            var groupings = items.GroupBy(t => t.GetType().GetRuntimeProperties().Single(propertyInfo => propertyInfo.Name == propertyName).GetValue(t, null));

            collection.AddRange(groupings.Select(grouping => new Grouping<T,K>(grouping, (K)grouping.Key)));
        }
    }
}

