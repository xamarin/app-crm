using System;
using System.Globalization;

using Android.Graphics;
using Android.Util;

namespace BarChart {
	class BarChartAttributesParser {
		public readonly float BarWidth;
		public readonly float BarOffset;
		public readonly Color BarColor;
		public readonly float BarCaptionFontSize;
		public readonly Color BarCaptionInnerColor;
		public readonly Color BarCaptionOuterColor;
		public readonly float LegendFontSize;
		public readonly Color LegendColor;
		public readonly float? MinValue;
		public readonly float? MaxValue;

		public BarChartAttributesParser (IAttributeSet attributes, float defaultBarWidth, float defaultBarOffset, Color defaultBarColor,
		                                 float defaultBarCaptionFontSize, Color defaultBarCaptionInnerColor, Color defaultBarCaptionOuterColor,
		                                 float defaultLegendFontSize, Color defaultLegendColor)
		{
			BarWidth = ParseFloat (attributes, "bar_width", defaultBarWidth);
			BarOffset = ParseFloat (attributes, "bar_offset", defaultBarOffset);
			BarColor = ParseColor (attributes, "bar_color", defaultBarColor);
			BarCaptionFontSize = ParseFloat (attributes, "bar_caption_fontSize", defaultBarCaptionFontSize);
			BarCaptionInnerColor = ParseColor (attributes, "bar_caption_innerColor", defaultBarCaptionInnerColor);
			BarCaptionOuterColor = ParseColor (attributes, "bar_caption_outerColor", defaultBarCaptionOuterColor);
			LegendFontSize = ParseFloat (attributes, "legend_fontSize", defaultLegendFontSize);
			LegendColor = ParseColor (attributes, "legend_color", defaultLegendColor);
			MinValue = ParseNullableFloat (attributes, "min_value", null);
			MaxValue = ParseNullableFloat (attributes, "max_value", null);
		}

		float ParseFloat (IAttributeSet attributes, string attributeName, float defaultNumber)
		{
			return ParseNullableFloat (attributes, attributeName, defaultNumber, "Wrong {0} value '{1}'. Must be a number") ?? defaultNumber;
		}

		float? ParseNullableFloat (IAttributeSet attributes, string attributeName, float? defaultNumber)
		{
			return ParseNullableFloat (attributes, attributeName, defaultNumber, "Wrong {0} value '{1}'. Must be a number or empty");
		}

		float? ParseNullableFloat (IAttributeSet attributes, string attributeName, float? defaultNumber, string exceptionFormat)
		{
			var numberAttr = attributes.GetAttributeValue (null, attributeName);
			
			if (numberAttr == null)
				return defaultNumber;

			if (numberAttr == String.Empty)
				return null;

			float result;
			if (float.TryParse (numberAttr, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
				return result;
			else
				throw new ArgumentException (String.Format (exceptionFormat, attributeName, numberAttr));
		}

		Color ParseColor (IAttributeSet attributes, string attributeName, Color defaultColor)
		{
			var colorAttr = attributes.GetAttributeValue (null, attributeName);

			if (String.IsNullOrEmpty (colorAttr))
				return defaultColor;

			try {
				return Color.ParseColor (colorAttr);
			} catch (AndroidException) {
				throw new ArgumentException (String.Format ("Wrong {0} value '{1}'. Must be a color '#RRGGBB' or '#AARRGGBB'", attributeName, colorAttr));
			}
		}
	}
}

