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
        => optionsBuilder.UseSqlite($"Data Source={Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Database\\Habit.db"))}");

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
