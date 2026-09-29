using ConcessionariaApi.Data;
using ConcessionariaApi.DTOs;
using ConcessionariaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApi.Controllers;

[ApiController]
[Route("api/v1/veiculos")]
[Produces("application/json")]
public sealed class VeiculosController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<VeiculoResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeiculoResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var veiculos = await dbContext.Veiculos
            .AsNoTracking()
            .OrderBy(item => item.Id)
            .Select(item => new VeiculoResponse(
                item.Id,
                item.Marca,
                item.Modelo,
                item.Ano,
                item.Preco,
                item.Quilometragem,
                item.Cor,
                item.Combustivel,
                item.Disponivel,
                item.CriadoEmUtc))
            .ToListAsync(cancellationToken);

        return Ok(veiculos);
    }

    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType<VeiculoResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculoResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var veiculo = await dbContext.Veiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        return veiculo is null
            ? VeiculoNaoEncontrado(id)
            : Ok(VeiculoResponse.FromEntity(veiculo));
    }

    [HttpPost]
    [ProducesResponseType<VeiculoResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeiculoResponse>> Create(
        [FromBody] VeiculoRequest request,
        CancellationToken cancellationToken)
    {
        var veiculo = new Veiculo
        {
            Marca = request.Marca.Trim(),
            Modelo = request.Modelo.Trim(),
            Ano = request.Ano,
            Preco = request.Preco,
            Quilometragem = request.Quilometragem,
            Cor = request.Cor.Trim(),
            Combustivel = request.Combustivel.Trim(),
            Disponivel = request.Disponivel,
            CriadoEmUtc = DateTime.UtcNow
        };

        dbContext.Veiculos.Add(veiculo);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = VeiculoResponse.FromEntity(veiculo);
        return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, response);
    }

    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] VeiculoRequest request,
        CancellationToken cancellationToken)
    {
        var veiculo = await dbContext.Veiculos
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (veiculo is null)
        {
            return VeiculoNaoEncontrado(id);
        }

        veiculo.Marca = request.Marca.Trim();
        veiculo.Modelo = request.Modelo.Trim();
        veiculo.Ano = request.Ano;
        veiculo.Preco = request.Preco;
        veiculo.Quilometragem = request.Quilometragem;
        veiculo.Cor = request.Cor.Trim();
        veiculo.Combustivel = request.Combustivel.Trim();
        veiculo.Disponivel = request.Disponivel;

        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var veiculo = await dbContext.Veiculos
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (veiculo is null)
        {
            return VeiculoNaoEncontrado(id);
        }

        dbContext.Veiculos.Remove(veiculo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    private ObjectResult VeiculoNaoEncontrado(int id) =>
        Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Veiculo nao encontrado",
            detail: $"Nao existe veiculo cadastrado com o ID {id}.");
}
