using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BarChart
{
	class LevelsView : View
	{
		#region DPI dependent

		float minLevelLinesAreaWidth;
		float barsOffset;
		float levelLineWidth;
		float bottomPadding;
		float legendFontSize;

		#endregion

		static readonly Color levelColor = new Color (65, 67, 71);
		bool gridHidden = false;
		bool legendHidden = false;
		Paint legendPaint;
		Paint levelPaint;
		LinkedList<LevelModel> levels;

		public LevelsView (Context context, Color legendColor) :
			base (context)
		{
			Initialize (legendColor);
		}

		public int LevelLinesLeft { get; private set; }

		#region Appearence Properties

		public float BottomPadding {
			get { return bottomPadding; }
			set {
				if (bottomPadding != value) {
					bottomPadding = value;
					PrepareDrawParams ();
					PostInvalidate ();
				}
			}
		}

		public float LegendFontSize {
			get { return legendFontSize; }
			set {
				if (legendFontSize != value) {
					legendFontSize = value;
					legendPaint.TextSize = value;
					PrepareDrawParams ();
					PostInvalidate ();
				}
			}
		}

		public Color LegendColor {
			get { return legendPaint.Color; }
			set {
				if (value != legendPaint.Color) {
					legendPaint.Color = value;
					PostInvalidate ();
				}
			}
		}

		public bool GridHidden {
			get { return gridHidden; }
			set {
				if (value != gridHidden) {
					gridHidden = value;
					PostInvalidate ();
				}
			}
		}

		public bool LegendHidden {
			get { return legendHidden; }
			set {
				if (value != legendHidden) {
					legendHidden = value;
					PrepareDrawParams ();
					PostInvalidate ();
				}
			}
		}

		#endregion

		public float MinValue { get; private set; }

		public float MaxValue { get; private set; }

		public void ClearLevels ()
		{
			levels.Clear ();
		}

		public void AddLevel (float value, string legend = null)
		{
			levels.AddLast (new LevelModel (value, legend ?? value.ToString ()));
			PrepareDrawParams ();
		}

		public void SetExtremums (float min, float max)
		{
			MinValue = min;
			MaxValue = max;
			PrepareDrawParams ();
		}

		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			if (VisibleLevels.Any ()) {
				float legendRight = VisibleLevels
					.Select (x => x.LegendPoint)
					.Max (x => x.X);

				int minw = (int)FloatMath.Floor (legendRight + barsOffset + minLevelLinesAreaWidth);
				int w = ResolveSize (minw, widthMeasureSpec);

				int minh = (int)FloatMath.Floor (2 * legendFontSize + bottomPadding);
				int h = ResolveSize (minh, heightMeasureSpec);

				SetMeasuredDimension (w, h);
			} else {
				base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
			}
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);

			PrepareDrawParams (w, h);
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			foreach (var lvl in VisibleLevels) {
				if (!GridHidden) {
					canvas.DrawRect (lvl.LineRect, levelPaint);
				}
				if (!legendHidden) {
					canvas.DrawText (lvl.Legend, lvl.LegendPoint.X, lvl.LegendPoint.Y, legendPaint);
				}
			}
		}

		private void PrepareDrawParams ()
		{
			PrepareDrawParams (Width, Height);
		}

		void PrepareDrawParams (int width, int height)
		{
			float maxLegendWidth = legendHidden ? 0 : VisibleLevels
				.Select (x => x.Legend)
				.Select (x => legendPaint.MeasureText (x))
				.DefaultIfEmpty ()
				.Max ();

			LevelLinesLeft = maxLegendWidth != 0 ? (int)FloatMath.Floor (maxLegendWidth + barsOffset) : 0;
			float linesAreaTop = legendFontSize / 2;
			float linesAreaHeight = height - bottomPadding - legendFontSize;
			float legendFontAscent = legendPaint.GetFontMetrics ().Ascent;

			foreach (var lvl in VisibleLevels) {
				float normalizedValue = lvl.Value.Normalize (MinValue, MaxValue);
				float top = linesAreaTop + (1 - normalizedValue) * linesAreaHeight;
				lvl.LineRect = new RectF (LevelLinesLeft, top, width, top + levelLineWidth);
				lvl.LegendPoint = new PointF (maxLegendWidth, top - legendFontSize / 2 - legendFontAscent);
			}
		}

		IEnumerable<LevelModel> VisibleLevels {
			get {
				return levels.Where (x => x.Value >= MinValue && x.Value <= MaxValue);
			}
		}

		void Initialize (Color legendColor)
		{
			this.levels = new LinkedList<LevelModel> ();
			InitilizeDpiDependentParams ();
			InitializePaints (legendColor);
		}

		void InitilizeDpiDependentParams ()
		{
			float density = Resources.DisplayMetrics.Density;

			this.minLevelLinesAreaWidth = 50 * density;
			this.barsOffset = 10 * density;
			this.levelLineWidth = 1 * density;
			this.bottomPadding = 0 * density;
			this.legendFontSize = 0 * density;
		}

		void InitializePaints (Color legendColor)
		{
			legendPaint = new Paint (PaintFlags.AntiAlias);
			legendPaint.SetStyle (Paint.Style.FillAndStroke);
			legendPaint.TextSize = legendFontSize;
			legendPaint.TextAlign = Paint.Align.Right;
			legendPaint.Color = legendColor;
			
			levelPaint = new Paint (PaintFlags.AntiAlias);
			levelPaint.SetStyle (Paint.Style.Fill);
			levelPaint.Color = levelColor;
		}

		class LevelModel
		{
			public readonly float Value;
			public readonly string Legend;
			public RectF LineRect;
			public PointF LegendPoint;

			public LevelModel (float value, string legend)
			{
				Value = value;
				Legend = legend;
			}
		}
	}
}

