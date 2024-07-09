using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApi.Models;

public partial class ManageContext : DbContext
{
    public ManageContext()
    {
    }

    public ManageContext(DbContextOptions<ManageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC27601A7C93");

            entity.ToTable("Book");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Genre)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Title).HasMaxLength(35);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
    }

    //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}