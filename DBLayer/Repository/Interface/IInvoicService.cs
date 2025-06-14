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
    public interface IInvoicService
    {
        public Task<List<Invoice>> fetchAllInvoices();
        public Task<Invoice> fetchInvoiceById(int invoiceId);
        public Task<Invoice> AddOrEditInvoice(Invoice invoice, List<InvoiceProduct> products, byte[] ewayBillImg);
        }
}
