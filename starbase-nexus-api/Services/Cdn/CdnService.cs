using Microsoft.Extensions.Options;
using starbase_nexus_api.Exceptions;
using starbase_nexus_api.Models.Cdn;
using starbase_nexus_api.Services.Media;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Cdn
{
    public class CdnService : ICdnService
    {
        private readonly IOptions<CdnOptions> _options;
        private readonly IImageService _imageService;

        public const string ALLOWED_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890_-.";
        private readonly List<string> IMAGE_TYPES = new List<string>() { "jpg", "png", "gif" };
        private readonly List<string> ALLOWED_TYPES = new List<string>() { "jpg", "png", "gif", "svg" };

        public CdnService(IOptions<CdnOptions> options, IImageService imageService)
        {
            _options = options;
            _imageService = imageService;
        }

        public FolderContentResponse ListFolderContents(string path, string? userId)
        {
            if (userId != null)
                EnsureUserstorageExistance(userId);

            string securePath = GetSecurePath(path);
            string requestedLocalPath = $"{_options.Value.LocalPath}{Path.DirectorySeparatorChar}{securePath}";

            string[] directories = Directory.GetDirectories(requestedLocalPath);
            string[] files = Directory.GetFiles(requestedLocalPath);

            FolderContentResponse folderContentResponse = new FolderContentResponse();
            folderContentResponse.FolderNames = (from directory in directories.ToList() select Path.GetFileName(directory)).ToList();
            folderContentResponse.Files = new List<FileResponse>();

            foreach(string file in files)
            {
                folderContentResponse.Files.Add(new FileResponse
                {
                    Name = Path.GetFileName(file),
                    PublicUri = $"{_options.Value.PublicPath}/{securePath.Replace(Path.DirectorySeparatorChar,'/')}/{Path.GetFileName(file)}"
                });
            }

            return folderContentResponse;
        }

        public string CreateFolder(string folderName, string userId, string path)
        {
            EnsureUserstorageExistance(userId);
            string securePath = GetSecurePath(path);
            string cleanedFolderName = GetCleanedFilename(folderName);

            string localPath = $"{_options.Value.LocalPath}{Path.DirectorySeparatorChar}{securePath}";

            string targetPath = $"{localPath}{Path.DirectorySeparatorChar}{cleanedFolderName}";

            if (!securePath.StartsWith(userId))
                throw new AccessDeniedException("");

            if (Directory.Exists(targetPath))
                throw new FileExistsException("");

            Directory.CreateDirectory(targetPath);

            return cleanedFolderName;
        }

        public async Task<FileResponse> HandleFileUpload(Microsoft.AspNetCore.Http.IFormFile uploadedFile, string userId, string path, bool skipExisting = false)
        {
            EnsureUserstorageExistance(userId);

            string securePath = GetSecurePath(path);
            string filename = ContentDispositionHeaderValue.Parse(uploadedFile.ContentDisposition).FileName.Trim('"');
            string cleanedFilename = GetCleanedFilename(filename);

            if (!securePath.StartsWith(userId))
                throw new AccessDeniedException("");

            string destinationPath = $"{_options.Value.LocalPath}{Path.DirectorySeparatorChar}{securePath}{Path.DirectorySeparatorChar}{cleanedFilename}";
            int destinationIndex = 0;
            string cleanedFilenameWithoutExtension = Path.GetFileNameWithoutExtension(cleanedFilename);
            string extensionWithDot = Path.GetExtension(cleanedFilename);

            if (!ALLOWED_TYPES.Contains(extensionWithDot.ToLower().Substring(1)))
                throw new FileTypeNotAllowedException("");

            if (skipExisting && File.Exists(destinationPath))
                throw new FileExistsException($"File {cleanedFilename} already exists");

            while (File.Exists(destinationPath))
            {
                destinationIndex++;
                destinationPath = $"{_options.Value.LocalPath}{Path.DirectorySeparatorChar}{securePath}{Path.DirectorySeparatorChar}{cleanedFilenameWithoutExtension}_{destinationIndex}{extensionWithDot}";
            }

            if (destinationIndex > 0)
                cleanedFilename = $"{cleanedFilenameWithoutExtension}_{destinationIndex}{extensionWithDot}";
            else
                cleanedFilename = $"{cleanedFilenameWithoutExtension}{extensionWithDot}";

            using (FileStream stream = new FileStream(destinationPath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(stream);
            }

            string type = Path.GetExtension(cleanedFilename).Replace(".", "");

            if (IMAGE_TYPES.Contains(type.ToLower()))
            {
                _imageService.OptimizeImage(destinationPath);
            }

            return new FileResponse
            {
                Name = cleanedFilename,
                PublicUri = $"{_options.Value.PublicPath}{Path.DirectorySeparatorChar}{securePath}{Path.DirectorySeparatorChar}{cleanedFilename}"
            };
        }

        private string GetUserstorageLocalPath(string userId)
        {
            return $"{_options.Value.LocalPath}{Path.DirectorySeparatorChar}{userId}";
        }

        private void EnsureUserstorageExistance(string userId)
        {
            string userstorageLocalPath = GetUserstorageLocalPath(userId);
            if (!Directory.Exists(userstorageLocalPath))
            {
                Directory.CreateDirectory(userstorageLocalPath);
            }
        }

        private string GetCleanedFilename(string filename)
        {
            string cleanedFilename = "";
            foreach (char character in filename.Replace(" ", "_").AsEnumerable())
            {
                if (ALLOWED_CHARACTERS.Contains(character))
                    cleanedFilename = $"{cleanedFilename}{character}";
            }
            cleanedFilename = cleanedFilename.Replace("..", "");

            while (cleanedFilename.Length > 0 && !char.IsLetterOrDigit(cleanedFilename.Substring(0, 1).ToCharArray().First()))
            {
                cleanedFilename.Substring(1);
            }

            return cleanedFilename;
        }

        private string GetSecurePath(string? path)
        {
            if (path == null)
                return "";

            if (path.StartsWith(Path.DirectorySeparatorChar))
                path = path.Substring(1);

            return path.Replace('/',Path.DirectorySeparatorChar).Replace($"..{Path.DirectorySeparatorChar}", "");
        }

    }
}
