using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Data.Mapping;



namespace WebApplication1.Endpoints;

public static class ReservasEndpoints
{

    const string GetReservaEndpointName = "GetReserva";


    public static RouteGroupBuilder MapReservas(this WebApplication app)
    {
        var group = app.MapGroup("reservas")
        .WithParameterValidation();

        //GET /games
        group.MapGet("/", async (ReservaStoreContext dbContext) =>
         await dbContext.Reservas
                    .Include(reserva => reserva.Tipo)
                    .Select(reserva => reserva.ToReservaSummaryDto())
                    .AsNoTracking()
                   .ToListAsync());

        group.MapGet("/livres", async (ReservaStoreContext dbContext) =>
        {
            var todosQuartos = Enumerable.Range(1, 30).ToList();

            var quartosOcupados = await dbContext.Reservas
            .Where(r => r.Entrada <= DateTime.Now && r.Saida >= DateTime.Now)
            .Select(r => r.NumeroQuarto)
            .Distinct()
            .ToListAsync();

            var quartosLivres = todosQuartos.Except(quartosOcupados).ToList();

            return Results.Ok(quartosLivres);
        }

        );
        //GET /games/1
        group.MapGet("/{id}", async (int id, ReservaStoreContext dbContext) =>
        {
            Reserva? reserva = await dbContext.Reservas.FindAsync(id);

            return reserva is null ? Results.NotFound() : Results.Ok(reserva.ToReservaDetailsDto());

        })
        .WithName(GetReservaEndpointName);

        //POST /reserva
        group.MapPost("/", async (CreateReservaDto newReserva, ReservaStoreContext dbContext) =>
    {
        Reserva reserva = newReserva.ToEntity();

        var quartosValidos = Enumerable.Range(1, 30).ToArray();
        if (!quartosValidos.Contains(reserva.NumeroQuarto))
        {
            return Results.BadRequest("Escolha um quarto de 1 a 30");
        }

        bool existConflit = await dbContext.Reservas.AnyAsync(r =>
        r.NumeroQuarto == reserva.NumeroQuarto &&
        reserva.Entrada < r.Saida &&
        reserva.Saida > r.Entrada
      );



        if (existConflit)
        {
            return Results.BadRequest("quarto ocupado");
        }

        var tiposValidos = new[] { 1, 2, 3 };

        if (!tiposValidos.Contains(reserva.TipoId))
        {
            return Results.BadRequest("Tipo de quarto inexistente. Os tipos válidos são: 1, 2 ou 3.");
        }



        dbContext.Reservas.Add(reserva);
        await dbContext.SaveChangesAsync();
        return Results.CreatedAtRoute(GetReservaEndpointName, new { id = reserva.Id }, reserva.ToReservaDetailsDto());

    });   //POST /reserva
        group.MapPost("/client", async (CreateReservaDto newReserva, ReservaStoreContext dbContext) =>
    {
        Reserva reserva = newReserva.ToEntity();

        var quartosValidos = Enumerable.Range(1, 30).ToArray();
        if (!quartosValidos.Contains(reserva.NumeroQuarto))
        {
            return Results.BadRequest("Escolha um quarto de 1 a 30");
        }

        bool existConflit = await dbContext.Reservas.AnyAsync(r =>
        r.NumeroQuarto == reserva.NumeroQuarto &&
        reserva.Entrada < r.Saida &&
        reserva.Saida > r.Entrada
      );



        if (existConflit)
        {
            return Results.BadRequest("quarto ocupado");
        }

        var tiposValidos = new[] { 1, 2, 3 };

        if (!tiposValidos.Contains(reserva.TipoId))
        {
            return Results.BadRequest("Tipo de quarto inexistente. Os tipos válidos são: 1, 2 ou 3.");
        }



        dbContext.Reservas.Add(reserva);
        await dbContext.SaveChangesAsync();
        return Results.CreatedAtRoute(GetReservaEndpointName, new { id = reserva.Id }, reserva.ToReservaDetailsDto());

    });


        group.MapPut("/{id}", async (int id, UpdateReservaDto updateReserva, ReservaStoreContext dbContext) =>
        {
            var existingReserva = await dbContext.Reservas.FindAsync(id);

            if (existingReserva is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingReserva)
                           .CurrentValues
                           .SetValues(updateReserva.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
        // DELETE /games/1

        group.MapDelete("/{id}", async (int id, ReservaStoreContext dbContext) =>
          {
              await dbContext.Reservas
                         .Where(game => game.Id == id)
                         .ExecuteDeleteAsync();

              return Results.NoContent();
          });

        return group;
    }

}



