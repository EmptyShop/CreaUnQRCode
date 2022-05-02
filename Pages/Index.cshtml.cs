using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using QRCoder;
using System.Linq;

namespace CreaUnQRCode.Pages
{
    public class IndexModel : PageModel
    {
        public string Mensaje { get; set; }
        public Boolean acolor { get; set; }
        public string elQRCode { get; set; }

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            Mensaje = Request.Form[nameof(Mensaje)].ToString().Trim();
            acolor = Request.Form[nameof(acolor)].Contains("true");
            
            if (!String.IsNullOrEmpty(Mensaje))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(Mensaje, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);

                byte[] qrCodeAsBitmapByteArr;

                if (!acolor)
                {
                    qrCodeAsBitmapByteArr = qrCode.GetGraphic(10);
                }
                else
                {
                    qrCodeAsBitmapByteArr = qrCode.GetGraphic(10, "#336699", "#C0C0C0");
                }

                elQRCode = new String("data:image/png;base64," + Convert.ToBase64String(qrCodeAsBitmapByteArr));
            }   
        }
    }
}
