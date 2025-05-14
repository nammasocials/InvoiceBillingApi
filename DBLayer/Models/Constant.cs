using System;
using System.Collections.Generic;

namespace DBLayer.Models;

public partial class Constant
{
    public byte ConstantCode { get; set; }

    public string Category { get; set; } = null!;

    public int ConstantValue { get; set; }

    public string ConstantName { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public bool Status { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
