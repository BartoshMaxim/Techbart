using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Email { get; set; }

        public string CustomerPassword { get; set; }

        public string CustomerPhone { get; set; }

        public MainAddress Address { get; set; }

        public RoleType CustomerRole { get; set; }
    }
}
