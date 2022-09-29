using Back.Application.Models;
using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using Back.Persistence.Context;
using CaseBack.Application.Models;
using CaseBack.Application.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Services
{
    public class ProductOrderService : IProductOrderService
    {
        private readonly AppDbContext db;
        public IConfiguration configuration { get; }
        public ProductOrderService(AppDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public async Task<ResponseModel> CreateOrder(ProductOrder productOrder)
        {
            try
            {
                // IQueryable<ProductOrder> products = db.ProductOrders.Include(p => p.product);
                var product = await db.Products.Where(x => x.Id == productOrder.ProductId).FirstOrDefaultAsync();

                if (product != null)
                {
                    var quantityOrder = productOrder.Quantity;
                    if (productOrder.OrderType == 0)
                    {
                        product.ProductQtd += quantityOrder;
                        db.Products.Update(product);
                        await db.ProductOrders.AddAsync(productOrder);
                        await db.SaveChangesAsync();
                        return new ResponseModel(201, "Ordem do produto criada!");
                    }

                    if (product.ProductQtd >= quantityOrder)
                    {
                        product.ProductQtd -= quantityOrder;
                        db.Products.Update(product);
                        await db.ProductOrders.AddAsync(productOrder);
                        await db.SaveChangesAsync();
                        return new ResponseModel(201, "Ordem do produto criada!");
                    }
                    else
                    {
                        return new ResponseModel(406, "Quantidade do produto insuficiente!");
                    }
                }
                else
                {
                    return new ResponseModel(409, "Produto não encontrado!");
                }
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }

        }
        public async Task<ResponseModel> GetOne(int id)
        {
            try
            {
                var product = await db.ProductOrders.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ResponseModel(404, "Ordem não encontrada");
                }

                return new ResponseModel(200, product);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
        public async Task<ResponseModel> DeleteOrder(string ids)
        {
            try
            {
                var arrIds = ids.Split(',');
                foreach (string element in arrIds)
                {
                var findOrder = await db.ProductOrders.Where(x => x.Id == Convert.ToInt32(element)).FirstOrDefaultAsync();
                if (findOrder != null)
                {
                    db.Remove(findOrder);
                    db.SaveChanges();

                    return new ResponseModel(200, "Ordem deletada!");
                }

                }
                return new ResponseModel(404, "Ordem não encontrada!");
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
        public async Task<ResponseModel> ListOrder(string? searchString, int page)
        {
            try
            {
                IQueryable<ProductOrder> orders = db.ProductOrders.Include(p => p.product);

                orders = !String.IsNullOrEmpty(searchString) ? db.ProductOrders.Where(x => !String.IsNullOrEmpty(x.product.ProductName) && x.product.ProductName!.Contains(searchString)) : orders;

                var Pager = new Pager(orders.Count(), page, 15);
                orders = orders.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);

                var model = new ProductOrderModel(await orders.ToListAsync(), Pager);

                return new ResponseModel(200, model);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
        public async Task<ResponseModel> ExportExcel()
        {
            try
            {
                var orders = await db.ProductOrders.ToListAsync();
                var excel = ExcelHelper.GenerateExcel(orders);
                return new ResponseModel(200, excel, "Ordem gerada em pdf");
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }
    }
}
