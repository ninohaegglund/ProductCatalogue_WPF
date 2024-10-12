
using System.Collections.ObjectModel;

using Moq;
using Shared.Models;
using Shared.Services;

namespace Shared.Tests;

public class ProductService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ProductService _productService;

    public ProductService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();

        
        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns("");

       
        _productService = new ProductService(_fileServiceMock.Object);
    }

    [Fact]//Test for adding a product to the list
    public void AddToList_ShouldAddProduct_WhenProductIsValid()
    {
        // Arrange
        var product = new Product { Name = "Test Product", Price = 10.0M };

        // Act
        var result = _productService.AddToList(product);

        // Assert
        Assert.True(result); 
        Assert.Single(_productService.GetProducts()); 
        Assert.Contains(product, _productService.GetProducts()); 
    }
 

    [Fact] //Test verifying correct amount is added to the list
    public void GetProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var product1 = new Product { Name = "Product 1", Price = 5.0M };
        var product2 = new Product { Name = "Product 2", Price = 10.0M };
        _productService.AddToList(product1);
        _productService.AddToList(product2);

        // Act
        var products = _productService.GetProducts();

        // Assert
        Assert.Equal(2, ((Collection<Product>)products).Count);
    }
    [Fact]//Test for deleting a product in the list
    public void DeleteProduct_ShouldRemoveExistingProductFromList()
    {
        // Arrange
        var product1 = new Product { Id = Guid.NewGuid().ToString(), Name = "Product 1", Price = 5.0M };
        var product2 = new Product { Id = Guid.NewGuid().ToString(), Name = "Product 2", Price = 10.0M };
        _productService.AddToList(product1);
        _productService.AddToList(product2);

        // Act
        _productService.DeleteProduct(product1);

        // Assert
       var products = _productService.GetProducts().ToList();
       Assert.Single(products);
       Assert.DoesNotContain(products, p => p.Id ==product1.Id);
    }
    [Fact]//Test for updating a product in the list
    public void UpdateProduct_ShouldUpdateExistingProductInList()
    {
        // Arrange
        var product1 = new Product { Id = Guid.NewGuid().ToString(), Name = "Product 1", Price = 5.0M };
        var product2 = new Product { Id = Guid.NewGuid().ToString(), Name = "Product 2", Price = 10.0M };
        _productService.AddToList(product1);
        _productService.AddToList(product2);

        var updatedProduct = new Product { Id = product1.Id, Name = "Updated Product 1", Price = 15.0M };

        // Act
        _productService.UpdateProduct(product1, updatedProduct);

        // Assert
        var products = _productService.GetProducts().ToList();
        Assert.Equal(2, products.Count); //Verifying that both products are still in the list
        Assert.Contains(products, p => p.Id == product1.Id && p.Name == updatedProduct.Name && p.Price == updatedProduct.Price);
    }

}

//Utöka dina enhetstester till att inkludera tester för:
//Att ta bort en produkt från listan; 
//Att uppdatera en produkt.; 
//Att spara och läsa in produkter från fil.