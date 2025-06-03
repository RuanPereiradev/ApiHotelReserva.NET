namespace WebApplication1.Dtos;

public record class ReservaSummaryDto(
    int Id,
    string NomeCliente,
    int NumeroQuarto,
    string Tipo,
    decimal Price,
    DateTime Entrada, 
    DateTime Saida
);
