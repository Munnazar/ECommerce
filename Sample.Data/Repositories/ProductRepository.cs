using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task<bool> AddProduct(ProductRequest request, Product product)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@CatageryId", request.CatageryId);
                parameters.Add("@Price", request.Price);
                parameters.Add("@ProductName", request.ProductName);
                parameters.Add("@Image", product.Image);
                //parameters.Add("@ImageFile", request.ImageFile);

                var result = await connection.ExecuteAsync("AddProduct", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }

        public async Task<bool> DeleteProduct(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new
                {
                    Id = id
                };

               var result = await connection.ExecuteAsync("DeleteProductById", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }

        public async Task<List<Product>> GetByCatageryId(int catageryId)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@CategoryId", catageryId); // Make sure categoryId is the correct value

                var result = await connection.QueryAsync<Product>("GetProductsByCategoryId", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<List<Product>> GetProductById(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new 
                {
                    Id = id
                };

                var result = await connection.QueryAsync<Product>("GetProductById", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }

        }

        public async Task<List<Product>> GetProducts()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var result = await connection.QueryAsync<Product>("GetAllProductsWithCategoryName", commandType: CommandType.StoredProcedure);

                return result.ToList();
            }

        }

        //public async Task<bool> UpDateProduct(int id, ProductRequest request, Product product)
        //{

        //    using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();
        //        var parameters = new
        //        {
        //            Id = id,
        //            CatageryId = request.CatageryId,
        //            ProductName = request.ProductName,
        //            Price = request.Price,
        //            Image = product.Image
        //        };

        //       var result = await connection.ExecuteAsync("UpdateProductIfCategoryExists", parameters, commandType: CommandType.StoredProcedure);
        //        return result > 0;
        //    }

        //}

        public async Task<bool> UpDateProduct(int id, ProductRequest request, Product product)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@CatageryId", request.CatageryId);
                parameters.Add("@ProductName", request.ProductName);
                parameters.Add("@Price", request.Price);
                parameters.Add("@Image", product.Image);

                var result = await connection.ExecuteAsync("UpdateProductIfCategoryExists", parameters, commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

    }
}
