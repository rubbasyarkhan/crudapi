using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Crud
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }
}
