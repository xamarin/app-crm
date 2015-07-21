using Newtonsoft.Json;
using System;

namespace MobileCRM.Models
{
    public class Account : Contact
    {
        public Account()
            : base()
        {
            ContactId = Notes = string.Empty;
            Industry = IndustryTypes[0];
            OpportunityStage = OpportunityStages[0];
            OpportunityStagePercent = OpportunityStagePercentages[0];
        }

        [JsonProperty(PropertyName = "contactid")]
        public string ContactId { get; set; }

        [JsonProperty(PropertyName = "is_lead")]
        public bool IsLead { get; set; }

        [JsonProperty(PropertyName = "industry")]
        public string Industry { get; set; }

        [JsonProperty(PropertyName = "oppt_size")]
        public double OpportunitySize { get; set; }

        [JsonProperty(PropertyName = "oppt_stage")]
        public string OpportunityStage { get; set; }


        private double _OpportunityStagePercent;
        [JsonIgnore]
        public double OpportunityStagePercent
        { 
            get
            {
                if (OpportunityStages.Length != OpportunityStagePercentages.Length)
                    throw new IndexOutOfRangeException("The OpportunityStages array and the OpportunityStagePercentages array must be of equal lenght.");

                double result = 0;

                for (int i = 0; i < OpportunityStages.Length; i++)
                {
                    if (OpportunityStage == OpportunityStages[i])
                    {
                        result = OpportunityStagePercentages[i];
                        break;
                    }
                }

                _OpportunityStagePercent = result;

                return result;
            } 
            private set { _OpportunityStagePercent = value; }
        }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [JsonIgnore]
        public static string[] IndustryTypes = { "None Selected", "Aerospace", "Education", "Electrical", "Entertainment", "Financial Services", "Logistic", "Healthcare", "Manufacturing", "Retail", "Other" };

        [JsonIgnore]
        public static string[] OpportunityStages = { "None Selected", "10% - Prospect", "50% - Value Proposition", "75% - Proposal" };

        [JsonIgnore]
        public static double[] OpportunityStagePercentages = { 0d, 10d, 50d, 75d };

    }
}
