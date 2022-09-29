using Back.Domain.Entities;
using CaseBack.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Models
{
    public class ProductOrderModel
    {
        public List<ProductOrder> orders { get; set; }
        public Pager pager { get; set; }

        public ProductOrderModel(List<ProductOrder> orders, Pager pager)
        {
            this.orders = orders;
            this.pager = pager;
        }
    }
}