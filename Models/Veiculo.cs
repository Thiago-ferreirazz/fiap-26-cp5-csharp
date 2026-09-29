namespace ConcessionariaApi.Models;

public sealed class Veiculo
{
    public int Id { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public int Ano { get; set; }
    public decimal Preco { get; set; }
    public long Quilometragem { get; set; }
    public required string Cor { get; set; }
    public required string Combustivel { get; set; }
    public bool Disponivel { get; set; }
    public DateTime CriadoEmUtc { get; set; }
}
