using Application.Dto_s.ClientDto_s;
using Application.Interfaces.ReposoitoryInterfaces;
using Application.Interfaces.ReposoitoryInterfaces.OfferedServicInterface;
using Application.Interfaces.ServiceInterfaces.ClientInterfaces;
using Ardalis.Specification;
using AutoMapper;
using Domain.Entity.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ServiceImplemention
{
    public class OfferedServices : IClientService
    {
        private readonly IOfferedServiceRepository _offeredServiceRepository;
        private readonly IMapper _mapper;

        public OfferedServices(IOfferedServiceRepository  offeredServiceRepository, IMapper mapper)
        {
            _offeredServiceRepository = offeredServiceRepository;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message)> AddServiceAsync(OfferedServiceRequestDto request)
        {
            var service = _mapper.Map<OfferedService>(request);

            if (request.ImageFiles == null || !request.ImageFiles.Any())
            {
                return (false, "No images uploaded.");
            }

            // Define a directory to save the uploaded files
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uploadedImageUrls = new List<string>();

            foreach (var imageFile in request.ImageFiles)
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
                uploadedImageUrls.Add($"/UploadedFiles/{fileName}"); // Save relative path
            }

            // Store the file URLs in the entity
            service.ImageUrls = uploadedImageUrls;

            var entity = await _offeredServiceRepository.CreateAsync(service);

            if (entity==null) 
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
            var service = await _offeredServiceRepository.GetByIdAsync(serviceId);

            // Check if the service exists
            if (service == null)
            {
                return (false, "Service Not Found");
            }

            // Delete the service using the repository
            var isDeleted = await _offeredServiceRepository.RemoveAsync(service);

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
           var service =  await _offeredServiceRepository.GetByIdAsync(serviceId);
            
            if (service == null)
            {
                return (false, null,"Service Not Found");
            }
            var offeredservice = _mapper.Map<OfferedServiceResponseDto>(service);

            return (true, offeredservice, "Service Found");


        }

        public async Task<(bool Success, List<OfferedServiceResponseDto>? Services, string Message)> GetServicesAsync()
        {

            var services = await _offeredServiceRepository.GetAllAsync();

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
    }
}
