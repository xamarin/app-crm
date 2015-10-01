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
using System.Linq;
using Newtonsoft.Json;

// The model class files are hared between the mobile and service projects. 
// If EntityData were compatible with PCL profile 78, the models could be in a PCL.

#if SERVICE
using Microsoft.WindowsAzure.Mobile.Service;
#endif

namespace XamarinCRM.Models
{
    public class Account : 
#if SERVICE
        EntityData
#else
        BaseModel
#endif
    {
        public Account()
        {
            FirstName = LastName = Company = Street = Unit = City = PostalCode = State = Country = Phone = JobTitle = Email = string.Empty;
            Industry = IndustryTypes[0];
            OpportunityStage = OpportunityStages[0];
            IsLead = true;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsLead { get; set; }
        public string Industry { get; set; }
        public double OpportunitySize { get; set; }
        public string OpportunityStage { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public static string[] IndustryTypes = { "None Selected", "Aerospace", "Education", "Electrical", "Entertainment", "Financial Services", "Logistic", "Healthcare", "Manufacturing", "Retail", "Other" };

        [JsonIgnore]
        public static string[] OpportunityStages = { "10% - Prospect", "50% - Value Proposition", "75% - Proposal", "100% - Closed" };


#if !SERVICE
        [JsonIgnore]
        public double OpportunityStagePercent
        {
            get
            {
                if (OpportunityStages.Length != OpportunityStagePercentages.Length)
                    throw new IndexOutOfRangeException("The OpportunityStages array and the OpportunityStagePercentages array must be of equal length.");

                double result = 0;

                for (int i = 0; i < OpportunityStages.Length; i++)
                {
                    if (OpportunityStage == OpportunityStages[i])
                    {
                        result = OpportunityStagePercentages[i];
                        break;
                    }
                }

                return result;
            }
        }
            
        [JsonIgnore]
        public int IndustryTypeCurrentIndex
        {
            get { return IndustryTypes.ToList().IndexOf(Industry); }
            set { Industry = IndustryTypes[value]; }
        }

        [JsonIgnore]
        public int OpportunityStageCurrentIndex
        {
            get { return OpportunityStages.ToList().IndexOf(OpportunityStage); }
            set { OpportunityStage = OpportunityStages[value]; }
        }

        [JsonIgnore]
        public static double[] OpportunityStagePercentages = { 10d, 50d, 75d, 100d };

        public string DisplayContact
        {
            get { return FirstName + " " + LastName; }
        }

        [JsonIgnore]
        public string AddressString
        {
            get
            {
                return string.Format(
                    "{0}{1} {2} {3} {4}",
                    Street,
                    !string.IsNullOrWhiteSpace(Unit) ? " " + Unit + "," : string.Empty + ",",
                    !string.IsNullOrWhiteSpace(City) ? City + "," : string.Empty,
                    State,
                    PostalCode);
            }
        }

        [JsonIgnore]
        public string DisplayName
        {
            get { return this.ToString(); }
        }

        [JsonIgnore]
        public string CityState
        {
            get { return City + ", " + State; }
        }

        [JsonIgnore]
        public string CityStatePostal
        {
            get { return CityState + " " + PostalCode; }
        }

        [JsonIgnore]
        public string StatePostal
        {
            get { return State + " " + PostalCode; }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

#endif
    }
}
