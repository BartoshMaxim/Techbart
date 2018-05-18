using Techbart.DB.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;


namespace Techbart.DB
{
    public class Customer : ICustomer
    {

        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The password must be longer than 6 symbols")]
        [MaxLength(256, ErrorMessage = "The password must be shorter than 256 symbols")]
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

        public string[] GetRoles()
        {
            if (CustomerRole == RoleType.Admin)
            {
                return new string[] { "User", "Admin" };
            }
            else if (CustomerRole == RoleType.User)
            {
                return new string[] { "User" };
            }
            else
            {
                return null;
            }
        }
    }
}
