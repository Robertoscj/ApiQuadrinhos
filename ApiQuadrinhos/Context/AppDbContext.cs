using ApiQuadrinhos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiQuadrinhos.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Quadrinho> Quadrinhos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
     
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<Categoria>().HasKey(t => t.Id);
        builder.Entity<Categoria>().
            Property(p => p.Nome).HasMaxLength(100).IsRequired();

        // 1 : N => Categoria : Quadrinhos
        builder.Entity<Categoria>().HasMany(c => c.Quadrinhos)
            .WithOne(b => b.Categoria)
            .HasForeignKey(b => b.CategoriaId);

        
        builder.Entity<Categoria>().HasData(
          new Categoria(1, "Aventura"),
          new Categoria(2, "Ação"),
          new Categoria(3, "Drama"),
          new Categoria(4, "Romance"),
          new Categoria(5, "Ficção")
        );

        builder.Entity<Quadrinho>().HasKey(t => t.Id);

        //configura o tamanho máximo das propriedades que irão gerar colunas com tamanho correspondentes 
        builder.Entity<Quadrinho>().Property(p => p.Titulo).HasMaxLength(100).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Descricao).HasMaxLength(200).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Autor).HasMaxLength(200).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Editora).HasMaxLength(100).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Formato).HasMaxLength(100).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Cor).HasMaxLength(50).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Origem).HasMaxLength(100).IsRequired();
        builder.Entity<Quadrinho>().Property(p => p.Imagem).HasMaxLength(250).IsRequired();

        builder.Entity<Quadrinho>().Property(p => p.Preco).HasPrecision(10, 2);

        
        foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

    }
}
