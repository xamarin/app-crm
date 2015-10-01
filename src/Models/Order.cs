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
using System;

// The model class files are hared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

#if SERVICE
using Microsoft.WindowsAzure.Mobile.Service;
#endif

namespace XamarinCRM.Models
{
    public class Order : 
#if SERVICE
        EntityData
#else
        BaseModel
#endif
    {
        public Order()
        {
            AccountId = string.Empty;

            //New orders default to open status. 
            IsOpen = true;
            Item = string.Empty;
            OrderDate = DateTime.UtcNow;
            ClosedDate = null; // Is never shown unless order is closed, in which case this should have a sane value.
            DueDate = DateTime.UtcNow.AddDays(7);
            Price = 0;
        }

        public bool IsOpen { get; set; }
        public string AccountId { get; set; }
        public double Price { get; set; }
        public string Item { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }

#if !SERVICE
        public string Status
        {
            get { return (IsOpen) ? "Open Orders" : "Delivered Orders"; }

        }
#endif
    }
}
