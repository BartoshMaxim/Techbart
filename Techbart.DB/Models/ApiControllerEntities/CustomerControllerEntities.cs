using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techbart.DB.Models
{
    public class CustomerLoginRequest : ICustomerLoginRequest
    {
        // CUSTOMER FIELDS
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CustomerPassword { get; set; }

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

        // LOGIN MODEL FIELDS

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public ICustomer GetCustomer()
        {
            return new Customer
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                Country = Country,
                CreatedDate = CreatedDate,
                CustomerId = CustomerId,
                CustomerPassword = CustomerPassword,
                CustomerPhone = CustomerPhone,
                CustomerRole = CustomerRole,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName
            };
        }

        public ILoginModel GetLoginModel()
        {
            return new LoginModel
            {
                Login = Login,
                Password = Password
            };
        }
    }
}