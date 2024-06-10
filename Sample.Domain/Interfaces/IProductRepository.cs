using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductById(int id);
        Task<List<Product>> GetByCatageryId(int catageryId);
        Task<bool> AddProduct(ProductRequest request, Product product);
        Task<bool> UpDateProduct(int id, ProductRequest request, Product product);
        Task<bool> DeleteProduct(int id);
    }
}
