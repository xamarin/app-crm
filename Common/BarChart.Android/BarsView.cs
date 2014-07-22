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
  class BarsView : View
  {
    const float baseValue = 0;

    #region DPI dependent

    float topPadding;
    float minBarsAreaHeight;
    float barOuterBorderWidth;
    float barInnerBorderWidth;
    float captionFontSize;
    float captionTopMargin;
    float captionShadowRadius;
    float captionShadowDx;
    float captionShadowDy;
    float legendFontSize;
    float legendOffset;
    float barWidth;
    float barInterval;
    float zeroBarHeight;

    #endregion

    static readonly Color barOuterBorderColor = Color.Black;
    static readonly Color barInnerBorderColor = new Color(255, 255, 255, 127);
    static readonly Color captionInnerColor = Color.Black;
    static readonly Color captionOuterColor = Color.White;
    static readonly Color captionShadowColor = new Color(255, 255, 255, 181);
    Color barColor;
    Paint barPaint;
    Paint barInnerBorderPaint;
    Paint barOuterBorderPaint;
    Paint captionInnerPaint;
    Paint captionOuterPaint;
    Paint legendPaint;
    float? minValue;
    float? maxValue;
    int? downedBarIndex;
    BarModel[] barModels;
    RectF[] barRects;
    RectF[] barInnerBorderRects;
    PointF[] captionInnerPoints;
    PointF[] captionOuterPoints;
    PointF[] legendPoints;
    RectF[] clickableRects;

    public BarsView(Context context, Color legendColor) :
      base(context)
    {
      Initialize(legendColor);
    }

    public event EventHandler<BarClickEventArgs> BarClick;

    #region Appearence Properties

    public float BarWidth
    {
      get { return barWidth; }
      set
      {
        if (barWidth != value)
        {
          barWidth = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public float BarOffset
    {
      get { return barInterval; }
      set
      {
        if (barInterval != value)
        {
          barInterval = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public Color BarColor
    {
      get { return barColor; }
      set
      {
        if (value != barColor)
        {
          barColor = value;
          PostInvalidate();
        }
      }
    }

    public float CaptionFontSize
    {
      get { return captionFontSize; }
      set
      {
        if (captionFontSize != value)
        {
          captionFontSize = value;
          captionInnerPaint.TextSize = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public Color BarCaptionInnerColor
    {
      get { return captionInnerPaint.Color; }
      set
      {
        if (value != captionInnerPaint.Color)
        {
          captionInnerPaint.Color = value;
          PostInvalidate();
        }
      }
    }

    public Color BarCaptionOuterColor
    {
      get { return captionOuterPaint.Color; }
      set
      {
        if (value != captionOuterPaint.Color)
        {
          captionOuterPaint.Color = value;
          PostInvalidate();
        }
      }
    }

    public float TopPadding
    {
      get { return topPadding; }
      set
      {
        if (topPadding != value)
        {
          topPadding = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public float LegendOffset
    {
      get { return legendOffset; }
      set
      {
        if (value != legendOffset)
        {
          legendOffset = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public float LegendFontSize
    {
      get { return legendFontSize; }
      set
      {
        if (legendFontSize != value)
        {
          legendFontSize = value;
          legendPaint.TextSize = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public Color LegendColor
    {
      get { return legendPaint.Color; }
      set { legendPaint.Color = value; }
    }

    #endregion

    public float? MinValue
    {
      get { return minValue; }
      set
      {
        if (minValue != value)
        {
          minValue = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public float? MaxValue
    {
      get { return maxValue; }
      set
      {
        if (maxValue != value)
        {
          maxValue = value;
          PrepareDrawParams();
          PostInvalidate();
        }
      }
    }

    public float ResolvedMinValue { get; private set; }

    public float ResolvedMaxValue { get; private set; }

    public IEnumerable<BarModel> ItemsSource
    {
      get { return barModels; }
      set
      {
        if (value == null)
          throw new ArgumentNullException("data");
        barModels = value.ToArray();
        PrepareDrawParams();
        RequestLayout();
        PostInvalidate();
      }
    }

    public override bool OnTouchEvent(MotionEvent e)
    {
      if (e.Action == MotionEventActions.Down)
        downedBarIndex = FindBar(e.RawX, e.RawY);
      else if (e.Action == MotionEventActions.Up && downedBarIndex.HasValue)
      {
        var upedBarIndex = FindBar(e.RawX, e.RawY);
        if (upedBarIndex == downedBarIndex)
          OnBarClick(downedBarIndex.Value);
        downedBarIndex = null;
      }

      return base.OnTouchEvent(e);
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
      if (HasBars())
      {
        int minw = (int)FloatMath.Floor(barRects.Last().Right + barInterval);
        int w = ResolveSize(minw, widthMeasureSpec);

        int minh = (int)FloatMath.Floor(topPadding + minBarsAreaHeight + legendOffset + legendFontSize);
        int h = ResolveSize(minh, heightMeasureSpec);

        SetMeasuredDimension(w, h);
      }
      else
      {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
      }
    }

    protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
    {
      base.OnSizeChanged(w, h, oldw, oldh);

      PrepareDrawParams(h);
    }

    protected override void OnDraw(Canvas canvas)
    {
      base.OnDraw(canvas);
      if (HasBars())
      {
        for (int i = 0; i < barModels.Length; i++)
        {
          barPaint.Color = barModels[i].Color ?? barColor;
          canvas.DrawRect(barRects[i], barPaint);
          if (barInnerBorderRects[i] != null)
            canvas.DrawRect(barInnerBorderRects[i], barInnerBorderPaint);
          canvas.DrawRect(barRects[i], barOuterBorderPaint);

          if (captionInnerPoints[i] != null)
            canvas.DrawText(barModels[i].ValueCaption, captionInnerPoints[i].X, captionInnerPoints[i].Y, captionInnerPaint);
          if (captionOuterPoints[i] != null)
            canvas.DrawText(barModels[i].ValueCaption, captionOuterPoints[i].X, captionOuterPoints[i].Y, captionOuterPaint);

          if (legendPoints[i] != null)
            canvas.DrawText(barModels[i].Legend, legendPoints[i].X, legendPoints[i].Y, legendPaint);
        }
      }
    }

    void PrepareDrawParams()
    {
      PrepareDrawParams(Height);
    }

    void PrepareDrawParams(float height)
    {
      ResolveExtremums();

      if (HasBars())
      {
        float barsHeight = height - topPadding - legendOffset - legendFontSize;
        PrepareBarRects(barInterval, topPadding, barsHeight);
        PrepareBarInnerBorderRects();
        PrepareCaptionPoints();
        PrepareClickableRects();
        PrepareLegendPoints(height);
      }
    }

    void ResolveExtremums()
    {
      ResolvedMinValue = MinValue ?? ResolveAutoMinValue();
      ResolvedMaxValue = MaxValue ?? ResolveAutoMaxValue();
    }

    float ResolveAutoMinValue()
    {
      float min = barModels != null ? barModels.Min(m => m.Value) : baseValue;
      return min;
    }

    float ResolveAutoMaxValue()
    {
      float max = barModels != null ? barModels.Max(m => m.Value) : baseValue;
      return max;
    }

    void PrepareBarRects(float x, float y, float height)
    {
      float min = ResolvedMinValue;
      float max = ResolvedMaxValue;

      float normalizedBaseLine = Math.Max(baseValue.Normalize(min, max), 0);
      float bottom = y + (1 - normalizedBaseLine) * height;

      barRects = new RectF[barModels.Length];

      for (int i = 0; i < barModels.Length; i++)
      {
        float left = x + i * (barWidth + barInterval);
        float right = left + barWidth;

        float top = y + (1 - barModels[i].Value.Normalize(min, max)) * height;

        if (top == bottom)
        {
          top -= zeroBarHeight / 2;
          bottom += zeroBarHeight / 2;
        }

        barRects[i] = new RectF(left, Math.Min(top, bottom), right, Math.Max(top, bottom));
      }
    }

    void PrepareBarInnerBorderRects()
    {
      barInnerBorderRects = new RectF[barRects.Length];

      for (int i = 0; i < barRects.Length; i++)
      {
        if (barRects[i].Height() > zeroBarHeight)
        {
          barInnerBorderRects[i] = new RectF(barRects[i]);
          barInnerBorderRects[i].Inset(barInnerBorderWidth, barInnerBorderWidth);
        }
      }
    }

    void PrepareCaptionPoints()
    {
      captionInnerPoints = new PointF[barModels.Length];
      captionOuterPoints = new PointF[barModels.Length];
      var fontMetrics = captionInnerPaint.GetFontMetrics();
      float fontAscent = fontMetrics.Ascent;
      float fontDescent = fontMetrics.Descent;

      for (int i = 0; i < barModels.Length; i++)
      {
        if (!barModels[i].ValueCaptionHidden && barModels[i].ValueCaption != null)
        {
          if (captionFontSize + captionShadowDy + captionShadowRadius + captionTopMargin < barRects[i].Height())
          {
            float y = barModels[i].Value >= baseValue ?
                          barRects[i].Top + captionTopMargin - fontAscent :
                          barRects[i].Bottom - (fontDescent + captionShadowDy + captionShadowRadius);
            captionInnerPoints[i] = new PointF(barRects[i].CenterX(), y);
          }
          else
          {
            float y = barModels[i].Value >= baseValue ?
              barRects[i].Top - fontDescent :
              barRects[i].Bottom - fontAscent + captionTopMargin;
            captionOuterPoints[i] = new PointF(barRects[i].CenterX(), y);
          }
        }
      }
    }

    void PrepareClickableRects()
    {
      clickableRects = new RectF[barModels.Length];

      for (int i = 0; i < barModels.Length; i++)
      {
        clickableRects[i] = new RectF(barRects[i]);

        if (captionOuterPoints[i] != null)
        {
          if (barModels[i].Value >= baseValue)
            clickableRects[i].Top -= captionFontSize;
          else
            clickableRects[i].Bottom += captionFontSize + captionTopMargin;
        }
      }
    }

    void PrepareLegendPoints(float legendBottom)
    {
      legendPoints = new PointF[barModels.Length];

      if (LegendFontSize != 0)
      {
        float y = legendBottom - legendPaint.GetFontMetrics().Descent;

        for (int i = 0; i < barModels.Length; i++)
        {
          if (barModels[i].Legend != null)
            legendPoints[i] = new PointF(barRects[i].CenterX(), y);
        }
      }
    }

    int? FindBar(float rawX, float rawY)
    {
      int[] locationOnScreen = new int[2];
      GetLocationOnScreen(locationOnScreen);

      float x = rawX - locationOnScreen[0];
      float y = rawY - locationOnScreen[1];

      for (int i = 0; i < barRects.Length; i++)
      {
        if (clickableRects[i].Contains(x, y))
          return i;
      }

      return null;
    }

    void OnBarClick(int clickedBarIndex)
    {
      if (BarClick != null)
      {
        var bar = barModels[clickedBarIndex];
        BarClick(this, new BarClickEventArgs(bar));
      }
    }

    bool HasBars()
    {
      return barModels != null && barModels.Any();
    }

    void Initialize(Color legendColor)
    {
      barColor = new Color(62, 181, 227);
      Clickable = true;
      InitilizeDpiDependentParams();
      InitializePaints(legendColor);
    }

    void InitilizeDpiDependentParams()
    {
      float density = Resources.DisplayMetrics.Density;

      this.topPadding = 0 * density;
      this.barWidth = 40 * density;
      this.barInterval = 10 * density;
      this.zeroBarHeight = 2 * density;
      this.minBarsAreaHeight = 50 * density;
      this.barOuterBorderWidth = 1 * density;
      this.barInnerBorderWidth = 1 * density;
      this.captionFontSize = 16 * density;
      this.captionTopMargin = 4 * density;
      this.captionShadowRadius = 1 * density;
      this.captionShadowDx = 0 * density;
      this.captionShadowDy = 1 * density;
      this.legendFontSize = 0 * density;
      this.legendOffset = 0 * density;
    }

    void InitializePaints(Color legendColor)
    {
      barPaint = new Paint(PaintFlags.AntiAlias);
      barPaint.SetStyle(Paint.Style.Fill);
      barPaint.Color = barColor;

      barOuterBorderPaint = new Paint(PaintFlags.AntiAlias);
      barOuterBorderPaint.SetStyle(Paint.Style.Stroke);
      barOuterBorderPaint.Color = barOuterBorderColor;
      barOuterBorderPaint.StrokeWidth = barOuterBorderWidth;

      barInnerBorderPaint = new Paint(PaintFlags.AntiAlias);
      barInnerBorderPaint.SetStyle(Paint.Style.Stroke);
      barInnerBorderPaint.Color = barInnerBorderColor;
      barInnerBorderPaint.StrokeWidth = barInnerBorderWidth;

      captionInnerPaint = new Paint(PaintFlags.AntiAlias);
      captionInnerPaint.SetStyle(Paint.Style.FillAndStroke);
      captionInnerPaint.SetShadowLayer(captionShadowRadius, captionShadowDx, captionShadowDy, captionShadowColor);
      captionInnerPaint.TextSize = captionFontSize;
      captionInnerPaint.TextAlign = Paint.Align.Center;
      captionInnerPaint.Color = captionInnerColor;

      captionOuterPaint = new Paint(PaintFlags.AntiAlias);
      captionOuterPaint.SetStyle(Paint.Style.FillAndStroke);
      captionOuterPaint.TextSize = captionFontSize;
      captionOuterPaint.TextAlign = Paint.Align.Center;
      captionOuterPaint.Color = captionOuterColor;

      legendPaint = new Paint(PaintFlags.AntiAlias);
      legendPaint.SetStyle(Paint.Style.FillAndStroke);
      legendPaint.TextSize = legendFontSize;
      legendPaint.TextAlign = Paint.Align.Center;
      legendPaint.Color = legendColor;
    }
  }
}

