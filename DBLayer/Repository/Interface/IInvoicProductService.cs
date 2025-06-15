using DBLayer.Models;
using DBLayer.Repository.Service;
using DBLayer.VModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Repository.Interface
{
    public interface IInvoicProductService
    {
        public Task<List<VMInvoiceProduct>> fetchAllInvoiceProductsByInvoiceId(int invoiceId);
        public Task<VMInvoiceProduct> fetchInvoiceProductsById(int invoiceProductId);
        public Task<VMInvoiceProduct> AddOrEditInvoiceProducts(VMAddInvoiceProduct product, int invoiceId);
    }
}
