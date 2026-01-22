using AutoMapper;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.Domain.Entidades;
using Pedidos.Domain.Enum;

namespace Pedidos.App.AutoMapper
{
    public class MapperRequisicaoParaDomain : Profile
    {
        public MapperRequisicaoParaDomain()
        {
            CreateMap<PedidoRequisicao, Pedido>(MemberList.Destination)
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.ClienteNome))
              .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
              .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => PedidoStatus.Novo))
              .ForMember(dest => dest.ValorTotal, opt => opt.Ignore())
              .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens))
              .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<ItemPedidoRequisicao, ItemPedido>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PedidoId, opt => opt.Ignore())
                .ForMember(dest => dest.ProdutoNome, opt => opt.MapFrom(src => src.ProdutoNome))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.PrecoUnitario))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<UsuarioRequisicao, Usuario>(MemberList.Destination)
            .IgnoreAllPropertiesWithAnInaccessibleSetter();

        

        }

    }
}
