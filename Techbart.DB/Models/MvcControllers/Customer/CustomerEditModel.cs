using Techbart.DB.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Techbart.DB
{
    public class CustomerEditModel : ICustomer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public RoleType CustomerRole { get; set; }

        public string CustomerPassword { get; set; }

        public static explicit operator Customer(CustomerEditModel editModel)
        {
            return new Customer
            {
                Address1 = editModel.Address1,
                Address2 = editModel.Address2,
                City = editModel.City,
                Country = editModel.Country,
                CreatedDate = editModel.CreatedDate,
                CustomerId = editModel.CustomerId,
                CustomerPhone = editModel.CustomerPhone,
                CustomerRole = editModel.CustomerRole,
                Email = editModel.Email,
                FirstName = editModel.FirstName,
                LastName = editModel.LastName
            };
        }
    }
}