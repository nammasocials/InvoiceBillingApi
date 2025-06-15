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
    public class InvoicProductService : IInvoicProductService
    {
        private readonly InvoiceBillingContext _context;
        private readonly IProductService iProductService;
        private readonly IMapper iMapper;
        public InvoicProductService(InvoiceBillingContext context, IProductService _ProductService, IMapper _mapper)
        {
            _context = context;
            iProductService = _ProductService;
            iMapper = _mapper;
        }
        public async Task<List<VMInvoiceProduct>> fetchAllInvoiceProductsByInvoiceId(int invoiceId)
        {
            var contextInvoiceProduct = await _context.InvoiceProducts.Where(C => C.InvoiceId == invoiceId).ToListAsync();
            return iMapper.Map<List<VMInvoiceProduct>>(contextInvoiceProduct);
        }
        public async Task<VMInvoiceProduct> fetchInvoiceProductsById(int invoiceProductId)
        {
            var contextInvoiceProduct = await _context.InvoiceProducts.Where(C => C.InvoiceProdId == invoiceProductId).FirstOrDefaultAsync();
            return iMapper.Map<VMInvoiceProduct>(contextInvoiceProduct);
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<VMInvoiceProduct> AddOrEditInvoiceProducts(VMAddInvoiceProduct product, int invoiceId)
        {
            //var mappedInvoiceProduct = iMapper.Map<InvoiceProduct>(product);
            //var existingInvProd = await _context.InvoiceProducts
            //        .FirstOrDefaultAsync(a => a.InvoiceProdId == mappedInvoiceProduct.InvoiceProdId);
            //if (existingInvProd != null)
            //{
            //    //existingInvProd.InvoiceId = mappedInvoiceProduct.InvoiceId;
            //    existingInvProd.ProductId = mappedInvoiceProduct.ProductId;
            //    existingInvProd.Quantity = mappedInvoiceProduct.Quantity;
            //    existingInvProd.Cost = mappedInvoiceProduct.Cost;
            //    existingInvProd.MId = 0;
            //    existingInvProd.MTime = DateTime.Now;
            //    _context.InvoiceProducts.Update(existingInvProd);
            //    await _context.SaveChangesAsync();
            //    var res = await iProductService.updateProductQuantity(mappedInvoiceProduct.ProductId, mappedInvoiceProduct.Quantity);
            //    var updatedInvoiceProduct = iMapper.Map<VMInvoiceProduct>(mappedInvoiceProduct);
            //    return updatedInvoiceProduct;
                
            //}
            //else
            //{
                var contextProduct = await iProductService.fetchProductById(product.ProductId);
                var newInvoiceProduct = new InvoiceProduct
                {
                    InvoiceId = invoiceId,
                    ProductId = product.ProductId,
                    UnitCost = contextProduct.Cost,
                    Cost = contextProduct.Cost * product.Quantity,
                    //Cost = mappedInvoiceProduct.Cost,
                    Quantity = product.Quantity,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.InvoiceProducts.AddAsync(newInvoiceProduct);
                await _context.SaveChangesAsync();
                var res = await iProductService.updateProductQuantity(product.ProductId, product.Quantity, invoiceId);
                var updatedInvoiceProduct = iMapper.Map<VMInvoiceProduct>(newInvoiceProduct);
                return updatedInvoiceProduct;
            //}

        }
    }
}
