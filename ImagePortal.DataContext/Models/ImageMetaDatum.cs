using System;
using System.Collections.Generic;

namespace ImagePortal.DataContext.Models;

public partial class ImageMetaDatum
{
    public int ImageMetaDataId { get; set; }

    public string? Tags { get; set; }

    public string? Categories { get; set; }

    public int ImageId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ImageDatum Image { get; set; } = null!;
}
