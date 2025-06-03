using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Mapping;

namespace WebApplication1.Endpoints;

public static class TiposEdpoints
{
    public static RouteGroupBuilder MapTiposEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");
        group.MapGet("/", async (ReservaStoreContext dbContext) =>
            await dbContext.Tipos
                            .Select(tipo => tipo.ToDto())
                            .AsNoTracking()
                           .ToListAsync());

        return group;
    }
}