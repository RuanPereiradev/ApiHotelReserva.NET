using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Data.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;



namespace WebApplication1.Endpoints;

public static class ReservasEndpoints
{

    const string GetReservaEndpointName = "GetReserva";


    public static RouteGroupBuilder MapReservas(this WebApplication app)
    {
        var group = app.MapGroup("reservas")
        .WithParameterValidation();

        //GET /games
        group.MapGet("/", async (AppDbContext dbContext) =>
         await dbContext.Reservas
                    .Include(reserva => reserva.Tipo)
                    .Select(reserva => reserva.ToReservaSummaryDto())
                    .AsNoTracking()
                   .ToListAsync()).RequireAuthorization();

        group.MapGet("/livres", async (AppDbContext dbContext) =>
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
        group.MapGet("/{id}", async (int id, AppDbContext dbContext) =>
        {
            Reserva? reserva = await dbContext.Reservas.FindAsync(id);

            return reserva is null ? Results.NotFound() : Results.Ok(reserva.ToReservaDetailsDto());

        })
        .WithName(GetReservaEndpointName).RequireAuthorization();

        //POST /reserva
        group.MapPost("/", async (CreateReservaDto newReserva, AppDbContext dbContext) =>
 {
     var validationContext = new ValidationContext(newReserva);
     var validationResults = new List<ValidationResult>();
     bool isValid = Validator.TryValidateObject(newReserva, validationContext, validationResults, true);

     if (!isValid)
     {
         return Results.BadRequest(validationResults);
     }

     if (newReserva.Entrada >= newReserva.Saida)
     {
         return Results.BadRequest("A data de entrada deve ser anterior a data de saida");
     }

     Reserva reserva = newReserva.ToEntity();

     var quartosValidos = Enumerable.Range(1, 30).ToArray();
     if (!quartosValidos.Contains(reserva.NumeroQuarto))
     {
         return Results.BadRequest("Quarto de 1 a 30");
     }

     bool existConflit = await dbContext.Reservas.AnyAsync(r =>
         r.NumeroQuarto == reserva.NumeroQuarto &&
         reserva.Entrada < r.Saida &&
         reserva.Saida > r.Entrada
         );

     if (existConflit)
     {
         return Results.BadRequest("Quartp ocupado para o periodo informado");
     }

     var tiposValidos = new[] { 1, 2, 3 };
     if (!tiposValidos.Contains(reserva.TipoId))
     {
         return Results.BadRequest("Tipo de quarto inexistente.");
     }

     dbContext.Reservas.Add(reserva);
     await dbContext.SaveChangesAsync();

     return Results.CreatedAtRoute("GetReserva", new { id = reserva.Id }, reserva.ToReservaDetailsDto());
 
 }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        //POST /reserva
        group.MapPost("/client", async (CreateReservaDto newReserva, AppDbContext dbContext) =>
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


        group.MapPut("/{id}", async (int id, UpdateReservaDto updateReserva, AppDbContext dbContext) =>
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

        group.MapDelete("/{id}", async (int id, AppDbContext dbContext) =>
          {
              await dbContext.Reservas
                         .Where(game => game.Id == id)
                         .ExecuteDeleteAsync();

              return Results.NoContent();
          });

        return group;
    }

}



