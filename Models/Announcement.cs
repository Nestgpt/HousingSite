using System;
using System.Collections.Generic;

namespace WebA.Models;

public partial class Announcement
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime ReleaseDate { get; set; }
}
