using ImageMagick;
using System;
using System.Drawing;
using System.IO;

namespace starbase_nexus_api.Services.Media
{
    public class ImageService : IImageService
    {
        public void GenerateThumbnail(string sourcePath, string destinationPath)
        {
            if (!File.Exists(destinationPath))
            {
                FileStream stream = File.OpenRead(sourcePath);

                Image newImage = GetResizedImage(100, stream);
                newImage.Save(destinationPath);
            }
        }

        public Image GetResizedImage(int width, Stream resourceImage)
        {
            Image image = Image.FromStream(resourceImage);
            decimal ratio = Convert.ToDecimal(image.Height) / Convert.ToDecimal(image.Width);
            int relativeHeight = Convert.ToInt32(Math.Round(width * ratio));
            Image thumb = image.GetThumbnailImage(width, relativeHeight, () => false, IntPtr.Zero);

            return thumb;
        }

        public void OptimizeImage(string sourcePath)
        {
            FileInfo fileInfo = new FileInfo(sourcePath);
            Console.WriteLine(sourcePath + " => Bytes after upload: " + fileInfo.Length);
            MagickImage image = new MagickImage(sourcePath);

            image.Write(sourcePath);
            fileInfo.Refresh();
            Console.WriteLine(sourcePath + " => Bytes after resave: " + fileInfo.Length);

            ImageOptimizer optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(fileInfo);
            fileInfo.Refresh();
            Console.WriteLine(sourcePath + " => Bytes after optimization:  " + fileInfo.Length);
        }
    }
}
