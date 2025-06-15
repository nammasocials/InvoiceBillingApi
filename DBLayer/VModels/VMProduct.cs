using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.VModels
{
    public class VMProduct
    {
        public int ProductId { get; set; }

        public string? ProductNo { get; set; }

        public string? ProductName { get; set; }

        public byte[]? Img { get; set; }

        public decimal Cost { get; set; }

        public bool IsActive { get; set; }

        public int Quantity { get; set; }

        public string Hsncode { get; set; } = null!;

        public int? CId { get; set; }

        public DateTime? CTime { get; set; }

        public int? MId { get; set; }

        public DateTime? MTime { get; set; }
    }
}
