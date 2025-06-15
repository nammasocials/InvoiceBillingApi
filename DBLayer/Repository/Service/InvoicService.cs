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
    public class InvoicService : IInvoicService
    {
        private readonly InvoiceBillingContext _context;
        private readonly IInvoicProductService iInvoicProductService;
        private readonly IMapper iMapper;
        public InvoicService(InvoiceBillingContext context, IInvoicProductService _InvoicProductService, IMapper _mapper)
        {
            _context = context;
            iInvoicProductService = _InvoicProductService;
            iMapper = _mapper;
        }
        public async Task<List<VMInvoice>> fetchAllInvoices()
        {
            var contextInvoice =  await _context.Invoices.Where(C => C.IsActive == true).ToListAsync();
            return iMapper.Map<List<VMInvoice>>(contextInvoice);
        }
        public async Task<VMInvoice> fetchInvoiceById(int invoiceId)
        {
            var invoice = await _context.Invoices.Where(C => C.IsActive == true && C.InvoiceId == invoiceId).FirstOrDefaultAsync();
            var vmInvoice = iMapper.Map<VMInvoice>(invoice);
            vmInvoice.products = await iInvoicProductService.fetchAllInvoiceProductsByInvoiceId(invoiceId);
            return vmInvoice;
        }
        /// <summary>
        /// // Add Or Edit Services
        /// </summary>
        /// <returns></returns>
        public async Task<VMInvoice> AddOrEditInvoice(VMAddInvoice invoice,List<VMAddInvoiceProduct> products, byte[] ewayBillImg)
        {
            //var contextInvoice = iMapper.Map<Invoice>(invoice);
            //var existingInvoice = await _context.Invoices
            //        .FirstOrDefaultAsync(a => a.InvoiceId == contextInvoice.InvoiceId);
            //if (existingInvoice != null)
            //{
            //    existingInvoice.InvoiceNo = contextInvoice.InvoiceNo;
            //    existingInvoice.CustomerId = contextInvoice.CustomerId;
            //    existingInvoice.TotalCost = products.Sum(I => I.Cost);
            //    existingInvoice.VehicelNo = contextInvoice.VehicelNo;
            //    existingInvoice.EwayImg = ewayBillImg;
            //    existingInvoice.Ewaybill = contextInvoice.Ewaybill;
            //    existingInvoice.IsActive = true;
            //    existingInvoice.MId = 0;
            //    existingInvoice.MTime = DateTime.Now;
            //    foreach (var prods in products)
            //    {
            //        await iInvoicProductService.AddOrEditInvoiceProducts(prods, contextInvoice.InvoiceId);
            //    }
            //    _context.Invoices.Update(existingInvoice);
            //    await _context.SaveChangesAsync();
            //    var updatedInvoice = iMapper.Map<VMInvoice>(existingInvoice);
            //    return updatedInvoice;
            //}
            //else
            //{
                var newInvoice = new Invoice
                {
                    CustomerId = invoice.CustomerId,
                    TotalCost = 0,
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
                var updatedInvoice = iMapper.Map<VMInvoice>(newInvoice);
                return updatedInvoice;
            //}
        }
    }
}
