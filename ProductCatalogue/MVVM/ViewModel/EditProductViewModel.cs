

using ProductCatalogue.Core;
using Shared.Services;
using Shared.Models;
using System.Windows.Input;

namespace ProductCatalogue.MVVM.ViewModel;

public class EditProductViewModel : ObservableObject
{
    private string _productName;
    private decimal _productPrice;
    private readonly ProductService _productService;

    public EditProductViewModel(Product product, ProductService productService)
    {
        _productName = product.Name;
        _productPrice = product.Price;
        _productService = productService;

        SaveCommand = new RelayCommand(SaveProduct);
        CancelCommand = new RelayCommand(Cancel);
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
    public ICommand CancelCommand { get; }

    public void SaveProduct(object parameter)
    {

    }

    public void Cancel(object parameter)
    {

    }



}
