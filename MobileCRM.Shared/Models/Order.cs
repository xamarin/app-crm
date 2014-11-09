using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
    public class Order : BaseModel
    {
        public static string COMBO = "Combo";
        public static string PAPER = "Paper";
        public static string INK = "Ink";
        public static string PRINTER = "Printer";
        public static string SCANNER = "Scanner";

      public Order()
        : base()
      {
        AccountId = string.Empty;

        //New orders default to open status. 
        IsOpen = true;

        Item = ItemTypes[0];
        //ItemLevel = ItemLevels[0];
        //Discount = 0;
        Signature = string.Empty;
        OrderDate = DateTime.Today;
        ClosedDate = DateTime.Today;
        DueDate = DateTime.Today.AddDays(7);
      }

      [JsonProperty(PropertyName="is_open")]
      public bool IsOpen { get; set; }

      [JsonProperty(PropertyName = "account_id")]
      public string AccountId { get; set; }

      [JsonProperty(PropertyName="price")]
      public int Price { get; set; }


      [JsonProperty(PropertyName = "discount")]
      public int Discount { get; set; }

      [JsonProperty(PropertyName="item")]
      public string Item { get; set; }


      [JsonProperty(PropertyName = "item_level")]
      public string ItemLevel { get; set; }


      [JsonProperty(PropertyName="signature_points")]
      public string Signature { get; set; }


      [JsonProperty(PropertyName="order_date")]
      public DateTime OrderDate { get; set; }


      [JsonProperty(PropertyName="due_date")]
      public DateTime DueDate { get; set; }


      [JsonProperty(PropertyName = "closed_date")]
      public DateTime ClosedDate { get; set; }

      [JsonIgnore]
      public string Quote
      {
          get { return Price.ToString(); }
      }

      [JsonIgnore]
      //public static string[] ItemTypes = new string[] { "Paper", "Ink", "Printer", "Scanner", "Combo" };
      public static string[] ItemTypes = new string[] { PAPER, INK, PRINTER, SCANNER, COMBO };


      [JsonIgnore]
      public static string[] ItemLevels = new string[] { "Individual", "Business", "Enterprise" };

      [JsonIgnore]
      public string Description
      {
        get
        {
          return "Price Quote: $" + Price + " | Due: " + DueDate.ToString("d"); 
        }
      }

      [JsonIgnore]
      public string HistoryDescription
      {
        get
        {
          return "Total: $" + Price + " | Closed: " + ClosedDate.ToString("d");
        }
      }
   

    }
}
