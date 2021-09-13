using System.Drawing;
using System.IO;

namespace starbase_nexus_api.Services.Media
{
    public interface IImageService
    {
        void GenerateThumbnail(string sourcePath, string destinationPath);
        Image GetResizedImage(int width, Stream resourceImage);
        void OptimizeImage(string sourcePath);
    }
}