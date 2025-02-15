using Microsoft.AspNetCore.Http;

namespace Maintenance.Application.Helper
{
    public interface IFileUploaderService
    {
        Task<string> SaveFileAsync(IFormFile file, string directoryName);
        void RemoveFile(string filePath);
        string GetImageBaseUrl();
    }
}
