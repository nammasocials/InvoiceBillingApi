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
    public interface IInvoicService
    {
        public Task<List<VMInvoice>> fetchAllInvoices();
        public Task<VMInvoice> fetchInvoiceById(int invoiceId);
        public Task<VMInvoice> AddOrEditInvoice(VMAddInvoice invoice, List<VMAddInvoiceProduct> products, byte[] ewayBillImg);
        }
}
