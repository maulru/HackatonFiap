using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace AgendaAPI.Infrastructure.Security
{
    public class AuthorizePacienteAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userTypeClaim = user.Claims.FirstOrDefault(c => c.Type == "tipo")?.Value;
            if (string.IsNullOrEmpty(userTypeClaim) || userTypeClaim != "Paciente")
            {
                context.Result = new ForbidResult();
                return;
            }

            var idPacienteClaim = user.Claims.FirstOrDefault(c => c.Type == "IdPaciente")?.Value;
            if (string.IsNullOrEmpty(idPacienteClaim))
            {
                context.Result = new ForbidResult();
                return;
            }

            context.HttpContext.Items["IdPaciente"] = idPacienteClaim;
        }
    }
}
