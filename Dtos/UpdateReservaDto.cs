using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public record class UpdateReservaDto(
        [Required][StringLength(50)] string NomeCliente,
       [Required] int NumeroQuarto,
       [Required] Decimal Price,
       [Required] DateTime Entrada,
       [Required] DateTime Saida
    );
}
