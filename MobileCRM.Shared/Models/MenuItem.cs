using System;
using System.Collections.Generic;
using System.Text;

namespace MobileCRM.Shared.Models
{
  public enum MenuType
  {
    Dashboard,
    Accounts,
    Leads,
    Contacts,
    Catalog,
    Settings
  }

  public class MenuItem
  {
    public MenuItem()
    {
      MenuType = MenuType.Accounts;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public MenuType MenuType { get; set; }
  }
}
