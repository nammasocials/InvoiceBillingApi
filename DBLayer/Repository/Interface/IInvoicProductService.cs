using DBLayer.Models;
using DBLayer.Repository.Service;
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
        public Task<List<InvoiceProduct>> fetchAllInvoiceProductsByInvoiceId(int invoiceId);
        public Task<InvoiceProduct> fetchInvoiceProductsById(int invoiceProductId);
        public Task<InvoiceProduct> AddOrEditInvoiceProducts(InvoiceProduct product, int invoiceId);
    }
}
