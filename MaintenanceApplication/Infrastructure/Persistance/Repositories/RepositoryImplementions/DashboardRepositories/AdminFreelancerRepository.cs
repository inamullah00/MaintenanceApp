using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entity.UserEntities;
using Maintenance.Application.Dto_s.DashboardDtos.Order_Limit_PerformanceReportin_Dtos;
using Maintenance.Application.Interfaces.ReposoitoryInterfaces.DashboardInterfaces.FreelancerInterfaces;
using Maintenance.Application.ViewModel;
using Maintenance.Application.Wrapper;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Maintenance.Infrastructure.Persistance.Repositories.RepositoryImplementions.DashboardRepositories
{
    public class AdminFreelancerRepository : IAdminFreelancerRepository
    {

        private readonly ApplicationDbContext _context;

        public AdminFreelancerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<FreelancerService>> GetFreelancerServicesAsync(Guid freelancerId, CancellationToken cancellationToken)
        {
            return await _context.FreelancerServices.Where(fs => fs.FreelancerId == freelancerId).ToListAsync(cancellationToken);
        }

        public async Task<Freelancer> GetFreelancerByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Freelancers.AsNoTracking().FirstOrDefaultAsync(a => a.Email.ToLower().Trim().Equals(email.ToLower().Trim()), cancellationToken);
        }
        public async Task<Freelancer> GetFreelancerByPhoneNumberAsync(string phoneNumber, Guid? countryId, CancellationToken cancellationToken)
        {
            return await _context.Freelancers.AsNoTracking().FirstOrDefaultAsync(f => !string.IsNullOrEmpty(f.PhoneNumber) && f.PhoneNumber.ToLower().Trim().Equals(phoneNumber.ToLower().Trim()) && f.CountryId == countryId, cancellationToken);
        }

        public async Task<bool> AddFreelancerAsync(Freelancer freelancer, CancellationToken cancellationToken = default)
        {
            await _context.Freelancers.AddAsync(freelancer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Freelancer?> GetFreelancerByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false)
        {
            var query = _context.Freelancers
                .Include(f => f.FreelancerServices)
             .ThenInclude(fs => fs.Service)
                .AsQueryable();

            if (!trackChanges)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }
        public async Task<bool> UpdateFreelancer(Freelancer freelancer, CancellationToken cancellationToken)
        {

            _context.Update(freelancer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task UpdateFreelancerServicesAsync(Freelancer freelancer, List<Guid> newServiceIds, CancellationToken cancellationToken)
        {
            var existingServiceIds = freelancer.FreelancerServices.Select(fs => fs.ServiceId).ToHashSet();

            var servicesToRemove = freelancer.FreelancerServices.Where(fs => !newServiceIds.Contains(fs.ServiceId));
            if (servicesToRemove.Any())
            {
                _context.FreelancerServices.RemoveRange(servicesToRemove);
            }

            var servicesToAdd = newServiceIds.Except(existingServiceIds)
                .Select(serviceId => new FreelancerService
                {
                    FreelancerId = freelancer.Id,
                    ServiceId = serviceId,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

            if (servicesToAdd.Any())
            {
                _context.FreelancerServices.AddRange(servicesToAdd);
            }
        }


        public async Task<bool> Approve(Freelancer freelancer, CancellationToken cancellationToken = default)
        {
            _context.Freelancers.Update(freelancer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> Suspend(Freelancer freelancer, CancellationToken cancellationToken = default)
        {
            _context.Freelancers.Update(freelancer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<PaginatedResponse<FreelancerResponseViewModel>> GetFilteredFreelancersAsync(FreelancerFilterViewModel filter, ISpecification<Freelancer>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Freelancers.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = (from freelancer in query
                                 join country in _context.Countries.AsNoTracking()
                                     on freelancer.CountryId equals country.Id into countryGroup
                                 from country in countryGroup.DefaultIfEmpty()
                                 where freelancer.IsType == UserType.Freelancer
                                 orderby freelancer.FullName
                                 select new FreelancerResponseViewModel
                                 {
                                     Id = freelancer.Id.ToString(),
                                     FullName = freelancer.FullName,
                                     Email = freelancer.Email,
                                     DialCode = country != null ? country.DialCode : string.Empty,
                                     CountryId = freelancer.CountryId,
                                     PhoneNumber = freelancer.PhoneNumber,
                                     DateOfBirth = DateOnly.FromDateTime(freelancer.DateOfBirth),
                                     ExperienceLevel = freelancer.ExperienceLevel,
                                     Status = freelancer.Status,
                                 });
            var totalCount = await filteredQuery.CountAsync();

            var freelancers = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<FreelancerResponseViewModel>(freelancers, totalCount, filter.PageNumber, filter.PageSize);
        }


        public async Task<PaginatedResponse<CompanyResponseViewModel>> GetFilteredCompaniesAsync(FreelancerFilterViewModel filter, ISpecification<Freelancer>? specification = null)
        {
            var query = SpecificationEvaluator.Default.GetQuery(query: _context.Freelancers.AsNoTracking().AsQueryable(), specification: specification);

            var filteredQuery = (from freelancer in query
                                 join country in _context.Countries.AsNoTracking()
                                     on freelancer.CountryId equals country.Id into countryGroup
                                 from country in countryGroup.DefaultIfEmpty()
                                 where freelancer.IsType == UserType.Company
                                 orderby freelancer.FullName
                                 select new CompanyResponseViewModel
                                 {
                                     Id = freelancer.Id.ToString(),
                                     FullName = freelancer.FullName,
                                     Email = freelancer.Email,
                                     DialCode = country != null ? country.DialCode : string.Empty,
                                     CountryId = freelancer.CountryId,
                                     PhoneNumber = freelancer.PhoneNumber,
                                     ExperienceLevel = freelancer.ExperienceLevel,
                                     Status = freelancer.Status,
                                 });
            var totalCount = await filteredQuery.CountAsync();

            var companies = await filteredQuery.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            return new PaginatedResponse<CompanyResponseViewModel>(companies, totalCount, filter.PageNumber, filter.PageSize);
        }




        // Check if the freelancer can accept new orders
        public async Task<(bool, ApplicationUser?, string? Message)> CanAcceptNewOrderAsync(string freelancerId, CancellationToken cancellationToken)
        {
            // Retrieve freelancer from the database
            var freelancer = await _context.Users
                .FirstOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

            // If freelancer is not found
            if (freelancer == null)
            {
                return (false, null, "Freelancer not found with the provided ID.");
            }

            // If freelancer has completed zero orders, they can accept new orders
            //if (freelancer.OrdersCompleted == 0)
            //{
            //    return (true, freelancer, "The freelancer has no completed orders yet and can accept new orders.");
            //}

            //// If the freelancer has completed orders but hasn't exceeded the order limit
            //if (freelancer.OrdersCompleted <= freelancer.MonthlyLimit)
            //{
            //    return (true, freelancer, $"The freelancer can accept new orders. Current limit is {freelancer.MonthlyLimit} and they have completed {freelancer.OrdersCompleted} orders.");
            //}

            // If freelancer has exceeded the order limit
            return (false, freelancer, "The freelancer has exceeded their monthly order limit and cannot accept new orders.");


        }


        // Set the order limit for a freelancer
        public async Task<bool> SetOrderLimitAsync(string freelancerId, int orderLimit, CancellationToken cancellationToken)
        {
            var freelancer = await _context.Users
                .FirstOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

            if (freelancer == null)
            {
                return false; // Freelancer not found
            }
            // Set the new order limit
            //freelancer.MonthlyLimit = orderLimit;

            _context.Users.Update(freelancer);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<FreelancerPerformanceReportResponseDto>> GeneratePerformanceReportAsync(string freelancerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            try
            {

                var performanceReport = await (from FreelancerOrders in _context.Orders
                                               join Freelancer in _context.Freelancers
                                               on FreelancerOrders.FreelancerId equals Freelancer.Id
                                               where FreelancerOrders.CreatedAt >= startDate && FreelancerOrders.CompletedDate <= endDate && FreelancerOrders.CompletedDate != null
                                               group FreelancerOrders by Freelancer.Id into g
                                               select new FreelancerPerformanceReportResponseDto
                                               {
                                                   FreelancerId = g.Key,
                                                   FreelancerName = g.Select(f => f.Freelancers.FullName).FirstOrDefault(),
                                                   TotalOrders = g.Count(),
                                                   TotalEarnings = g.Sum(o => o.FreelancerAmount), // Assuming `FreelancerAmount` is the payment to the freelancer
                                                   //AverageRating = g.Average(o => o.Rat), // Assuming `Rating` is available in the `Orders`
                                                   CompletedOrders = g.Count(o => o.Status == OrderStatus.Completed), // Adjust depending on your `Status` field
                                                   StartDate = startDate,
                                                   EndDate = endDate
                                               }).ToListAsync(cancellationToken);

                return performanceReport;
            }
            catch (Exception ex)
            {
                // Log the error or handle it as needed
                throw new Exception("Error fetching performance data", ex);
            }
        }

        Task<List<ApplicationUser>> IAdminFreelancerRepository.GenerateTopPerformersReportAsync(int month, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        //public async Task<PerformanceReportDto> GeneratePerformanceReportAsync(Guid freelancerId, DateTime startDate, DateTime endDate)
        //{
        //    var report = new PerformanceReportDto();

        //    // Get orders completed in the specified period
        //    var completedOrders = await _orderRepository.GetCompletedOrdersAsync(freelancerId, startDate, endDate);
        //    report.CompletedOrders = completedOrders.Count;

        //    // Get earnings (sum of payments to the freelancer)
        //    var earnings = await _paymentRepository.GetTotalEarningsAsync(freelancerId, startDate, endDate);
        //    report.TotalEarnings = earnings;

        //    // Get average rating and feedback count
        //    var feedbacks = await _feedbackRepository.GetFeedbacksForFreelancerAsync(freelancerId, startDate, endDate);
        //    report.AverageRating = feedbacks.Average(f => f.Rating);
        //    report.FeedbackCount = feedbacks.Count;

        //    // Order completion rate
        //    var totalAssignedOrders = await _orderRepository.GetAssignedOrdersAsync(freelancerId, startDate, endDate);
        //    report.OrderCompletionRate = (double)report.CompletedOrders / totalAssignedOrders.Count * 100;

        //    // Monthly limit usage
        //    var freelancer = await _userManager.FindByIdAsync(freelancerId.ToString());
        //    report.MonthlyLimitUsage = freelancer.OrdersCompleted.HasValue
        //        ? (freelancer.OrdersCompleted.Value / freelancer.MonthlyLimit.Value) * 100
        //        : 0;

        //    return report;
        //}

    }
}
