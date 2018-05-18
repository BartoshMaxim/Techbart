using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryDb
{
    public class Cake
    {
        public Cake()
        {
            
        }

        public Cake(Cake cake)
        {
            CakeId = cake.CakeId;
            CakeName = cake.CakeName;
            CakeDescription = cake.CakeDescription;
            CakePrice = cake.CakePrice;
            ImageId = cake.ImageId;

        }

        public int CakeId { get; set; }

        public string CakeName { get; set; }

        public string CakeDescription { get; set; }

        public int CakePrice { get; set; }

        /// <summary>
        /// Preview image
        /// </summary>
        public int ImageId { get; set; }

        public DateTime AddedTime { get; set; }
    }
}
