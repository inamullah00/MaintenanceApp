using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entity.UserEntities;
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

        public async Task<(bool Success, string Message)> AddServiceAsync(OfferedServiceRequestDto request)
        {
            var service = _mapper.Map<OfferedService>(request);

        

            // Call the ImageUpload method to handle file uploads
            var (uploadSuccess, uploadedImageUrls, uploadMessage) = await ImageUploadAsync(request.ImageFiles);

            if (!uploadSuccess)
            {
                return (false, uploadMessage);
            }
            // Store the uploaded image URLs in the entity
            service.ImageUrls = uploadedImageUrls;

            // Call the VideoUploadAsync method
            var (VideouploadSuccess, uploadedVideoUrls, VideouploadMessage) = await VideoUploadAsync(request.VideoFiles);

            if (!uploadSuccess)
            {
                return (false, uploadMessage);
            }

            // Store the uploaded video URLs in the entity
            service.VideoUrls = uploadedVideoUrls;



            var (AudioUploadSuccess, UploadedAudioUrls, AudioUploadMessage) = await AudioUploadAsync(request.AudioFiles);
            if (!AudioUploadSuccess)
            {
                return (false, AudioUploadMessage);
            }

            service.AudioUrls = UploadedAudioUrls;

            var entity = await _unitOfWork.OfferedServiceRepository.CreateAsync(service);

            if (entity == null)
            {
                return (false, "An Error Occured While Adding Service");
            }

            return (true, "Service Added Successfully!");

        }

        public async Task<(bool Success, string Message)> DeleteServiceAsync(Guid serviceId)
        {
            // Validate the serviceId (Check if it's empty or null)
            if (serviceId == Guid.Empty)
            {
                return (false, "Invalid Id");
            }

            // Fetch the service from the repository by Id
            var service = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);

            // Check if the service exists
            if (service == null)
            {
                return (false, "Service Not Found");
            }

            // Delete the service using the repository
            var isDeleted = await _unitOfWork.OfferedServiceRepository.RemoveAsync(service);

            // If deletion fails, return an error message
            if (!isDeleted)
            {
                return (false, "An Error Occurred While Deleting the Service");
            }

            // Successfully deleted the service, return a success message
            return (true, "Service Deleted Successfully!");
        }

        public async Task<(bool Success, OfferedServiceResponseDto? Service, string Message)> GetServiceAsync(Guid serviceId)
        {
           var service =  await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);
            
            if (service == null)
            {
                return (false, null,"Service Not Found");
            }
            var offeredservice = _mapper.Map<OfferedServiceResponseDto>(service);

            return (true, offeredservice, "Service Found");


        }

        public async Task<(bool Success, List<OfferedServiceResponseDto>? Services, string Message)> GetServicesAsync()
        {

            var services = await _unitOfWork.OfferedServiceRepository.GetAllAsync();

            var res = _mapper.Map<List<OfferedServiceResponseDto>>(services);

            if (res == null || !res.Any())
            {
                return (false, null, "No services found.");
            }

            return (true, res, $"{res.Count} service(s) found.");
        }

        public async Task<(bool Success, string Message)> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updatedRequest)
        {
            //var service = _mapper.Map<OfferedService>(updatedRequest);
            //await _genericRepository.UpdateAsync(serviceId,service);
            //return (true, "Service Updated Successfully!");

            return (false, "");
        }




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
