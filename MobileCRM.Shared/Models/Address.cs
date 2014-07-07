using MobileCRM.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
  public class Address
  {
    public Address()
    {
      Street = Unit = City = PostalCode = State = Country = string.Empty;
    }
    public string Street { get; set; }
    public string Unit { get; set; }
    public string City { get; set; }
    [Display("Postal Code")]
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }


    public override string ToString()
    {
      return string.Format("{0}{1} {2} {3} {4}", Street, !string.IsNullOrWhiteSpace(Unit) ? Unit + "," : string.Empty, !string.IsNullOrWhiteSpace(City) ? City + "," : string.Empty, State, PostalCode);
    }
  }
}
