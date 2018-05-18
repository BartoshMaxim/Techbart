using Techbart.DB;

namespace AdminDashboard.Core.ControllersLogic
{
	public static class ProductHepler
    {
        public static bool ImageIsExistsInCreateProductModel(CreateProductModel productModel)
        {
            foreach(var image in productModel.Files)
            {
                if (image != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
