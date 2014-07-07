using MobileCRM.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Pages.Leads
{
	public partial class LeadsDashboardView
	{
		public LeadsDashboardView ()
		{
			InitializeComponent ();
      var items = new List<BarItem>();
      items.Add(new BarItem { Name = "a", Value = 10 });
      items.Add(new BarItem { Name = "b", Value = 15 });
      items.Add(new BarItem { Name = "c", Value = 20 });
      items.Add(new BarItem { Name = "d", Value = 5 });
      items.Add(new BarItem { Name = "e", Value = 14 });
      Chart.Items = items;
		}
	}
}
