using Back.Domain.Entities;
using CaseBack.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Services.Interfaces
{
    public interface IProductService
    { 

        Task<ResponseModel> Add(Product product);
        Task<ResponseModel> GetOne(int id);
        Task<ResponseModel> Delete(string ids);
        Task<ResponseModel> Update(Product product);
        Task<ResponseModel> ListProduct(string? searchString, int page);
        Task<ResponseModel> ExportExcel();
    }
}
