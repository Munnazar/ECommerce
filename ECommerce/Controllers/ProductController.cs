using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Bussiness.Interfaces;
using Sample.Common.Utilities;
using Sample.Models.Requests.ContactUs;
using System.IO;
using System;
using Sample.Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Open")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;

        }

        [HttpGet]
        [Route("GetProducts")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProducts();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProduct/{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProducts/{CatageryId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetByCatageryId(int CatageryId)
        {
            var result = await _productService.GetByCatageryId(CatageryId);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest request, IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                    return BadRequest("Image file is required.");

                // Check if the uploaded file is an image
                if (!image.ContentType.Contains("image"))
                    return BadRequest("Please upload an image file.");

                // Generate a unique file name
                string fileName = image.FileName; // Using the original file name

                // Get the upload directory
                string uploadDirectory = Utility.GetResourcesFolderPath();

                // Combine directory and file name to get the full path
                string filePath = Path.Combine(uploadDirectory, fileName);

                // Write the uploaded file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                //var newProduct = new ProductRequest
                //{
                //    CatageryId = request.CatageryId,
                //    Price = request.Price,
                //    ProductName = request.ProductName,
                
                //};
                var product = new Product
                {
                    Image = fileName
                };

                var res = await _productService.AddProduct(request, product);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("UpdateProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductRequest request, IFormFile file)
        {

            string uploadDirectory = Utility.GetResourcesFolderPath();
            string re = Utility.FileUpload(file, uploadDirectory);
            var fileName = Path.GetFileName(re);

            //var productRequest = new ProductRequest
            //{
            //    CatageryId = request.CatageryId,
            //    Price = request.Price,
            //    ProductName = request.ProductName,
               
            //};

            var product = new Product
            {
                Image = fileName
            };

            var result = await _productService.UpDateProduct(id, request, product);

            if (result == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

    }
}
