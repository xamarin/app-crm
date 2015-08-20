using System;
using Newtonsoft.Json;

namespace XamarinCRM.Models
{
    public class Order : BaseModel
    {
        public Order() : base()
        {
            AccountId = string.Empty;

            //New orders default to open status. 
            IsOpen = true;

            Item = string.Empty;
            Signature = string.Empty;
            OrderDate = DateTime.Today;
            ClosedDate = DateTime.MinValue; // Is never shown unless order is closed, in which case this should have a sane value. Using MinValue to indicate a default value state.
            DueDate = DateTime.Today.AddDays(7);
            Price = 0;
        }

        [JsonProperty(PropertyName = "is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "discount")]
        public int Discount { get; set; }

        [JsonProperty(PropertyName = "item")]
        public string Item { get; set; }

        [JsonProperty(PropertyName = "item_level")]
        public string ItemLevel { get; set; }

        [JsonProperty(PropertyName = "signature_points")]
        public string Signature { get; set; }

        [JsonProperty(PropertyName = "order_date")]
        public DateTime OrderDate { get; set; }

        [JsonProperty(PropertyName = "due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty(PropertyName = "closed_date")]
        public DateTime ClosedDate { get; set; }

        [JsonIgnore]
        public string Quote
        {
            get { return Price.ToString(); }
        }
    }
}
