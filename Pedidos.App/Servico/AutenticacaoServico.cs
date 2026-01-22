using AutoMapper;
using FluentValidation;
using Pedidos.App.DTO.DtoRequisicao;
using Pedidos.App.DTO.DtoRespostas;
using Pedidos.App.Interface;
using Pedidos.Infra.Interface;
using System.Text.RegularExpressions;

namespace Pedidos.App.Servico
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ITokenAcessoServico _jwtHandlerServico;
        private readonly IValidator<LoginRequisicao> _validatorLogin;
        private readonly string _mensagemPadraoLoginInvalido = "Email e/ou senha inválidos";

        public AutenticacaoServico(
            IMapper mapper,
            IUsuarioRepositorio repositorio,
            ITokenAcessoServico jwtHandlerServico,
            IValidator<LoginRequisicao> validator)
        {
            _mapper = mapper;
            _repositorio = repositorio;
            _validatorLogin = validator;
            _jwtHandlerServico = jwtHandlerServico;
        }

        public async Task<LoginResposta> LoginAsync(LoginRequisicao loginRequisicao)
        {
            var validationResult = _validatorLogin.Validate(loginRequisicao);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.ToString());

            if (!EmailValido(loginRequisicao.Email))
                throw new UnauthorizedAccessException(_mensagemPadraoLoginInvalido);


            var usuario = await _repositorio.ObterUsuarioPorEmailAsync(loginRequisicao.Email)
                                ?? throw new UnauthorizedAccessException(_mensagemPadraoLoginInvalido);

            if (!BC.Verify(loginRequisicao.Senha, usuario.Senha))
                throw new UnauthorizedAccessException(_mensagemPadraoLoginInvalido);

            var token = await _jwtHandlerServico.GerarTokenAcessoAsync(usuario);

            return _mapper.Map<LoginResposta>(token);
        }

        private bool EmailValido(string email)
        {
            string emailPattern = @"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b$";
            return Regex.IsMatch(email!, emailPattern);
        }
 
    }
}
