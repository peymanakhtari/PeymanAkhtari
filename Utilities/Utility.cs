using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace PeymanAkhtari.Utilities
{
    public class Utility
    {
        public static byte[] GetButeArrayFromIFormFile(IFormFile img, MemoryStream ms)
        {
            img.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string pic = "data:image/jpeg;base64," + Convert.ToBase64String(fileBytes);
            return Encoding.ASCII.GetBytes(pic);
        }
        public static byte[] resizeImage(IFormFile img, Size size, MemoryStream ms)
        {
            img.CopyTo(ms);
            Image imgToResize = Image.FromStream(ms);
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            var image = (System.Drawing.Image)b;
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
            var resutl = "data:image/jpeg;base64," + Convert.ToBase64String(xByte);
            return Encoding.ASCII.GetBytes(resutl);
        }
        public static string uploadImage(IFormFile img, string path)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + path);
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(fileStream);
            }
            return fileName;
        }
        public static string uploadImage(IFormFile img, string path,string fileName)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + path);
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(fileStream);
            }
            return fileName;
        }
        public static void deleteFile(string path,string filenme)
        {
            string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + path);
            if (filenme!=null)
            {
                string deletepath = Path.Combine(_path, filenme);
                FileInfo file = new FileInfo(deletepath);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
         
        }
    }
}
