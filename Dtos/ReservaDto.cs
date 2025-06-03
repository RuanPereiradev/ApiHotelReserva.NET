namespace WebApplication1.Dtos
{
    public record class ReservaDto(
        int Id,
        string NomeCliente,
        int NumeroQuarto,
        decimal Price,
        DateTime Entrada,
        DateTime Saida,
        int TipoId  
    );

}
