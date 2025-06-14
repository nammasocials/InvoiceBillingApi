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
    public class InvoicService : IInvoicService
    {
        private readonly InvoiceBillingContext _context;
        private readonly IInvoicProductService iInvoicProductService;
        public InvoicService(InvoiceBillingContext context, IInvoicProductService _InvoicProductService)
        {
            _context = context;
            iInvoicProductService = _InvoicProductService;
        }
        public async Task<List<Invoice>> fetchAllInvoices()
        {
            return await _context.Invoices.Where(C => C.IsActive == true).ToListAsync();
        }
        public async Task<Invoice> fetchInvoiceById(int invoiceId)
        {
            var invoice = await _context.Invoices.Where(C => C.IsActive == true && C.InvoiceId == invoiceId).FirstOrDefaultAsync();
            invoice.InvoiceProducts = await iInvoicProductService.fetchAllInvoiceProductsByInvoiceId(invoiceId);
            return invoice;
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<Invoice> AddOrEditInvoice(Invoice invoice,List<InvoiceProduct> products, byte[] ewayBillImg)
        {
            var existingInvoice = await _context.Invoices
                    .FirstOrDefaultAsync(a => a.InvoiceId == invoice.InvoiceId);
            if (existingInvoice != null)
            {
                existingInvoice.InvoiceNo = invoice.InvoiceNo;
                existingInvoice.CustomerId = invoice.CustomerId;
                existingInvoice.TotalCost = invoice.InvoiceProducts.Sum(I => I.Cost);
                existingInvoice.VehicelNo = invoice.VehicelNo;
                existingInvoice.EwayImg = ewayBillImg;
                existingInvoice.Ewaybill = invoice.Ewaybill;
                existingInvoice.IsActive = true;
                existingInvoice.MId = 0;
                existingInvoice.MTime = DateTime.Now;
                foreach (var prods in products)
                {
                    await iInvoicProductService.AddOrEditInvoiceProducts(prods, invoice.InvoiceId);
                }
                _context.Invoices.Update(existingInvoice);
                await _context.SaveChangesAsync();
                return existingInvoice;
            }
            else
            {
                var newInvoice = new Invoice
                {
                    InvoiceNo = invoice.InvoiceNo,
                    CustomerId = invoice.CustomerId,
                    TotalCost = invoice.TotalCost,
                    VehicelNo = invoice.VehicelNo,
                    EwayImg = ewayBillImg,
                    Ewaybill = invoice.Ewaybill,
                    IsActive = true,
                    CId = 0,
                    CTime = DateTime.Now,
                };
                await _context.Invoices.AddAsync(newInvoice);
                await _context.SaveChangesAsync();
                foreach (var prods in products)
                {
                    await iInvoicProductService.AddOrEditInvoiceProducts(prods, newInvoice.InvoiceId);
                }
                return newInvoice;
            }
        }
    }
}
