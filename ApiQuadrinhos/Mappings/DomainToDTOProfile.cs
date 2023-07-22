using AutoMapper;
using ApiQuadrinhos.DTOs;
using ApiQuadrinhos.Entities;

namespace ApiQuadrinhos.Mappings;

public class DomainToDTOProfile : Profile
{
    public DomainToDTOProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Quadrinho, QuadrinhosDTO>().ReverseMap();

        // cria um mapeamento entre a classe Manga e a classe MangaCategoriaDTO.
        // O mapeamento especifica que a propriedade NomeCategoria do DTO será
        // mapeada a partir da propriedade Nome da propriedade Categoria do objeto Manga.
        CreateMap<Quadrinho, QuadrinhosCategoriaDTO>()
            .ForMember(dto => dto.NomeCategoria, opt=> opt.MapFrom(src=> src.Categoria.Nome));
    }
}
