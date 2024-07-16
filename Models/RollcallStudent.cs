using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class RollcallStudent
{
    public int RollcallId { get; set; }

    public int StudentId { get; set; }

    public string Rollcall { get; set; } = null!;

    public DateTime Datetime { get; set; }
}
