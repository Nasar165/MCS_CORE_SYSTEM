using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Components
{
    public class ImageHandler
    {
        public readonly string DirectoryPath = $"{Directory.GetCurrentDirectory()}/images/";

        public ImageHandler()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
        }

        public bool FileIsImage(IFormFile file)
        {
            switch (file.ContentType.ToLower())
            {
                case "image/png":
                    return true;
                case "image/jpeg":
                    return true;
                case "image/jpg":
                    return true;
                case "image/webp":
                    return true;
                default:
                    return false;
            }
        }

        public bool FileNotLargerThan(uint size, IFormFile image)
            => image.Length < size ? true : false;

        public bool FileIsImageAndLessThan2MB(IFormFile image)
            => FileNotLargerThan(2000000, image) && FileIsImage(image);

        private Image ResizeImage(Image img, int width, int height)
        {
            var size = new Size(width, height);
            var newImage = new Bitmap(size.Width, size.Height);
            using (var graphic = Graphics.FromImage((Bitmap)newImage))
            {
                graphic.InterpolationMode = InterpolationMode.Low;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(img, new Rectangle(Point.Empty, size));
            }
            return newImage;
        }

        private byte[] convertToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public byte[] ResizeImage(byte[] image, int width, int height)
        {

            using (var memoryStream = new MemoryStream(image))
            {
                using (var img = Image.FromStream(memoryStream))
                {
                    return convertToByteArray(ResizeImage(img, width, height));
                }
            }
        }
    }
}