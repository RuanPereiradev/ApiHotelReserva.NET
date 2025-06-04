namespace WebApplication1.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public required string NomeCliente { get; set; }
        public required int NumeroQuarto { get; set; }
        public int TipoId { get; set; }
        public Tipo? Tipo { get; set; }
        public decimal Price { get; set; }
        public required DateTime Entrada { get; set; }
        public required DateTime Saida { get; set; }
        public decimal FinalPrice { get; set; }
    }

}
