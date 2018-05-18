using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }  

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class FullOrder
    {
        public int OrderId { get; set; }

        public Product Product { get; set; }

        public Customer Customer { get; set; }

        public float OrderWeight { get; set; }

        public OrderType OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
