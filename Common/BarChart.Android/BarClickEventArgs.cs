using System;

namespace BarChart
{
	public class BarClickEventArgs: EventArgs {
		public readonly BarModel Bar;
		
		public BarClickEventArgs (BarModel bar)
		{
			Bar = bar;
		}
	}
}

