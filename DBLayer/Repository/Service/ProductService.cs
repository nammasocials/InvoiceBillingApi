using AutoMapper;
using DBLayer.Models;
using DBLayer.Repository.Interface;
using DBLayer.VModels;
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
        private readonly IMapper iMapper;
        public ProductService(InvoiceBillingContext context, IMapper _mapper)
        {
            _context = context;
            iMapper = _mapper;
        }
        /// <summary>
        /// // Fetch Services
        /// </summary>
        /// <returns></returns>
        public async Task<List<VMProduct>> fetchAllProducts()
        {
            var contextProduct = await _context.Products.Where(C => C.IsActive == true).ToListAsync();
            return iMapper.Map<List<VMProduct>>(contextProduct);
        }
        public async Task<VMProduct> fetchProductById(int productId)
        {
            var contextProduct = await _context.Products.Where(C => C.IsActive == true && C.ProductId == productId).FirstOrDefaultAsync();
            return iMapper.Map<VMProduct>(contextProduct);
        }
        public async Task<VMProduct> fetchProductByNo(string productNo)
        {
            var contextProduct = await _context.Products.Where(C => C.IsActive == true && C.ProductNo == productNo).FirstOrDefaultAsync();
            return iMapper.Map<VMProduct>(contextProduct);
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<VMProduct> AddOrEditProduct(VMProduct product, byte[] image)
        {
            var mappedProduct = iMapper.Map<Product>(product);
            var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(a => a.ProductId == mappedProduct.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = mappedProduct.ProductName;
                existingProduct.Img = image;
                existingProduct.Cost = mappedProduct.Cost;
                existingProduct.Quantity = mappedProduct.Quantity;
                existingProduct.IsActive = true;
                existingProduct.Hsncode = mappedProduct.Hsncode;
                existingProduct.MId = 0;
                existingProduct.MTime = DateTime.Now;
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
                var updatedProduct = iMapper.Map<VMProduct>(existingProduct);
                return updatedProduct;
            }
            else
            {
                var newProduct = new Product
                {
                    ProductName = mappedProduct.ProductName,
                    Img = image,
                    Cost = mappedProduct.Cost,
                    Quantity = mappedProduct.Quantity,
                    IsActive = true,
                    Hsncode = mappedProduct.Hsncode,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                var updatedProduct = iMapper.Map<VMProduct>(newProduct);
                return updatedProduct;
            }
        }
        public async Task<bool> updateProductQuantity(int productId,int quantity, int invoiceId)
        {
            var existingProduct = await _context.Products
                     .FirstOrDefaultAsync(a => a.ProductId == productId);
            if (existingProduct != null)
            {
                existingProduct.Quantity = existingProduct.Quantity - quantity;
                existingProduct.MId = 0;
                existingProduct.MTime = DateTime.Now;
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
                var existingInvoice = await _context.Invoices
                    .FirstOrDefaultAsync(a => a.InvoiceId == invoiceId);
                if (existingProduct != null)
                {
                    return true; 
                }
            }
            return false;
        }
    }
}
