using System;
using System.ComponentModel;
using System.Windows.Media;
using MobileCRMAndroid.Renderers;
using Newtonsoft.Json;
using Xamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Point = System.Windows.Point;

[assembly: ExportRenderer(typeof(MobileCRM.CustomControls.SignaturePad), typeof(SignaturePadRenderer))]
namespace MobileCRMAndroid.Renderers
{

  public class SignaturePadRenderer : ViewRenderer<MobileCRM.CustomControls.SignaturePad, SignaturePadView>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.CustomControls.SignaturePad> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;

      var signaturePad = new SignaturePadView()
      {
        BackgroundColor = Colors.White,
        StrokeColor = Colors.Black
      };

      if(!string.IsNullOrWhiteSpace(Element.DefaultPoints))
      {
        try
        {
          signaturePad.LoadPoints(JsonConvert.DeserializeObject<Point[]>(Element.DefaultPoints));
        }
        catch(Exception ex)
        {

        }
      }

      Element.GetPointString = () =>
      {
        if (signaturePad.Points == null)
          return string.Empty;


        return JsonConvert.SerializeObject(signaturePad.Points);
      };

      this.Element.GetImageEvent += Element_GetImageEvent;

      SetNativeControl(signaturePad);

      
    }

    void Element_GetImageEvent(object sender, EventArgs e)
    {
      //save image here?
    }
    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);
      if (this.Element == null || this.Control == null)
        return;
    }
  }
}