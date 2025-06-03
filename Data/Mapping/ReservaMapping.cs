using System;
using WebApplication1.Dtos;
using WebApplication1.Entities;

namespace WebApplication1.Data.Mapping
{
    public static class ReservaMapping
    {
        public static Reserva ToEntity(this CreateReservaDto reserva)
        {
            return new Reserva()
            {
                NomeCliente = reserva.NomeCliente,
                NumeroQuarto = reserva.NumeroQuarto,
                TipoId = reserva.TipoId,
                Entrada = reserva.Entrada,
                Saida = reserva.Saida,
            };
        }

        public static Reserva ToEntity(this UpdateReservaDto reserva, int id)
        {
            return new Reserva()
            {
                Id = id,
                NomeCliente = reserva.NomeCliente,
                NumeroQuarto = reserva.NumeroQuarto,
                Price = reserva.Price,
                Entrada = reserva.Entrada,
                Saida = reserva.Saida,
            };
        }

        public static ReservaSummaryDto ToReservaSummaryDto(this Reserva reserva)
        {
            return new(
                reserva.Id,
                reserva.NomeCliente,
                reserva.NumeroQuarto,
                reserva.Tipo!.Name,
                reserva.Tipo.Price,
                reserva.Entrada,
                reserva.Saida
               
            );
        }

        public static ReservaDetailsDto ToReservaDetailsDto(this Reserva reserva)
        {
            return new(
                  reserva.Id,
                reserva.NomeCliente,
                reserva.NumeroQuarto,
                reserva.TipoId,
                reserva.Price,
                reserva.Entrada,
                reserva.Saida
            );
        }


    }
}
