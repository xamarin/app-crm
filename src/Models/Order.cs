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

namespace XamarinCRM.Models
{
    public class Order : BaseModel

    {
        public Order()
        {
            AccountId = string.Empty;

            var now = DateTime.UtcNow;

            //New orders default to open status. 
            IsOpen = true;
            Item = string.Empty;
            OrderDate = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), DateTimeKind.Utc);
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
