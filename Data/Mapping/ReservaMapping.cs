using System;
using WebApplication1.Dtos;
using WebApplication1.Entities;

namespace WebApplication1.Data.Mapping
{
    public static class ReservaMapping
{
        //Pega um objeto CreateReservaDto(os dados recebidos no POST).
        // Converte para um objeto Reserva, que representa a entidade do banco de dados.
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

        //Pega um UpdateReservaDto(os dados de atualização).
        //Converte para um objeto Reserva com o id já preenchido.
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
        // Converte uma entidade Reserva do banco de dados 
        // para um DTO resumido(ReservaSummaryDto),
        //  usado geralmente para listagens rápidas.
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

        //  Parecido com o anterior, 
        // mas converte para um DTO detalhado(ReservaDetailsDto).
        public static ReservaDetailsDto ToReservaDetailsDto(this Reserva reserva)
        {
            return new(
                  reserva.Id,
                reserva.NomeCliente,
                reserva.NumeroQuarto,
                reserva.TipoId,
                reserva.Price,
                reserva.Entrada,
                reserva.Saida,
                reserva.FinalPrice
            );
        }


    }
}
