using System;
using System.Collections.Generic;

namespace DBLayer.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CTime { get; set; }

    public bool? IsActive { get; set; }
}
