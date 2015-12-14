// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using XamarinCRM.Models.Local;

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
            // If the specified propertyName does not exist on type T, throw an ArgumentException.
            if (typeof(T).GetRuntimeProperties().All(propertyInfo => propertyInfo.Name != propertyName))
            {
                throw new ArgumentException(String.Format("Type '{0}' does not have a property named '{1}'", typeof(T).Name, propertyName));
            }

            // Group the items in T by the different values of K.
            var groupings = items.GroupBy(t => t.GetType().GetRuntimeProperties().Single(propertyInfo => propertyInfo.Name == propertyName).GetValue(t, null));

            // Add new Grouping<T,K> items to the ObservableCollection<Grouping<T,K>> collection.
            collection.AddRange(groupings.Select(grouping => new Grouping<T,K>(grouping, (K)grouping.Key)));
        }
    }
}

