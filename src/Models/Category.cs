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

// The model class files are hared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

#if SERVICE
using Microsoft.WindowsAzure.Mobile.Service;
#endif

namespace XamarinCRM.Models
{
    public class Category : 
#if SERVICE
        EntityData
#else
        BaseModel
#endif
    {
        public Category()
        {
            Name = Description = ImageUrl = ParentCategoryId = string.Empty;
            Sequence = 0;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ParentCategoryId { get; set; }

        public bool HasSubCategories { get; set; }

        public int Sequence { get; set; }
    }
}
