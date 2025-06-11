using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
: IdentityDbContext<User>(options)
{
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Tipo> Tipos => Set<Tipo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tipo>().HasData(
            new Tipo {Id = 1, Name = "Standard", Price = 200.00m},
            new Tipo { Id = 2, Name = "Deluxe", Price = 350.00m },
            new Tipo { Id = 3, Name = "Suite Presidencial", Price = 1000.00m }

        );
    }
    }
