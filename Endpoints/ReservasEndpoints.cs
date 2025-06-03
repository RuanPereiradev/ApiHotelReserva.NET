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

            //GET /games/1
            group.MapGet("/{id}", async  (int id,ReservaStoreContext dbContext) =>
            {
                Reserva? reserva = await dbContext.Reservas.FindAsync(id);

                return reserva is null ? Results.NotFound() : Results.Ok(reserva.ToReservaDetailsDto());

            })
            .WithName(GetReservaEndpointName);

        //POST /reserva
        group.MapPost("/",  async (CreateReservaDto newReserva, ReservaStoreContext dbContext) =>
  {
      Reserva reserva =  newReserva.ToEntity();

      dbContext.Reservas.Add(reserva);
      await dbContext.SaveChangesAsync();
      return Results.CreatedAtRoute(GetReservaEndpointName, new{ id = reserva.Id}, reserva.ToReservaDetailsDto());


    
      });

    
            group.MapPut("/{id}", async(int id, UpdateReservaDto updateReserva, ReservaStoreContext dbContext) =>
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



