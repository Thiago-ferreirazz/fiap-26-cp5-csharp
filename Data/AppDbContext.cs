using ConcessionariaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApi.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var veiculo = modelBuilder.Entity<Veiculo>();

        veiculo.ToTable("Veiculos");
        veiculo.HasKey(item => item.Id);
        veiculo.Property(item => item.Marca).HasMaxLength(50).IsRequired();
        veiculo.Property(item => item.Modelo).HasMaxLength(80).IsRequired();
        veiculo.Property(item => item.Preco).HasPrecision(10, 2);
        veiculo.Property(item => item.Cor).HasMaxLength(30).IsRequired();
        veiculo.Property(item => item.Combustivel).HasMaxLength(30).IsRequired();
        // SQLite nao armazena o DateTimeKind; sem isso a data volta como Unspecified (sem "Z" no JSON).
        veiculo.Property(item => item.CriadoEmUtc)
            .IsRequired()
            .HasConversion(value => value, value => DateTime.SpecifyKind(value, DateTimeKind.Utc));
        veiculo.HasIndex(item => new { item.Marca, item.Modelo });
        veiculo.HasIndex(item => item.Disponivel);

        veiculo.HasData(
            new Veiculo
            {
                Id = 1,
                Marca = "Toyota",
                Modelo = "Corolla Altis",
                Ano = 2024,
                Preco = 158_900.00m,
                Quilometragem = 12_500,
                Cor = "Prata",
                Combustivel = "Flex",
                Disponivel = true,
                CriadoEmUtc = new DateTime(2026, 9, 29, 12, 0, 0, DateTimeKind.Utc)
            },
            new Veiculo
            {
                Id = 2,
                Marca = "Volkswagen",
                Modelo = "T-Cross Highline",
                Ano = 2025,
                Preco = 172_490.00m,
                Quilometragem = 8_200,
                Cor = "Branco",
                Combustivel = "Flex",
                Disponivel = true,
                CriadoEmUtc = new DateTime(2026, 9, 29, 12, 0, 0, DateTimeKind.Utc)
            },
            new Veiculo
            {
                Id = 3,
                Marca = "Chevrolet",
                Modelo = "Onix Premier",
                Ano = 2023,
                Preco = 98_900.00m,
                Quilometragem = 31_000,
                Cor = "Azul",
                Combustivel = "Flex",
                Disponivel = false,
                CriadoEmUtc = new DateTime(2026, 9, 29, 12, 0, 0, DateTimeKind.Utc)
            });
    }
}
