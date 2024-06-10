using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Data.Repositories
{
    public class CategoryRepository : ICatageryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AddCatagery(CatageryRequest request)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Name", request.Name);

                var result = await connection.ExecuteAsync("AddNewCategory", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> DeleteCatagery(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var parameters = new
                {
                    Id = id 
                };

               var result = await connection.ExecuteAsync("DeleteCategoryById", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }

        public async Task<List<Catagery>> GetCatageries()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Catagery>("GetAllCategories", commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<Catagery>> GetCatageryById(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new 
                { 
                    Id = id 
                };

                var result = await connection.QueryAsync<Catagery>("GetCategoryById", parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<bool> UpDateCatagery(int id, CatageryRequest request)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new 
                {
                    Id = id,
                    Name = request.Name 
                };

                var result = await connection.ExecuteAsync("UpdateCategoryById", parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }

        }
    }
}
