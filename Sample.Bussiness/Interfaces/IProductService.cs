using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using Sample.Models.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Bussiness.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponce<List<Product>>> GetProducts();
        Task<ApiResponce<List<Product>>> GetProductById(int id);
        Task<ApiResponce<List<Product>>> GetByCatageryId(int catageryId);
        Task<ApiResponce<bool>> AddProduct(ProductRequest request, Product product);
        Task<ApiResponce<bool>> UpDateProduct(int id, ProductRequest request, Product product);
        Task<ApiResponce<bool>> DeleteProduct(int id);
    }
}
