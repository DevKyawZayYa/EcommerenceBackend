using EcommerenceBackend.Application.Domain.Users;

namespace EcommerenceBackend.Application.UseCases.Queries.GetUserProfileById
{
    public class GetUserProfileByIdResponse
    {
        public FirstName? FirstName { get; set; }
        public LastName? LastName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? MobileCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ? LastLoginDate { get; set; }
    }
}
