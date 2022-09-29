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
    public class ProductService : IProductService
    {
        private readonly AppDbContext db;
        public IConfiguration configuration { get; }
        public ProductService(AppDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public async Task<ResponseModel> Add(Product product)
        {
            try
            {
                var findProduct = await db.Products.Where(x => x.ProductName == product.ProductName).FirstOrDefaultAsync();

                if (findProduct == null) 
                {
                    if (!String.IsNullOrEmpty(product.ProductImage) && !product.ProductImage.Contains("Uploads/Products"))
                    {
                        product.ProductImage = MediaHelper.SaveMedia($"wwwroot/Uploads/Products/{product.ProductName}/Photos", product.ProductImage);
                    }
                    db.Products.Add(product);
                    db.SaveChanges();

                    return new ResponseModel(200, "Produto criado com sucesso", product);
                }

                return new ResponseModel(409, "Produto já existe");
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
                var product = await db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ResponseModel(404, "Produto não encontrado");
                }

                return new ResponseModel(200, product);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> Delete(string ids)
        {
            try
            {
                var arrIds = ids.Split(',');
                foreach (string element in arrIds)
                {
                var product = await db.Products.Where(x => x.Id == Convert.ToInt32(element)).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ResponseModel(404, "Produto não encontrado");
                }
                db.Products.Remove(product);
                db.SaveChanges();
                }
                return new ResponseModel(200, "Ordem deletada");
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> Update(Product product)
        {
            try
            {
                var Findproduct = await db.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();

                if (Findproduct == null)
                {
                    return new ResponseModel(404, "Produto não encontrado");
                }
                if (!String.IsNullOrEmpty(product.ProductImage) && !product.ProductImage.Contains("Uploads/Products"))
                {
                    product.ProductImage = MediaHelper.SaveMedia($"wwwroot/Uploads/Products/{product.ProductName}/Photos", product.ProductImage);
                    Findproduct.ProductImage = product.ProductImage;
                }

                Findproduct.ProductName = product.ProductName;
                Findproduct.ProductQtd = product.ProductQtd;
                Findproduct.ProductImage = product.ProductImage;
                db.Products.Update(Findproduct);
                await db.SaveChangesAsync();
                return new ResponseModel(200, Findproduct);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }

        public async Task<ResponseModel> ListProduct(string? searchString, int page)
        {
            try
            {
                {
                    var products = !String.IsNullOrEmpty(searchString) ? db.Products.Where(x => x.ProductName!.Contains(searchString)) : db.Products;

                    var Pager = new Pager(products.Count(), page, 15);
                    products = products.Skip((Pager.CurrentPage - 1) * Pager.PageSize).Take(Pager.PageSize);

                    var model = new ProductModel(await products.ToListAsync(), Pager);

                    return new ResponseModel(200, model);
                }
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
                var findProduct = await db.Products.ToListAsync();
                var excel = ExcelHelper.GenerateExcel(findProduct);
                return new ResponseModel(200, excel);
            }
            catch (Exception e)
            {
                return new ResponseModel(500, e.Message);
            }
        }          
    }
}
