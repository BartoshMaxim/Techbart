namespace Techbart.DB.Interfaces
{
    public interface IOrderSupplement
    {
        int OrderSupplementId { get; set; }

        int OrderId { get; set; }

        int SupplementId { get; set; }
    }
}
