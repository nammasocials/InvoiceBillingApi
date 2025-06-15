using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.VModels
{
    public class VMAddInvoiceProduct
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class VMInvoiceProduct
    {
        public int InvoiceProdId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Cost { get; set; }
        public int? CId { get; set; }
        public DateTime? CTime { get; set; }
        public int? MId { get; set; }
        public DateTime? MTime { get; set; }
    }
}
