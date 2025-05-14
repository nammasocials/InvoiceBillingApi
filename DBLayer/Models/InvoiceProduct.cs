using System;
using System.Collections.Generic;

namespace DBLayer.Models;

public partial class InvoiceProduct
{
    public int InvoiceProdId { get; set; }

    public int InvoiceId { get; set; }

    public int ProductId { get; set; }

    public decimal Cost { get; set; }

    public int? CId { get; set; }

    public DateTime? CTime { get; set; }

    public int? MId { get; set; }

    public DateTime? MTime { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
