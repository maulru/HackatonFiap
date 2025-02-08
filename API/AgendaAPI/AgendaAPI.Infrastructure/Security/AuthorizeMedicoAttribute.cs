using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgendaAPI.Infrastructure.Security
{
    public class AuthorizeMedicoAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Obtendo o tipo de usuário do Token
            var userTypeClaim = user.Claims.FirstOrDefault(c => c.Type == "tipo")?.Value;

            if (string.IsNullOrEmpty(userTypeClaim) || userTypeClaim != "Medico")
            {
                context.Result = new ForbidResult();
                return;
            }

            var idMedicoClaim = user.Claims.FirstOrDefault(c => c.Type == "IdMedico")?.Value;
            if (string.IsNullOrEmpty(idMedicoClaim))
            {
                context.Result = new ForbidResult();
                return;
            }

            context.HttpContext.Items["IdMedico"] = idMedicoClaim;
        }
    }
}
