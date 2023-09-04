using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Test.Models;

public partial class CorsoContext : DbContext
{
    public CorsoContext()
    {
    }

    public CorsoContext(DbContextOptions<CorsoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllieviLezione> AllieviLeziones { get; set; }

    public virtual DbSet<Allievo> Allievos { get; set; }

    public virtual DbSet<Insegnanti> Insegnantis { get; set; }

    public virtual DbSet<Lezioni> Lezionis { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllieviLezione>(entity =>
        {
            entity.HasKey(e => e.Allievo);

            entity.ToTable("AllieviLezione");

            entity.HasIndex(e => e.Allievo, "IX_AllieviLezione").IsUnique();

            entity.Property(e => e.Allievo).ValueGeneratedNever();
            entity.Property(e => e.IdLezione).HasColumnName("Id_Lezione");

            entity.HasOne(d => d.AllievoNavigation).WithOne(p => p.AllieviLezione)
                .HasForeignKey<AllieviLezione>(d => d.Allievo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllieviLezione_Allievo");

            entity.HasOne(d => d.IdLezioneNavigation).WithMany(p => p.AllieviLeziones)
                .HasForeignKey(d => d.IdLezione)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AllieviLezione_Lezioni");
        });

        modelBuilder.Entity<Allievo>(entity =>
        {
            entity.HasKey(e => e.IdAllievo);

            entity.ToTable("Allievo");

            entity.Property(e => e.IdAllievo).HasColumnName("Id_Allievo");
            entity.Property(e => e.Cognome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorsoScelto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Corso_scelto");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Insegnanti>(entity =>
        {
            entity.HasKey(e => e.IdInsegnante);

            entity.ToTable("Insegnanti");

            entity.HasIndex(e => e.Lezione, "IX_Insegnanti").IsUnique();

            entity.Property(e => e.IdInsegnante).HasColumnName("Id_Insegnante");
            entity.Property(e => e.Cognome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lezioni>(entity =>
        {
            entity.HasKey(e => e.IdLezioni);

            entity.ToTable("Lezioni");

            entity.Property(e => e.IdLezioni)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id_Lezioni");
            entity.Property(e => e.CodCorso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Cod_Corso");
            entity.Property(e => e.Corso)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLezioniNavigation).WithOne(p => p.Lezioni)
                .HasPrincipalKey<Insegnanti>(p => p.Lezione)
                .HasForeignKey<Lezioni>(d => d.IdLezioni)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lezioni_Insegnanti");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
