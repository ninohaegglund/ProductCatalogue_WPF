using Newtonsoft.Json;
using Shared.Models;
using System.Collections.ObjectModel;

namespace Shared.Services;


public class ProductService : IProductService
{
    private ObservableCollection<Product> _products;
    private readonly IFileService _fileService;

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;

        var json = _fileService.LoadFromFile();
        if (!string.IsNullOrEmpty(json))
        {
            _products = new ObservableCollection<Product>(JsonConvert.DeserializeObject<List<Product>>(json)!);
        }
        else
        {
            _products = new ObservableCollection<Product>();
        }
    }

    public void SaveProductList()
    {

        _fileService.SaveToFile(JsonConvert.SerializeObject(_products.ToList()));
    }

    public bool AddToList(Product product)
    {
        try
        {
            _products.Add(product);
            SaveProductList();
            return true;
        }
        catch
        {
            return false;
        }

    }
    public void DeleteProduct(Product product)
    {
        if (_products.Contains(product))
        {
            _products.Remove(product);
            SaveProductList();

        }

    }
    public void UpdateProduct(Product existingProduct, Product updatedProduct)
    {
        var product = _products.FirstOrDefault(p => p.Id == existingProduct.Id);
        if (product != null)
        {
            product!.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            SaveProductList();
        }
    }

    public IEnumerable<Product> GetProducts()
    {

        return _products;
    }
    public bool ProductExists(string productName)
    {
        return _products.Any(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
    }
}