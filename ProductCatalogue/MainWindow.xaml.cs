using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ProductCatalogue.MVVM.View;
using Shared.Enums;
using Shared.Models;
using Shared.Services;


namespace ProductCatalogue;

public partial class MainWindow : Window
{
    private readonly IProductService _productService;
    private ObservableCollection<Product> _products = [];

    public MainWindow(IProductService productService)
    {
        InitializeComponent();
        _productService = productService;
        LvProducts.ItemsSource = _products;
        LoadProducts();
        LoadCategories();

    }
    private void LoadCategories()
    {
        CbCategories.ItemsSource = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();
    }
    private void LoadProducts()
    {
        _products.Clear();
        foreach (var product in _productService.GetProducts())
        {
            product.PriceFormatted = product.Price.ToString("C");
            _products.Add(product);
        }

       
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        if(string.IsNullOrEmpty(InputName.Text))
        {
            MessageBox.Show(
                "Please enter a valid name",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        if(CbCategories.SelectedItem == null)                                                                                                                                                                                                                                                                                                                                                                                       
        {
            MessageBox.Show(
               "Please select a category",
               "Error",
               MessageBoxButton.OK,
               MessageBoxImage.Information);             
            return;
        }

        if (decimal.TryParse(InputPrice.Text, out decimal price))
        {
            var product = new Product
            {
                Name = InputName.Text,
                Price = price,
                Category = (Category)CbCategories.SelectedItem
            };
            if (_productService!.ProductExists(product.Name))
            {
                MessageBox.Show(
                $"A product with the name '{product.Name}' already exists. Please enter a new product name..",
                "Confirm",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            else
            {
               _productService.AddToList(product);
               InputName.Text = "";
               InputPrice.Text = "";
               CbCategories.SelectedIndex = -1; //reset selection after adding product
               LoadProducts();
            }          
        }     
        else
        {
            MessageBox.Show(
            "Invalid price. Please enter a valid number.",
            "Confirm",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }
    }

    private void BtnExit_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show(
            "Are you sure you want to exit?",
            "Confirm exit",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    private void Btn_Delete_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var product = button?.DataContext as Product;

        if (product != null)
        {
            _productService.DeleteProduct(product);
            _products.Remove(product);

        }
        

    }

    private void Btn_Edit_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var product = button?.DataContext as Product;

        if (product != null)
        { 
            var editProductWindow = new EditProductWindow(product, _productService);
            editProductWindow.ShowDialog();
            LoadProducts();
            
        }
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchInput = SearchBox.Text.ToLower();

        var filteredProducts = _productService.GetProducts().
            Where(p => p.Name.ToLower().Contains(searchInput) ||
            p.Category.ToString().ToLower().Contains(searchInput)).
            ToList();

        _products.Clear();
        foreach (var product in filteredProducts)
        {
            product.PriceFormatted = product.Price.ToString("C");
            _products.Add(product);
        }


    }
}