using System;
using System.Collections.Generic;
using ImagePortal.DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace ImagePortal.DataContext.Context;

public partial class ImageDataContext : DbContext
{
    public ImageDataContext(DbContextOptions<ImageDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImageDatum> ImageData { get; set; }

    public virtual DbSet<ImageMetaDatum> ImageMetaData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Base64URL).IsUnicode(false);
            entity.Property(e => e.FileType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImageMetaDatum>(entity =>
        {
            entity.HasKey(e => e.ImageMetaDataId);

            entity.Property(e => e.Categories)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tags).IsUnicode(false);

            entity.HasOne(d => d.Image).WithMany(p => p.ImageMetaData)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImageMetaData_ImageData");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
