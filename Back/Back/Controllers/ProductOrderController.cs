using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using CaseBack.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Back.Persistence.Context;

namespace Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductOrderController : ControllerBase
    {
        private readonly IProductOrderService service;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext db;

        public ProductOrderController(IProductOrderService service, IConfiguration _configuration, AppDbContext db)
        {
            this.service = service;
            this._configuration = _configuration;
            this.db = db;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateOrder(ProductOrder productOrder)
        {
            return new ResponseHelper().CreateResponse(await service.CreateOrder(productOrder));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            return new ResponseHelper().CreateResponse(await service.GetOne(id));
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteOrder(string ids)
        {
            return new ResponseHelper().CreateResponse(await service.DeleteOrder(ids));
        }

        [HttpPost]
        public async Task<IActionResult> ListOrder([FromQuery] string? searchString, int page)
        {
            return new ResponseHelper().CreateResponse(await service.ListOrder(searchString, page));
        }

        [HttpGet]
        [Route("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            return new ResponseHelper().CreateResponse(await service.ExportExcel());
        }
        [HttpGet]
        [Route("pdf")]
        public async Task<object> ExportPdf()
        {
            var Order = db.ProductOrders.ToList();

            ViewAsPdf pdf = new("Order", Order)
            
            {
                FileName = "Task.pdf",
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 8"
            };

            byte[] pdfData = pdf.BuildFile(ControllerContext).Result;
            return Convert.ToBase64String(pdfData);
        }
    }
}
