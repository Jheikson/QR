using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Mvc;
using System.Drawing;

namespace QRGenerator.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            GeneratePDF("C:\\Users\\Jheikson\\Desktop", "PDF_QR", "https://localhost:44371/siniestro/83943424");
            return View();
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private static void GeneratePDF(string ruta, string nombreArchivo, string qrText)
        {
            Document doc = new Document(PageSize.A4);
            var output = new FileStream($"{ruta}\\{nombreArchivo}.pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);

            doc.Open();
            BarcodeQRCode qrCode = new BarcodeQRCode(qrText, 100, 100, null);
            var image = qrCode.GetImage();
            image.ScaleToFitHeight = false;
            //image.SetAbsolutePosition(400f, 700f);
            image.SetDpi(600, 600);
            doc.Add(image);

            doc.Close();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}