
using System.Windows;
using ProductCatalogue.MVVM.ViewModel;
using Shared.Services;
using Shared.Models;



namespace ProductCatalogue.MVVM.View;


public partial class EditProductWindow : Window
{
    public EditProductWindow(Product product, IProductService productService)
    {
        InitializeComponent();
        this.DataContext = new EditProductViewModel(product, productService, this);
    }

}
