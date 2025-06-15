using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.VModels
{
    public class VMInvoice
    {
        public int InvoiceId { get; set; }

        public string? InvoiceNo { get; set; }

        public int? CustomerId { get; set; }

        public decimal TotalCost { get; set; }

        public string? VehicelNo { get; set; }

        public byte[]? EwayImg { get; set; }

        public string? Ewaybill { get; set; }

        public bool IsActive { get; set; }

        public int? CId { get; set; }

        public DateTime? CTime { get; set; }

        public int? MId { get; set; }

        public DateTime? MTime { get; set; }
        public List<VMInvoiceProduct> products { get; set; }
    }
    public class VMAddInvoice
    {
        public int? CustomerId { get; set; }

        public string? VehicelNo { get; set; }

        public string? Ewaybill { get; set; }
    }
}
