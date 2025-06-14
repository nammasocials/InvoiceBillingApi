using DBLayer.Models;
using DBLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Service
{
    public class ProductService : IProductService
    {
        private readonly InvoiceBillingContext _context;
        public ProductService(InvoiceBillingContext context)
        {
            _context = context;
        }
        /// <summary>
        /// // Fetch Services
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> fetchAllProducts()
        {
            return await _context.Products.Where(C => C.IsActive == true).ToListAsync();
        }
        public async Task<Product> fetchProductById(int productId)
        {
            return await _context.Products.Where(C => C.IsActive == true && C.ProductId == productId).FirstOrDefaultAsync();
        }
        public async Task<Product> fetchProductByNo(string productNo)
        {
            return await _context.Products.Where(C => C.IsActive == true && C.ProductNo == productNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<Product> AddOrEditProduct(Product product, byte[] image)
        {
            var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(a => a.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Img = image;
                existingProduct.Cost = product.Cost;
                existingProduct.Quantity = product.Quantity;
                existingProduct.IsActive = product.IsActive;
                existingProduct.Hsncode = product.Hsncode;
                existingProduct.MId = 0;
                existingProduct.MTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return existingProduct;
            }
            else
            {
                var newProduct = new Product
                {
                    ProductName = product.ProductName,
                    Img = image,
                    Cost = product.Cost,
                    Quantity = product.Quantity,
                    IsActive = product.IsActive,
                    Hsncode = product.Hsncode,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return newProduct;
            }
        }
    }
}
