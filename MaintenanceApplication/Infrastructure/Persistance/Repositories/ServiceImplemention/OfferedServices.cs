using Application.Dto_s.ClientDto_s;
using Application.Interfaces.IUnitOFWork;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Ardalis.Specification;
using AutoMapper;
using Maintenance.Application.Common.Constants;
using Maintenance.Application.Dto_s.ClientDto_s.AddressDtos;
using Maintenance.Application.Services.Client;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.ClientEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Persistance.Repositories.ServiceImplemention
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
                return Result<string>.Failure(uploadMessage, StatusCodes.Status400BadRequest);
            }

            // Store the uploaded image URLs in the entity
            service.ImageUrls = uploadedImageUrls;

            // Call the VideoUploadAsync method
            var (videoUploadSuccess, uploadedVideoUrls, videoUploadMessage) = await VideoUploadAsync(request.VideoFiles);

            if (!videoUploadSuccess)
            {
                return Result<string>.Failure(videoUploadMessage, HttpResponseCodes.BadRequest);
            }

            // Store the uploaded video URLs in the entity
            service.VideoUrls = uploadedVideoUrls;

            // Call the AudioUploadAsync method
            var (audioUploadSuccess, uploadedAudioUrls, audioUploadMessage) = await AudioUploadAsync(request.AudioFiles);

            if (!audioUploadSuccess)
            {
                return Result<string>.Failure(audioUploadMessage, StatusCodes.Status400BadRequest);
            }

            // Store the uploaded audio URLs in the entity
            service.AudioUrls = uploadedAudioUrls;

            var entity = await _unitOfWork.OfferedServiceRepository.CreateAsync(service);

            if (entity == null)
            {
                return Result<string>.Failure(ErrorMessages.ServiceCreationFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<string>.Success(entity.Id.ToString(), SuccessMessages.ServiceCreated, StatusCodes.Status201Created);
        }


        public async Task<Result<string>> DeleteServiceAsync(Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                return Result<string>.Failure(ErrorMessages.InvalidServiceId, StatusCodes.Status400BadRequest);
            }

            var service = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);

            if (service == null)
            {
                return Result<string>.Failure(ErrorMessages.ServiceNotFound, StatusCodes.Status404NotFound);
            }

            var serviceEntity = _mapper.Map<OfferedService>(service);

            var isDeleted = await _unitOfWork.OfferedServiceRepository.RemoveAsync(serviceEntity);

            if (!isDeleted)
            {
                return Result<string>.Failure(ErrorMessages.ServiceDeletionFailed, StatusCodes.Status500InternalServerError);
            }

            return Result<string>.Success(SuccessMessages.ServiceDeleted, StatusCodes.Status200OK);
        }


        public async Task<Result<OfferedServiceResponseDto>> GetServiceAsync(Guid serviceId)

        {
            if (serviceId == Guid.Empty)
            {
                return Result<OfferedServiceResponseDto>.Failure(
                 ErrorMessages.InvalidServiceId,
                 StatusCodes.Status400BadRequest
             );
            }

            var service = await _unitOfWork.OfferedServiceRepository.GetByIdAsync(serviceId);

            var offeredService = _mapper.Map<OfferedServiceResponseDto>(service);

            return Result<OfferedServiceResponseDto>.Success(offeredService, ErrorMessages.OperationSuccess, StatusCodes.Status200OK);
        }


        public async Task<Result<List<OfferedServiceResponseDto>>> GetServicesAsync()
        {
            var services = await _unitOfWork.OfferedServiceRepository.GetAllAsync();

            var res = _mapper.Map<List<OfferedServiceResponseDto>>(services);

            return Result<List<OfferedServiceResponseDto>>.Success(res, $"{res.Count} service(s) found.", StatusCodes.Status200OK);
        }


        public async Task<Result<OfferedServiceResponseDto>> UpdateServiceAsync(Guid serviceId, OfferedUpdateRequestDto updateRequest)
        {

            if (serviceId == Guid.Empty || updateRequest == null)
            {
                return Result<OfferedServiceResponseDto>.Failure(
                 ErrorMessages.InvalidServiceData,
                 StatusCodes.Status400BadRequest
             );
            }

            var service = _mapper.Map<OfferedService>(updateRequest);
            var (res, data) = await _unitOfWork.OfferedServiceRepository.UpdateAsync(service, serviceId);
            if (!res)
            {

                return Result<OfferedServiceResponseDto>.Failure(
                  ErrorMessages.ServiceUpdateFailed,
                  StatusCodes.Status500InternalServerError
              );

            }
            return Result<OfferedServiceResponseDto>.Success(
            _mapper.Map<OfferedServiceResponseDto>(data),
            SuccessMessages.ServiceUpdated,
            StatusCodes.Status200OK);


        }


        #region Client Location/Address


       public async Task<Result<string>> SaveAddressAsync(ClientAddressRequestDto request)
        {

            if (request == null || request.ClientId == Guid.Empty)
            {
                return Result<string>.Failure("Invalid address data", StatusCodes.Status400BadRequest);
            }

            var address = _mapper.Map<ClientAddress>(request);

            //var entity = await _unitOfWork.ClientAddressRepository.CreateAsync(address);

            //if (entity == null)
            //{
            //    return Result<string>.Failure("Failed to save address", StatusCodes.Status500InternalServerError);
            //}

            //return Result<string>.Success(entity.Id.ToString(), "Address saved successfully", StatusCodes.Status201Created);

            return null;
        }

        public async Task<Result<List<ClientAddressResponseDto>>> GetSavedAddressesAsync(Guid clientId)
        {
            return null;
        }

        public async Task<Result<string>> DeleteAddressAsync(Guid addressId)
        {

            return null;
        }

        #endregion



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
