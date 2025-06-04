namespace WebApplication1.Dtos;

public record class ReservaDetailsDto(
   int Id,
    string NomeCliente,
    int NumeroQuarto,
    int TipoId,
    decimal Price,
    DateTime Entrada,
    DateTime Saida,
    decimal FinalPrice
    );