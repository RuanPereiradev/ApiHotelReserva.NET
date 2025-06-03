using System;
using WebApplication1.Dtos;
using WebApplication1.Entities;

namespace WebApplication1.Data.Mapping;

public static class TipoMapping
{
    public static TipoDto ToDto(this Tipo tipo)
    {
        return new TipoDto(tipo.Id, tipo.Name, tipo.Price);
    }
}