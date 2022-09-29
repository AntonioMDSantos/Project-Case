using Back.Domain.Entities;
using CaseBack.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Models
{
    public class ProductModel
    {
        public List<Product> products { get; set; }
        public Pager pager { get; set; }

        public ProductModel(List<Product> products, Pager pager) 
        { 
            this.products = products; 
            this.pager = pager; 
        }
    }
}