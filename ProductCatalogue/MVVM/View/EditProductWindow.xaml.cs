
using System.Windows;
using ProductCatalogue.MVVM.ViewModel;
using Shared.Services;
using Shared.Models;



namespace ProductCatalogue.MVVM.View;


public partial class EditProductWindow : Window
{
    public EditProductWindow(Product product, ProductService productService)
    {
        InitializeComponent();
        this.DataContext = new EditProductViewModel(product, productService);
    }


    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        //edit button functionality
    }
}
