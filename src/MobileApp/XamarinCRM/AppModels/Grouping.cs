//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinCRM.AppModels
{
    public class Grouping<T,K> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinCRM.Models.Grouping&lt;T,K&gt;"/> class.
        /// </summary>
        /// <param name="items">A collection of items of type T</param>
        /// <param name="key">A key of type K.</param>
        public Grouping(IEnumerable<T> items, K key)
        {
            Key = key;
            Items.AddRange(items);
        }
    }
}

