using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.VModels
{
    public class VMCustomer
    {
        public int CustomerId { get; set; }

        public string? CustomerNo { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? ContactNo { get; set; }

        public string? EmailId { get; set; }

        public string Gstno { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string PinCode { get; set; } = null!;

        public bool? IsActive { get; set; }

        public int? CId { get; set; }

        public DateTime? CTime { get; set; }

        public int? MId { get; set; }

        public DateTime? MTime { get; set; }
    }
}
