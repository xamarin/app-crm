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

using Newtonsoft.Json;
using System;
using System.Linq;

namespace XamarinCRM.Models
{
    public class Account : BaseModel
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
