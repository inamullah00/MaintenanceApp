namespace Application.Dto_s.UserDto_s
{
    public class UserDetailsResponseDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Role { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string ExpertiseArea { get; set; }
        public string Rating { get; set; }
        public string Bio { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Skills { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? IsVerified { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int? MonthlyLimit { get; set; }
        public int? OrdersCompleted { get; set; }
        public decimal? TotalEarnings { get; set; }
        public DateTime? ReportMonth { get; set; }
    }
}
