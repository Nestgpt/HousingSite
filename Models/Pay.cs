using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebA.Models;

public partial class Pay
{
    public int Id { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageMimeType { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
