

using ProductCatalogue.Core;
using Shared.Services;
using Shared.Models;
using System.Windows.Input;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows;

namespace ProductCatalogue.MVVM.ViewModel;

public class EditProductViewModel : ObservableObject
{
    private string _productName;
    private decimal _productPrice;
    private readonly IProductService _productService;
    private readonly Product _product;
    private readonly Window _window;

    public EditProductViewModel(Product product, IProductService productService, Window window)
    {
        _product = product;
        _productName = product.Name;
        _productPrice = product.Price;
        _productService = productService;
        _window = window;

        SaveCommand = new RelayCommand(SaveProduct);
    }

    public string ProductName
    {
        get => _productName;
        set => SetProperty(ref _productName, value);
    }

    public decimal ProductPrice
    {
        get => _productPrice;
        set => SetProperty(ref _productPrice, value);
    }

    public ICommand SaveCommand { get;}

    public void SaveProduct(object parameter)
    {
        if(string.IsNullOrWhiteSpace(ProductName))
        {
            MessageBox.Show(
                "Product name cannot be empty!",
                "Message",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
        var products = _productService.GetProducts();

        var existingProduct = products
            .FirstOrDefault(p => p.Name.Equals(ProductName, StringComparison.OrdinalIgnoreCase) && p.Id != _product.Id);

        if (existingProduct !=null)
        {
            MessageBox.Show(
                "A product with this name already exists. Please enter a different name.",
                "Message",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        var updatedProduct = new Product
        {
            Id = _product.Id,
            Name = ProductName,
            Price = ProductPrice,

        };
         _productService.UpdateProduct(_product, updatedProduct);

        _window.Close();

    }

 



}
