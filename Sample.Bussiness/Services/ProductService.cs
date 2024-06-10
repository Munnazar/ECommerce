using Sample.Bussiness.Interfaces;
using Sample.Data.Repositories;
using Sample.Domain.Interfaces;
using Sample.Domain.Models;
using Sample.Models.Requests.ContactUs;
using Sample.Models.Responces;


namespace Sample.Bussiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatageryRepository _catalogRepository;
        public ProductService(IProductRepository productRepository, ICatageryRepository catalogRepository)
        {
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;

        }
        public async Task<ApiResponce<bool>> AddProduct(ProductRequest request, Product product)
        {
            var existingCategory = await _catalogRepository.GetCatageryById(request.CatageryId);
            if (existingCategory.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Category not found." };
            }

            var result = await _productRepository.AddProduct(request, product);
            if (result != null)
            {
                return new ApiResponce<bool> { Success = true, Result = result, Message = "Data Added Successfully" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Enter valid CatageryId" };
        }

        public async Task<ApiResponce<bool>> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id);

            if (existingProduct.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Product not found." };
            }
            var result = await _productRepository.DeleteProduct(id);
            if (result != null)
            {
                return new ApiResponce<bool> { Result = result, Success = true, Message = "Delete Record" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "No Record Found" };
        }

        public async Task<ApiResponce<List<Product>>> GetByCatageryId(int catageryId)
        {
            var result = await _productRepository.GetByCatageryId(catageryId);
            if (result != null && result.Count() > 0)
            {
                return new ApiResponce<List<Product>> { Result = result, Success = true, Message = "Get Data Successfully" };
            }
            return new ApiResponce<List<Product>> { Result = null, Success = false, ErrorMessage = "NO Record Found Invalid Id" };
        }

        public async Task<ApiResponce<List<Product>>> GetProductById(int id)
        {
            var result = await _productRepository.GetProductById(id);
            if (result != null && result.Count() > 0)
            {
                return new ApiResponce<List<Product>> { Result = result, Success = true, Message = "Get Data Successfully" };
            }
            return new ApiResponce<List<Product>> { Result = null, Success = false, ErrorMessage = "NO Record Found Invalid Id" };
        }

        public async Task<ApiResponce<List<Product>>> GetProducts()
        {
            var result = await _productRepository.GetProducts();
            if (result != null && result.Count() > 0)
            {
                return new ApiResponce<List<Product>> { Result = result, Success = true, Message = "Get Data Successfully" };
            }
            return new ApiResponce<List<Product>> { Result = null, Success = false, ErrorMessage = "NO Record Found" };
        }

        public async Task<ApiResponce<bool>> UpDateProduct(int id, ProductRequest request, Product product)
        {
            var existingProduct = await _productRepository.GetProductById(id);
            
            if (existingProduct.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Product not found." };
            }

            var existingCategory = await _catalogRepository.GetCatageryById(request.CatageryId);
            if (existingCategory.Count <= 0)
            {
                return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Category not found." };
            }

            var result = await _productRepository.UpDateProduct(id, request, product);
            if (result != null)
            {
                return new ApiResponce<bool> { Result = result, Success = true, Message = "Record Successfully Updated" };
            }
            return new ApiResponce<bool> { Result = false, Success = false, ErrorMessage = "Invalid Record" };
        }
    }
}
