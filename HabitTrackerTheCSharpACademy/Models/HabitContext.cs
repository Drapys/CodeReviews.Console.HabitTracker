using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerTheCSharpACademy.Models;

public partial class HabitContext : DbContext
{
    public HabitContext()
    {
    }

    public HabitContext(DbContextOptions<HabitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Habit> Habits { get; set; }

    public virtual DbSet<Occurence> Occurences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source= .\\Database\\Habit.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>(entity =>
        {
            entity.ToTable("Habit");

            entity.HasIndex(e => e.Id, "IX_Habit_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Occurence>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Occurences_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
