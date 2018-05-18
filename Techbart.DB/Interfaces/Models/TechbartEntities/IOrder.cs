using System;

namespace Techbart.DB.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; set; }

        int ProductId { get; set; }

        int CustomerId { get; set; }

        float OrderWeight { get; set; }

        OrderType OrderType { get; set; }

        DateTime OrderDate { get; set; }

        DateTime CreatedDate { get; set; }
    }
}
