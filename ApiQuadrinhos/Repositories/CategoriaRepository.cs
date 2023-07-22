using ApiQuadrinhos.Context;
using ApiQuadrinhos.Entities;
using ApiQuadrinhos.Repositories.Interfaces;

namespace ApiQuadrinhos.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }
}
