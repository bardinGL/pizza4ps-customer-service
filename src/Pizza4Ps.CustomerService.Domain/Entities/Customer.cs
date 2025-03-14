using Pizza4Ps.CustomerService.Domain.Abstractions;
using Pizza4Ps.CustomerService.Domain.Enums;

namespace Pizza4Ps.CustomerService.Domain.Entities
{
    public class Customer : EntityAuditBase<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public Guid? StreetId { get; set; }

        public virtual Street Street { get; set; }

        public Customer()
        {
        }

        public Customer(Guid id, string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth, string email, string phoneNumber, string avatar, Guid streetId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            StreetId = streetId;
        }

        public void UpdateCustomer(string firstName, string lastName, GenderEnum gender, DateTime dateOfBirth, string email, string phoneNumber, string avatar, Guid streetId)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            StreetId = streetId;
        }
    }
}
