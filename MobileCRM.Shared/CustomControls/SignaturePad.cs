using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.CustomControls
{
    public class SignaturePad : View
    {
      
      public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create<SignaturePad, Color>(p => p.BackgroundColor, Color.White);
 
      public Color BackgroundColor
      {
        get { return (Color)GetValue(BackgroundColorProperty); }
        set { SetValue(BackgroundColorProperty, value); }
      }


      public static readonly BindableProperty StrokeColorProperty =
        BindableProperty.Create<SignaturePad, Color>(p => p.StrokeColor, Color.Black);

      public Color StrokeColor
      {
        get { return (Color)GetValue(StrokeColorProperty); }
        set { SetValue(StrokeColorProperty, value); }
      }


      public static readonly BindableProperty SignatureLineColorProperty =
        BindableProperty.Create<SignaturePad, Color>(p => p.SignatureLineColor, Color.Black);

      public Color SignatureLineColor
      {
        get { return (Color)GetValue(SignatureLineColorProperty); }
        set { SetValue(SignatureLineColorProperty, value); }
      }

      public static readonly BindableProperty SignaturePromptProperty =
        BindableProperty.Create<SignaturePad, string>(p => p.SignaturePrompt, string.Empty);

      public string SignaturePrompt
      {
        get { return (string)GetValue(SignaturePromptProperty); }
        set { SetValue(SignaturePromptProperty, value); }
      }



      public static readonly BindableProperty SignaturePromptColorProperty =
        BindableProperty.Create<SignaturePad, Color>(p => p.SignaturePromptColor, Color.Black);

      public Color SignaturePromptColor
      {
        get { return (Color)GetValue(SignaturePromptColorProperty); }
        set { SetValue(SignaturePromptColorProperty, value); }
      }


      public static readonly BindableProperty CaptionProperty =
        BindableProperty.Create<SignaturePad, string>(p => p.Caption, string.Empty);

      public string Caption
      {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
      }

      public static readonly BindableProperty CaptionColorProperty =
        BindableProperty.Create<SignaturePad, Color>(p => p.CaptionColor, Color.Black);

      public Color CaptionColor
      {
        get { return (Color)GetValue(CaptionColorProperty); }
        set { SetValue(CaptionColorProperty, value); }
      }

      public static readonly BindableProperty DefaultPointsProperty =
       BindableProperty.Create<SignaturePad, string>(p => p.DefaultPoints, string.Empty);

      public string DefaultPoints
      {
        get { return (string)GetValue(DefaultPointsProperty); }
        set { SetValue(DefaultPointsProperty, value); }
      }

      public Func<string> GetPointString { get; set; }



      public event EventHandler GetImageEvent;

      public void GetImage()
      {

        var handler = GetImageEvent;
        if (handler != null)
          handler(this, EventArgs.Empty);
      }	
    }
}
