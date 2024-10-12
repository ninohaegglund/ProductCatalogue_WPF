using Shared.Models;

namespace Shared.Services
{
    public interface IProductService
    {
        bool AddToList(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product existingProduct, Product updatedProduct);
        IEnumerable<Product> GetProducts();
        bool ProductExists(string productName);
        void SaveProductList();
    }
}