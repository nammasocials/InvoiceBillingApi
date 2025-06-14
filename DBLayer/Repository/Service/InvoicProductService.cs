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
    public class InvoicProductService : IInvoicProductService
    {
        private readonly InvoiceBillingContext _context;
        private readonly IProductService iProductService;
        public InvoicProductService(InvoiceBillingContext context, IProductService _ProductService)
        {
            _context = context;
            iProductService = _ProductService;
        }
        public async Task<List<InvoiceProduct>> fetchAllInvoiceProductsByInvoiceId(int invoiceId)
        {
            return await _context.InvoiceProducts.Where(C => C.InvoiceId == invoiceId).ToListAsync();
        }
        public async Task<InvoiceProduct> fetchInvoiceProductsById(int invoiceProductId)
        {
            return await _context.InvoiceProducts.Where(C => C.InvoiceProdId == invoiceProductId).FirstOrDefaultAsync();
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<InvoiceProduct> AddOrEditInvoiceProducts(InvoiceProduct product, int invoiceId)
        {
            var existingInvProd = await _context.InvoiceProducts
                    .FirstOrDefaultAsync(a => a.InvoiceProdId == product.InvoiceProdId);
            if (existingInvProd != null)
            {
                existingInvProd.InvoiceId = product.InvoiceId;
                existingInvProd.ProductId = product.ProductId;
                existingInvProd.Quantity = product.Quantity;
                existingInvProd.Cost = product.Cost;
                existingInvProd.MId = 0;
                existingInvProd.MTime = DateTime.Now;
                _context.InvoiceProducts.Update(existingInvProd);
                await _context.SaveChangesAsync();
                var res = await iProductService.updateProductQuantity(product.ProductId, product.Quantity);
                return existingInvProd;
                
            }
            else
            {
                var newInvoiceProduct = new InvoiceProduct
                {
                    InvoiceId = product.InvoiceId,
                    ProductId = product.ProductId,
                    Cost = product.Cost,
                    Quantity = product.Quantity,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.InvoiceProducts.AddAsync(newInvoiceProduct);
                await _context.SaveChangesAsync();
                var res = await iProductService.updateProductQuantity(product.ProductId, product.Quantity);
                return newInvoiceProduct;
            }

        }
    }
}
