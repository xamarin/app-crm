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
  public class BarChartView : ViewGroup
  {
    const int autoLevelsCount = 5;
    static readonly Color defaultLegendColor = Color.White;
    bool autoLevelsEnabled = true;
    bool legendHidden = false;
    float legendFontSize;
    float bottomLegendOffset;
    LevelsView levelsView;
    BarsView barsView;
    HorizontalScrollView scrollView;

    public BarChartView(Context context) :
      base(context)
    {
      Initialize();
    }

    public BarChartView(Context context, IAttributeSet attrs) :
      base(context, attrs)
    {
      Initialize();

      var parser = new BarChartAttributesParser(attrs, BarWidth, BarOffset, BarColor,
                                                BarCaptionFontSize, BarCaptionInnerColor, BarCaptionOuterColor,
                                                LegendFontSize, LegendColor);

      BarWidth = parser.BarWidth;
      BarOffset = parser.BarOffset;
      BarColor = parser.BarColor;
      BarCaptionFontSize = parser.BarCaptionFontSize;
      BarCaptionInnerColor = parser.BarCaptionInnerColor;
      BarCaptionOuterColor = parser.BarCaptionOuterColor;
      LegendFontSize = parser.LegendFontSize;
      LegendColor = parser.LegendColor;
      MinimumValue = parser.MinValue;
      MaximumValue = parser.MaxValue;
    }

    public BarChartView(Context context, IAttributeSet attrs, int defStyle) :
      base(context, attrs, defStyle)
    {
      Initialize();
    }

    public event EventHandler<BarClickEventArgs> BarClick;

    #region Appearence Properties

    public float BarWidth
    {
      get { return barsView.BarWidth; }
      set { barsView.BarWidth = value; }
    }

    public float BarOffset
    {
      get { return barsView.BarOffset; }
      set { barsView.BarOffset = value; }
    }

    public Color BarColor
    {
      get { return barsView.BarColor; }
      set { barsView.BarColor = value; }
    }

    public float BarCaptionFontSize
    {
      get { return barsView.CaptionFontSize; }
      set { barsView.CaptionFontSize = value; }
    }

    public Color BarCaptionInnerColor
    {
      get { return barsView.BarCaptionInnerColor; }
      set { barsView.BarCaptionInnerColor = value; }
    }

    public Color BarCaptionOuterColor
    {
      get { return barsView.BarCaptionOuterColor; }
      set { barsView.BarCaptionOuterColor = value; }
    }

    public float LegendFontSize
    {
      get { return barsView.LegendFontSize; }
      set
      {
        if (value != legendFontSize)
        {
          legendFontSize = value;
          levelsView.LegendFontSize = value;
          if (!LegendHidden)
          {
            barsView.TopPadding = GetBarsViewTopPadding(value);
            barsView.LegendFontSize = value;
            levelsView.BottomPadding = GetLevelsViewBottomPadding(value, bottomLegendOffset);
          }
        }
      }
    }

    public Color LegendColor
    {
      get { return barsView.LegendColor; }
      set
      {
        barsView.LegendColor = value;
        levelsView.LegendColor = value;
      }
    }

    public bool GridHidden
    {
      get { return levelsView.GridHidden; }
      set { levelsView.GridHidden = value; }
    }

    public bool LegendHidden
    {
      get { return legendHidden; }
      set
      {
        if (value != legendHidden)
        {
          legendHidden = value;

          if (legendHidden)
          {
            barsView.LegendOffset = GetBarsViewTopPadding(legendFontSize);
            barsView.LegendFontSize = 0;
            levelsView.BottomPadding = 0;
          }
          else
          {
            barsView.LegendOffset = bottomLegendOffset;
            barsView.LegendFontSize = legendFontSize;
            levelsView.BottomPadding = GetLevelsViewBottomPadding(legendFontSize, bottomLegendOffset);
          }
        }
      }
    }

    public bool LevelsHidden
    {
      get { return levelsView.LegendHidden; }
      set
      {
        if (value != levelsView.LegendHidden)
        {
          levelsView.LegendHidden = value;
          RequestLayout();
        }
      }
    }

    #endregion

    public IEnumerable<BarModel> ItemsSource
    {
      get { return barsView.ItemsSource; }
      set
      {
        barsView.ItemsSource = value;
        levelsView.SetExtremums(barsView.ResolvedMinValue, barsView.ResolvedMaxValue);
        AddAutoLevelIndicatorsIfEnabled();
      }
    }

    public float? MinimumValue
    {
      get { return barsView.MinValue; }
      set
      {
        if (barsView.MinValue != value)
        {
          barsView.MinValue = value;
          levelsView.SetExtremums(barsView.ResolvedMinValue, barsView.ResolvedMaxValue);
          AddAutoLevelIndicatorsIfEnabled();
        }
      }
    }

    public float? MaximumValue
    {
      get { return barsView.MaxValue; }
      set
      {
        if (barsView.MaxValue != value)
        {
          barsView.MaxValue = value;
          levelsView.SetExtremums(barsView.ResolvedMinValue, barsView.ResolvedMaxValue);
          AddAutoLevelIndicatorsIfEnabled();
        }
      }
    }

    public bool AutoLevelsEnabled
    {
      get { return autoLevelsEnabled; }
      set
      {
        if (autoLevelsEnabled != value)
        {
          autoLevelsEnabled = value;
          AddAutoLevelIndicatorsIfEnabled();
          if (!autoLevelsEnabled)
            levelsView.ClearLevels();
        }
      }
    }

    public void AddLevelIndicator(float value, string title = null)
    {
      if (!autoLevelsEnabled)
        levelsView.AddLevel(value, title);
    }

    void AddAutoLevelIndicatorsIfEnabled()
    {
      if (autoLevelsEnabled)
      {
        levelsView.ClearLevels();

        for (int i = 0; i < autoLevelsCount; i++)
        {
          float normalizedValue = (float)i / (autoLevelsCount - 1);
          float value = levelsView.MinValue + (levelsView.MaxValue - levelsView.MinValue) * normalizedValue;
          levelsView.AddLevel(value);
        }
      }
      levelsView.PostInvalidate();
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
      base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

      levelsView.Measure(widthMeasureSpec, heightMeasureSpec);

      int width = MeasureSpec.GetSize(widthMeasureSpec);
      var widthMeasureMode = MeasureSpec.GetMode(widthMeasureSpec);

      scrollView.Measure(MeasureSpec.MakeMeasureSpec(width - levelsView.LevelLinesLeft, widthMeasureMode), heightMeasureSpec);

      SetMeasuredDimension(levelsView.MeasuredWidth, scrollView.MeasuredHeight);
    }

    protected override void OnLayout(bool changed, int l, int t, int r, int b)
    {
      int width = r - l;
      int height = b - t;

      levelsView.Layout(0, 0, width, height);

      scrollView.Layout(levelsView.LevelLinesLeft, 0, width, height);
    }

    float GetBarsViewTopPadding(float legendFontSize)
    {
      return legendFontSize / 2;
    }

    float GetLevelsViewBottomPadding(float legendFontSize, float bottomLegendOffset)
    {
      return bottomLegendOffset + legendFontSize / 2;
    }

    void Initialize()
    {
      float density = Resources.DisplayMetrics.Density;
      legendFontSize = 11 * density;
      bottomLegendOffset = 11 * density;

      barsView = new BarsView(Context, defaultLegendColor)
      {
        LayoutParameters = new HorizontalScrollView.LayoutParams(HorizontalScrollView.LayoutParams.FillParent, HorizontalScrollView.LayoutParams.FillParent),
        TopPadding = GetBarsViewTopPadding(legendFontSize),
        LegendOffset = bottomLegendOffset,
        LegendFontSize = legendFontSize
      };
      barsView.BarClick += HandleBarClick;

      scrollView = new HorizontalScrollView(Context);
      scrollView.AddView(barsView);

      levelsView = new LevelsView(Context, defaultLegendColor)
      {
        BottomPadding = GetLevelsViewBottomPadding(legendFontSize, bottomLegendOffset),
        LegendFontSize = legendFontSize
      };

      AddView(levelsView);
      AddView(scrollView);
    }

    void HandleBarClick(object sender, BarClickEventArgs e)
    {
      if (BarClick != null)
        BarClick(this, e);
    }
  }
}

