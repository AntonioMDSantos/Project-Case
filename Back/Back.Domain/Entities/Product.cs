using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public class Product
    {
      
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int ProductQtd { get; set; } = 0;
            public int Purchase { get; set; } = 0;
            public int Sale { get; set; } = 0;
           public double Cost { get; set; } = 0.0;
            public double Revenue { get; set; } = 0.0;
            public double Profit { get; set; } = 0.0;
            public string? ProductImage { get; set; }

    }
    
}

