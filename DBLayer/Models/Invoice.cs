using System;
using System.Collections.Generic;

namespace DBLayer.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string? InvoiceNo { get; set; }

    public int? CustomerId { get; set; }

    public decimal TotalCost { get; set; }

    public byte[]? EwayImg { get; set; }

    public string? Ewaybill { get; set; }

    public bool IsActive { get; set; }

    public int? CId { get; set; }

    public DateTime? CTime { get; set; }

    public int? MId { get; set; }

    public DateTime? MTime { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
}
