using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Domain.Abastacao;

namespace Pedidos.Api.Controller
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Consumes("application/json", [])]
    [Produces("application/json", [])]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        protected BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected virtual ActionResult ValidaException(Exception ex)
        {
            switch (ex)
            {
                case NotFoundException notFound:
                    return NotFound(notFound.Message);

                case RegraNegocioException regra:
                    return BadRequest(regra.Message);

                case ValidationException validation:
                    return BadRequest(validation.Message);

                case UnauthorizedAccessException unauthorized:
                    return Unauthorized(unauthorized.Message);

                default:
                    _logger.LogError(ex, "Erro interno ao processar a solicitação.");
                    return StatusCode(500, "Erro interno ao processar a solicitação");
            }
        }
    }
}
