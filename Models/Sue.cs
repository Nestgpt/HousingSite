using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class Sue
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string? Reason { get; set; }

    public int Roomnumber { get; set; }

    public string? SelectedOption { get; set; }
}
