using starbase_nexus_api.Models.Cdn;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Cdn
{
    public interface ICdnService
    {
        FolderContentResponse ListFolderContents(string path, string? userId);
        string CreateFolder(string folderName, string userId, string path);
        Task<FileResponse> HandleFileUpload(Microsoft.AspNetCore.Http.IFormFile uploadedFile, string userId, string path, bool skipExisting = false);
    }
}