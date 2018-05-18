using Techbart.DB.Interfaces;

namespace Techbart.DB
{
    public class Supplement : ISupplement
    {
        public int SupplementId { get; set; }

        public string SupplementName { get; set; }

        public string SupplementDescription { get; set; }

        public int SupplementPrice { get; set; }

        public float SupplementWeight { get; set; }
    }
}
