using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MobileCRMAndroid.Renderers;
using System.ComponentModel;
using SignaturePad;
using MobileCRM.Shared.CustomControls;

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
        BackgroundColor = this.Element.BackgroundColor.ToUIColor(),
        StrokeColor = Element.StrokeColor.ToUIColor(),
        SignatureLineColor = Element.SignatureLineColor.ToUIColor(),
        
      };


      if (!string.IsNullOrWhiteSpace(Element.DefaultPoints))
      {
        try
        {
          signaturePad.LoadPoints(Newtonsoft.Json.JsonConvert.DeserializeObject<System.Drawing.PointF[]>(Element.DefaultPoints));
        }
        catch (Exception ex)
        {

        }
      }

      Element.GetPointString = () =>
      {
        if (signaturePad.Points == null)
          return string.Empty;

        return Newtonsoft.Json.JsonConvert.SerializeObject(signaturePad.Points);
      };

      signaturePad.SignaturePrompt.Text = Element.SignaturePrompt;
      signaturePad.SignaturePrompt.TextColor = (Element.SignaturePromptColor.ToUIColor());
      signaturePad.Caption.Text = Element.Caption;
      signaturePad.Caption.TextColor = (Element.CaptionColor.ToUIColor());


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