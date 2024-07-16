using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class AskForLeave
{
    public int AskForLeaveId { get; set; }

    public int StudentId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime Date { get; set; }

    public string SelectedOption { get; set; } = null!;
}
