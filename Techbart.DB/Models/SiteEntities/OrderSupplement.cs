using Techbart.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techbart.DB
{
    public class OrderSupplement : IOrderSupplement
    {
        public int OrderSupplementId { get; set; }

        public int OrderId { get; set; }

        public int SupplementId { get; set; }
    }
}
