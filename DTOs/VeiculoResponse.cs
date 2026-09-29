using ConcessionariaApi.Models;

namespace ConcessionariaApi.DTOs;

public sealed record VeiculoResponse(
    int Id,
    string Marca,
    string Modelo,
    int Ano,
    decimal Preco,
    long Quilometragem,
    string Cor,
    string Combustivel,
    bool Disponivel,
    DateTime CriadoEmUtc)
{
    public static VeiculoResponse FromEntity(Veiculo veiculo) =>
        new(
            veiculo.Id,
            veiculo.Marca,
            veiculo.Modelo,
            veiculo.Ano,
            veiculo.Preco,
            veiculo.Quilometragem,
            veiculo.Cor,
            veiculo.Combustivel,
            veiculo.Disponivel,
            veiculo.CriadoEmUtc);
}
