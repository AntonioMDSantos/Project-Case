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
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        private readonly IConfiguration _configuration;
        private readonly AppDbContext db;

        public ProductController(IProductService service, IConfiguration _configuration, AppDbContext db)
        {
            this.service = service;
            this._configuration = _configuration;
            this.db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            return new ResponseHelper().CreateResponse(await service.GetOne(id));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(Product product)
        {
            return new ResponseHelper().CreateResponse(await service.Add(product));
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] string ids)
        {
            return new ResponseHelper().CreateResponse(await service.Delete(ids));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(Product product)
        {
            return new ResponseHelper().CreateResponse(await service.Update(product));
        }

        [HttpPost]
        public async Task<IActionResult> ListProduct([FromQuery] string? searchString, int page)
        {
            return new ResponseHelper().CreateResponse(await service.ListProduct(searchString, page));
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
            var findProduct = db.Products.ToList();

            ViewAsPdf pdf = new("Product", findProduct)
            
            {
                FileName = "Task.pdf",
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 8"
            };

            byte[] pdfData = pdf.BuildFile(ControllerContext).Result;
            return Convert.ToBase64String(pdfData);
        }
    }
}
