using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos
{
    public record class CreateReservaDto(
       [Required] [StringLength(50)] string NomeCliente,
       [Range(1,30, ErrorMessage = "Quartos apenas de 1 a 30")]
       [Required] int NumeroQuarto,
       [Range(1, 3, ErrorMessage = "Tipo de quarto inexistente. Os tipos válidos são: 1, 2 ou 3")]
       int TipoId,
       [Required] DateTime Entrada,
       [Required] DateTime Saida);
    
}
