using System.ComponentModel.DataAnnotations;

namespace ConcessionariaApi.DTOs;

// Os textos sao normalizados com Trim na desserializacao, antes da validacao das DataAnnotations.
public sealed class VeiculoRequest
{
    [Required(ErrorMessage = "A marca e obrigatoria.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "A marca deve ter entre 2 e 50 caracteres.")]
    public string Marca { get; init => field = value?.Trim()!; } = string.Empty;

    [Required(ErrorMessage = "O modelo e obrigatorio.")]
    [StringLength(80, MinimumLength = 1, ErrorMessage = "O modelo deve ter entre 1 e 80 caracteres.")]
    public string Modelo { get; init => field = value?.Trim()!; } = string.Empty;

    [Range(1900, 2100, ErrorMessage = "O ano deve estar entre 1900 e 2100.")]
    public int Ano { get; init; }

    [Range(typeof(decimal), "0.01", "99999999.99", ErrorMessage = "O preco deve estar entre 0,01 e 99.999.999,99.")]
    public decimal Preco { get; init; }

    [Range(0, 2_000_000, ErrorMessage = "A quilometragem deve estar entre 0 e 2.000.000.")]
    public long Quilometragem { get; init; }

    [Required(ErrorMessage = "A cor e obrigatoria.")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "A cor deve ter entre 2 e 30 caracteres.")]
    public string Cor { get; init => field = value?.Trim()!; } = string.Empty;

    [Required(ErrorMessage = "O combustivel e obrigatorio.")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "O combustivel deve ter entre 2 e 30 caracteres.")]
    public string Combustivel { get; init => field = value?.Trim()!; } = string.Empty;

    public bool Disponivel { get; init; }
}
