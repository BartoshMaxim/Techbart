using Techbart.DB.Interfaces;

namespace Techbart.DB.Repositories
{
    public static class TechbartRepository
    {
        public static IProductImageRepository GetProductImageRepository()
        {
            return new ProductImageRepository();
        }

        public static IProductRepository GetProductRepository()
        {
            return new ProductRepository();
        }

        public static IOrderSupplementRepository GetProductSupplementRepository()
        {
            return new OrderSupplementRepository();
        }

        public static ICustomerRepository GetCustomerRepository()
        {
            return new CustomerRepository();
        }

        public static IImageRepository GetImageRepository()
        {
            return new ImageRepository();
        }

        public static IOrderRepository GetOrderRepository()
        {
            return new OrderRepository();
        }

        public static IOrderTypeRepository GetOrderTypeRepository()
        {
            return new OrderTypeRepository();
        }

        public static IRoleTypeRepository GetRoleTypeRepository()
        {
            return new RoleTypeRepository();
        }

        public static ISupplementRepository GetSupplementRepository()
        {
            return new SupplementRepository();
        }
    }
}
