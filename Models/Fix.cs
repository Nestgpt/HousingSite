using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class Fix
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Project { get; set; }

    public string? Site { get; set; }

    public string? SelectedOption { get; set; }
}
