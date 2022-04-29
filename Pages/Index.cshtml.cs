using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Drawing;
using System.IO;
using QRCoder;

namespace CreaUnQRCode.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        public string Mensaje { get; set; }
        public string elQRCode { get; set; }

        public IndexModel(/*ILogger<IndexModel> logger*/)
        {
            //_logger = logger;
        }

        public void OnGet()
        {
            string dateTime = DateTime.Now.ToString("d", new CultureInfo("en-US"));
            ViewData["TimeStamp"] = dateTime;
        }

        public void OnPost()
        {
            Mensaje = Request.Form[nameof(Mensaje)].ToString().Trim();
            QRCodeGenerator qrGenerator=new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Mensaje, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(10);

            elQRCode = "data:image/png;base64," + Convert.ToBase64String(qrCodeAsBitmapByteArr);
        }

    }
}
