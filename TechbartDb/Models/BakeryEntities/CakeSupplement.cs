using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class CakeSupplement
    {
        public int CakeSupplementId { get; set; }

        public int CakeId { get; set; }

        public int SupplementId { get; set; }
    }
}
