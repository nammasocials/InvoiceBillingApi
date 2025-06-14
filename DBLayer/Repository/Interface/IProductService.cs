using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Interface
{
    public interface IProductService
    {
        public Task<List<Product>> fetchAllProducts();
        public Task<Product> fetchProductById(int productId);
        public Task<Product> fetchProductByNo(string productNo);
        public Task<Product> AddOrEditProduct(Product product, byte[] image);
        public Task<bool> updateProductQuantity(int productId, int quantity);
    }
}
