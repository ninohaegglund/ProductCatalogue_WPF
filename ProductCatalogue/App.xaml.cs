﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Interfaces;
using Shared.Services;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace ProductCatalogue;


public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, "products.json");

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IProductService, ProductService>();
                services.AddSingleton<IFileService>( new FileService(filePath));
                services.AddSingleton<MainWindow>();

            }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
