using System;
using System.Collections.Generic;

namespace DBLayer.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductNo { get; set; }

    public string? ProductName { get; set; }

    public byte[]? Img { get; set; }

    public decimal Cost { get; set; }

    public bool IsActive { get; set; }

    public string Hsncode { get; set; } = null!;

    public int? CId { get; set; }

    public DateTime? CTime { get; set; }

    public int? MId { get; set; }

    public DateTime? MTime { get; set; }

    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
}
