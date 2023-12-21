using System;
using System.Collections.Generic;

namespace ImagePortal.DataContext.Models;

public partial class ImageDatum
{
    public int ImageId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] Data { get; set; }

    public string FileType { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<ImageMetaDatum> ImageMetaData { get; set; } = new List<ImageMetaDatum>();
}
