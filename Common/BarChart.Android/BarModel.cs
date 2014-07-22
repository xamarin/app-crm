using Android.Graphics;

namespace BarChart {
	public class BarModel {
		string valueCaption;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public float Value { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="BarChart.ChartModel"/> value caption hidden.
		/// </summary>
		/// <value>
		/// <c>true</c> if value caption hidden; otherwise, <c>false</c>.
		/// </value>
		public bool ValueCaptionHidden { get; set; }

		/// <summary>
		/// Gets or sets the value caption.
		/// </summary>
		/// <value>
		/// The value caption.
		/// </value>
		public string ValueCaption {
			get {
				return valueCaption ?? Value.ToString ();
			}
			set {
				valueCaption = value;
			}
		}

		/// <summary>
		/// Gets or sets the legend.
		/// </summary>
		/// <value>
		/// The legend.
		/// </value>
		public string Legend { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>
		/// The color.
		/// </value>
		public Color? Color { get; set; }
	}
}

