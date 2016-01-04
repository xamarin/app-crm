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

// The model class files are shared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

namespace XamarinCRM.Models
{
    public class Product : BaseModel
    {
        public Product()
        {
            Name = Description = ImageUrl = string.Empty;
            Price = 0;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        #if !SERVICE

        public string ThumbnailImageUrl 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(ImageUrl) || !ImageUrl.Contains("."))
                    return null;

                var index = ImageUrl.LastIndexOf('.');
                var name = ImageUrl.Substring(0, index);
                var extension = ImageUrl.Substring(index);
                return string.Format("{0}-thumb{1}", name, extension);
            }
        }

        #endif
    }
}
