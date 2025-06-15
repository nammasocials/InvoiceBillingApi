using DBLayer.Models;
using DBLayer.VModels;
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
        public Task<List<VMProduct>> fetchAllProducts();
        public Task<VMProduct> fetchProductById(int productId);
        public Task<VMProduct> fetchProductByNo(string productNo);
        public Task<VMProduct> AddOrEditProduct(VMProduct product, byte[] image);
        public Task<bool> updateProductQuantity(int productId, int quantity,int invoiceId);
    }
}
