using AutoMapper;
using Pedidos.App.DTO.DtoModelo;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.Domain.Entidades;

namespace Pedidos.App.AutoMapper
{
    public class MapperDomainParaResposta : Profile
    {
        public MapperDomainParaResposta()
        {
            CreateMap<Pedido, PedidoResposta>(MemberList.Destination)
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.ClienteNome))
              .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.DataCriacao))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
              .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ValorTotal))
              .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens))
              .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<ItemPedido, ItemPedidoResposta>(MemberList.Destination)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProdutoNome, opt => opt.MapFrom(src => src.ProdutoNome))
                .ForMember(dest => dest.Quantidade, opt => opt.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.PrecoUnitario))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();


            CreateMap<Usuario, UsuarioResposta>(MemberList.Destination);


 
            CreateMap<TokenAcesso, LoginResposta>(MemberList.Destination);
        }

    }
}
