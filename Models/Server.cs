using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class Server
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public string? Name { get; set; }

    public string? Major { get; set; }

    public string? Grade { get; set; }

    public string? Class { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Idnumber { get; set; }

    public string? InitialPassword { get; set; }

    public DateTime? Date { get; set; }

    public string? SelectedOption { get; set; }
}
