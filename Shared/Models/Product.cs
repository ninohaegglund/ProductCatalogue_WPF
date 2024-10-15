﻿using Shared.Enums;

namespace Shared.Models;
public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string PriceFormatted { get; set; } = string.Empty;
    public Category Category { get; set; }

}
