using Newtonsoft.Json;
using Shared.Models;

namespace Shared.Services;


public class ProductService
{
    private List<Product> _products = [];
    private readonly FileService _fileService;

    public ProductService(FileService fileService)
    {
        _fileService = fileService;
    }

    public void SaveProductList(List<Product> products)
    {
        _products = products; 
        _fileService.SaveToFile(JsonConvert.SerializeObject(_products)); 
    }

    public bool AddToList(Product Product)
    {
        try
        {
            _products.Add(Product);
            _fileService.SaveToFile(JsonConvert.SerializeObject(_products));
            return true;
        }
        catch { }
        return false;
    }

    public IEnumerable<Product> GetProducts()
    {
        var json = _fileService.GetFromFile();
        if (!string.IsNullOrEmpty(json))
        {
            _products = JsonConvert.DeserializeObject<List<Product>>(json)!;
        }

        return _products;
    }
    public bool ProductExists(string productName)
    {
        return _products.Any(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
    }
}