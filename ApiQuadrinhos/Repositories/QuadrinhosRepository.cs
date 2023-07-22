using ApiQuadrinhos.Context;
using ApiQuadrinhos.Entities;
using ApiQuadrinhos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiQuadrinhos.Repositories;

public class QuadrinhosRepository : Repository<Quadrinho>, IQuadrinhosRepository
{
    public QuadrinhosRepository(AppDbContext context) : base(context) { }
    public async Task<IEnumerable<Quadrinho>> GetQuadrinhosPorCategoriaAsync(int categoriaId)
    {
        return await _db.Quadrinhos.Where(b => b.CategoriaId == categoriaId).ToListAsync();
    }
    public async Task<IEnumerable<Quadrinho>> LocalizaQuadrinhoComCategoriaAsync(string criterio)
    {
        return await _db.Quadrinhos.AsNoTracking()
            .Include(b => b.Categoria)
            .Where(b => b.Titulo.Contains(criterio) ||
                        b.Autor.Contains(criterio) ||
                        b.Descricao.Contains(criterio) ||
                        b.Categoria.Nome.Contains(criterio))
            .ToListAsync();
    }
}