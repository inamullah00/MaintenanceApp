using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Ardalis.Specification;
using AutoMapper;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Client;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class OfferedServices : IClientService
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferedServices(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<string>> AddServiceAsync(OfferedServiceRequestDto request)
        {
            var service = _mapper.Map<OfferedService>(request);

            // Call the ImageUpload method to handle file uploads
            var (uploadSuccess, uploadedImageUrls, uploadMessage) = await ImageUploadAsync(request.ImageFiles);

            if (!uploadSuccess)
            {
                return Result<string>.Failure(uploadMessage, 400);
            }

            // Store the uploaded image URLs in the entity
            service.ImageUrls = uploadedImageUrls;

            // Call the VideoUploadAsync method
            var (videoUploadSuccess, uploadedVideoUrls, videoUploadMessage) = await VideoUploadAsync(request.VideoFiles);

            if (!videoUploadSuccess)
            {
                return Result<string>.Failure(videoUploadMessage, 400);
            }

            // Store the uploaded video URLs in the entity
            service.VideoUrls = uploadedVideoUrls;

            // Call the AudioUploadAsync method
            var (audioUploadSuccess, uploadedAudioUrls, audioUploadMessage) = await AudioUploadAsync(request.AudioFiles);

            if (!audioUploadSuccess)
            {
                return Result<string>.Failure(audioUploadMessage, 400);
            }

            // Store the uploaded audio URLs in the entity
            service.AudioUrls = uploadedAudioUrls;

            var entity = await _unitOfWork.OfferedServiceRepository.CreateAsync(service);

            if (entity == null)
            {
                return Result<string>.Failure("An error occurred while adding the service.", 400);
            }

            return Result<string>.Success(entity.Id.ToString(),"Service added successfully!", 201);
        }


        public async Task<Result<string>> DeleteServiceAsync(Guid serviceId)
        {
            // Validate the serviceId (Check if it's empty or null)
            if (serviceId == Guid.Empty)
            {
                return Result<string>.Failure("Invalid Id", 400);
            }

            // Fetch the service from the repository by Id
            var service = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);

            // Check if the service exists
            if (service == null)
            {
                return Result<string>.Failure("Service Not Found", 404);
            }

            // Delete the service using the repository
            var isDeleted = await _unitOfWork.OfferedServiceRepository.RemoveAsync(service);

            // If deletion fails, return an error message
            if (!isDeleted)
            {
                return Result<string>.Failure("An Error Occurred While Deleting the Service", 400);
            }

            // Successfully deleted the service, return a success message
            return Result<string>.Success("Service Deleted Successfully!",200);
        }


        public async Task<Result<OfferedServiceResponseDto>> GetServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);

            if (service == null)
            {
                return Result<OfferedServiceResponseDto>.Failure("Service Not Found", 404);
            }

            var offeredService = _mapper.Map<OfferedServiceResponseDto>(service);

            return Result<OfferedServiceResponseDto>.Success(offeredService, "Service Found",200);
        }


        public async Task<Result<List<OfferedServiceResponseDto>>> GetServicesAsync()
        {
            var services = await _unitOfWork.OfferedServiceRepository.GetAllAsync();

            var res = _mapper.Map<List<OfferedServiceResponseDto>>(services);

            if (res == null || !res.Any())
            {
                return Result<List<OfferedServiceResponseDto>>.Failure("No services found.",404);
            }

            return Result<List<OfferedServiceResponseDto>>.Success(res, $"{res.Count} service(s) found.",200);
        }


        //public async Task<(bool Success, string Message)> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updatedRequest)
        //{
        //    //var service = _mapper.Map<OfferedService>(updatedRequest);
        //    //await _genericRepository.UpdateAsync(serviceId,service);
        //    //return (true, "Service Updated Successfully!");

        //    return (false, "");
        //}




        #region Image Upload

        private async Task<(bool Success, List<string> ImageUrls, string Message)> ImageUploadAsync(IEnumerable<IFormFile> imageFiles)
        {

            if (imageFiles == null || !imageFiles.Any())
            {
                return (false, new List<string>(), "No images uploaded.");
            }


            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uploadedImageUrls = new List<string>();

            try
            {
                foreach (var imageFile in imageFiles)
                {
                    // Generate a unique file name
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                    var fullPath = Path.Combine(uploadPath, fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Add the file path to the list
                    uploadedImageUrls.Add($"/UploadedFiles/{fileName}");
                }

                return (true, uploadedImageUrls, "Files uploaded successfully.");
            }
            catch (Exception ex)
            {
                return (false, new List<string>(), $"An error occurred during file upload: {ex.Message}");
            }
        }

        #endregion

        #region Video Upload

        private async Task<(bool Success, List<string> VideoUrls, string Message)> VideoUploadAsync(IEnumerable<IFormFile> videoFiles)
        {


            if (videoFiles == null || !videoFiles.Any())
            {
                return (false, new List<string>(), "No Video uploaded.");
            }


            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedVideos");

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uploadedVideoUrls = new List<string>();

            try
            {
                foreach (var videoFile in videoFiles)
                {
                    // Generate a unique file name
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(videoFile.FileName)}";
                    var fullPath = Path.Combine(uploadPath, fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                    }

                    // Add the file path to the list
                    uploadedVideoUrls.Add($"/UploadedVideos/{fileName}");
                }

                return (true, uploadedVideoUrls, "Videos uploaded successfully.");
            }
            catch (Exception ex)
            {
                return (false, new List<string>(), $"An error occurred during video upload: {ex.Message}");
            }
        }

        #endregion


        #region Upload Audio

        private async Task<(bool Success, List<string> VideoUrls, string Message)> AudioUploadAsync(IEnumerable<IFormFile> AudioFiles)
        {
            if (AudioFiles == null || !AudioFiles.Any())
            {
                return (false, new List<string>(), "No Audio uploaded.");
            }

            var allowedContentTypes = new List<string> { "audio/mpeg", "audio/wav" };


            var UploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedAudios");

            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);
            }

            var UploadedAudioUrls = new List<string>();

         
            try
            {
                foreach (var audioFile in AudioFiles)
                {

                    // Validate ContentType (MIME Type)
                    if (!allowedContentTypes.Contains(audioFile.ContentType.ToLower()))
                    {
                        return (false, new List<string>(), "Invalid Audio File. Only .mp3 and .wav are allowed.");
                    }


                    //generate Unique File Name

                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(audioFile.FileName)}";
                    var fullPath = Path.Combine(UploadPath, fileName);


                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await audioFile.CopyToAsync(fileStream);

                        UploadedAudioUrls.Add($"/UploadedAudios/{fileName}");
                    }



                }

                return (true, UploadedAudioUrls, "Files uploaded successfully.");

            }
            catch (Exception ex)
            {
                return (false, new List<string>(), $"An error occurred during file upload: {ex.Message}");
            }
        }

        #endregion

    }
}
