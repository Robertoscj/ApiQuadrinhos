using ApiQuadrinhos.Repositories.Interfaces;
using ApiQuadrinhos.Entities;

namespace ApiQuadrinhos.Repositories.Interfaces;

public interface IQuadrinhosRepository : IRepository<Quadrinho>
{
    Task<IEnumerable<Quadrinho>>
        GetQuadrinhosPorCategoriaAsync(int categoriaId);

    Task<IEnumerable<Quadrinho>>
        LocalizaQuadrinhoComCategoriaAsync(string criterio);
}
