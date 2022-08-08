﻿using Adesso.Application.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.Commands.Product.Create;

public class CreateProductCommand: IRequest<string>
{
    public CreateProductCommand(string name, string imagePath, double price, int categoryId, int stock)
    {
        Name = name;
        ImagePath = imagePath;
        Price = price;
        CategoryId = categoryId;
        Stock = stock;
    }

    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}
