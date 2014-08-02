using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using MobileCRMAndroid.Renderers;
using System.ComponentModel;

using MobileCRM.Shared.CustomControls;
using Xamarin.Controls;

[assembly: ExportRenderer(typeof(MobileCRM.Shared.CustomControls.SignaturePad), typeof(SignaturePadRenderer))]
namespace MobileCRMAndroid.Renderers
{

  public class SignaturePadRenderer : ViewRenderer<MobileCRM.Shared.CustomControls.SignaturePad, SignaturePadView>
  {
    protected override void OnElementChanged(ElementChangedEventArgs<MobileCRM.Shared.CustomControls.SignaturePad> e)
    {
      base.OnElementChanged(e);
      if (e.OldElement != null || this.Element == null)
        return;

      var signaturePad = new SignaturePadView()
      {
        BackgroundColor = System.Windows.Media.Colors.White,
        StrokeColor = System.Windows.Media.Colors.Black
      };

      if(!string.IsNullOrWhiteSpace(Element.DefaultPoints))
      {
        try
        {
          signaturePad.LoadPoints(Newtonsoft.Json.JsonConvert.DeserializeObject<System.Windows.Point[]>(Element.DefaultPoints));
        }
        catch(Exception ex)
        {

        }
      }

      Element.GetPointString = () =>
      {
        if (signaturePad.Points == null)
          return string.Empty;


        return Newtonsoft.Json.JsonConvert.SerializeObject(signaturePad.Points);
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