using Maintenance.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Maintenance.Application.Helper
{
    public class FileUploaderService : IFileUploaderService
    {
        private readonly string _rootPath;
        private readonly string _defaultFolder = "Uploads";
        private readonly string _baseImageUrl;

        private const long MaxImageSize = 3 * 1024 * 1024; // 3MB for images
        private const long MaxPdfSize = 20 * 1024 * 1024; // 20MB for PDFs

        private readonly string[] _allowedImageTypes = { "image/jpeg", "image/png", "image/webp" };
        private const string AllowedPdfType = "application/pdf";

        public FileUploaderService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            var fileStorageSettings = configuration.GetSection("FileStorageSettings").Get<FileStorageSettings>();
            _rootPath = fileStorageSettings?.RootPath ?? webHostEnvironment.WebRootPath;
            _baseImageUrl = configuration.GetSection("ImageBaseUrl").Value;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string directoryName)
        {
            if (file == null || file.Length == 0) throw new ArgumentNullException(nameof(file));

            bool isPdf = file.ContentType == AllowedPdfType;

            // Validate file
            if (isPdf)
                ValidatePdfFile(file);
            else
                ValidateImageFile(file);
            // Ensure the Images directory exists
            var uploadPath = Path.Combine(_rootPath, _defaultFolder);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Create the subdirectory under Images
            var directoryPath = Path.Combine(uploadPath, directoryName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            // Generate a unique file name
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(directoryPath, uniqueFileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(_defaultFolder, directoryName, uniqueFileName);
        }

        public void RemoveFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            var fullPath = Path.Combine(_rootPath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        private void ValidateImageFile(IFormFile file)
        {
            if (!_allowedImageTypes.Contains(file.ContentType))
                throw new CustomException("Invalid image format. Only PNG, JPG, and WebP formats are supported.");

            if (file.Length > MaxImageSize)
                throw new CustomException("Image file size exceeds the 10MB limit.");
        }

        private void ValidatePdfFile(IFormFile file)
        {
            if (file.ContentType != AllowedPdfType)
                throw new CustomException("Invalid file format. Only PDF files are allowed.");

            if (file.Length > MaxPdfSize)
                throw new CustomException("PDF file size exceeds the 20MB limit.");
        }

        //public void ValidateImageFiles(List<IFormFile> files)
        //{
        //    if (files.Any())
        //    {
        //        var imageFormatList = files.Select(a => a.ContentType).ToList();
        //        var fileSizeList = files.Select(a => a.Length).ToList();
        //        ValidateImageType(imageFormatList);
        //        ValidateImageSize(fileSizeList);
        //    }
        //}

        //private void ValidateImageType(List<string> imagesTypes)
        //{
        //    string[] allowedImageTypes = { "image/jpeg", "image/png", "image/webp" };
        //    if (imagesTypes.Any(a => !allowedImageTypes.Contains(a))) throw new CustomException("Invalid Image Format. Only png,jpg,webp format supported");

        //}

        //private void ValidateImageSize(List<long> fileSizes)
        //{
        //    const long maxFileSize = 3 * 1024 * 1024;
        //    if (fileSizes.Any(size => size > maxFileSize))
        //    {
        //        throw new CustomException("File size exceeds the 3 MB limit.");
        //    }
        //}

        public string GetImageBaseUrl() => _baseImageUrl;
    }

    public class FileStorageSettings
    {
        public string RootPath { get; set; }
    }
}
