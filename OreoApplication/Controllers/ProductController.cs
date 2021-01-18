using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OreoAppBussinessLayer.IBussinessLayer;
using OreoAppCommonLayer.Model;

namespace OreoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL productBL;
        IConfiguration configuration;

        public ProductController(IProductBL productBL, IConfiguration configuration)
        {
            this.productBL = productBL;
            this.configuration = configuration;
        }
        [HttpGet("GetProducts")]
        [Authorize(Roles ="Admin,User")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> productList = this.productBL.GetAllProducts();
                if (productList != null)
                {
                    return this.Ok(new { Success = true, Message = "get Products successfully", Data = productList });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "get Products unsuccessfully",Data= productList });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message,Data=e.Data });
            }
        }
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                var result = this.productBL.AddProduct(product);
                if (result)
                {
                    return this.Ok(new { Success = true, Message = "Product added successfully",Data=result });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new { Success = false, Message = "Product is not added",Data=result });
                }
            }
            catch (Exception e)
            {
                var sqlException = e.InnerException as SqlException;

                if (sqlException.Number == 2601 || sqlException.Number == 2627)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new { Success = false, ErrorMessage = "Cannot insert duplicate Email values",Data= sqlException });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = e.Message, Data=e.Data});
                }
            }
        }
    }
}
