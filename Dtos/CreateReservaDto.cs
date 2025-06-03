using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public record class CreateReservaDto(
       [Required] [StringLength(50)] string NomeCliente,
       [Required] int NumeroQuarto,
       int TipoId,
       [Required] DateTime Entrada,
       [Required] DateTime Saida);
    
}
