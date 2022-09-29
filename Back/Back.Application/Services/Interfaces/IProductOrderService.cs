using Back.Domain.Entities;
using CaseBack.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Services.Interfaces
{
    public interface IProductOrderService
    {
        Task<ResponseModel> CreateOrder(ProductOrder productOrder);
        Task<ResponseModel> GetOne(int id);
        Task<ResponseModel> DeleteOrder(string ids);
        Task<ResponseModel> ListOrder(string? searchString, int page);
        Task<ResponseModel> ExportExcel();
    }
}
