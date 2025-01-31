using System.Net;
using System.Text.Json;
using UsuarioAPI.Application.DTOs.Base;
using UsuarioAPI.Domain.Exceptions;

namespace UsuarioAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            RetornoErroDTO retornoErroDTO = new RetornoErroDTO();

            switch (ex)
            {
                case UserBaseExceptions e: 
                    status = HttpStatusCode.BadRequest;
                    retornoErroDTO.mensagem = "Ocorreu um erro";
                    retornoErroDTO.status = (int)status;
                    retornoErroDTO.erros = e.Erros;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    retornoErroDTO.mensagem = "Erro interno no servidor";
                    retornoErroDTO.status = (int)status;
                    retornoErroDTO.erros.Add("Ocorreu um erro inesperado, tente novamente mais tarde");
                    break;
            }

            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(retornoErroDTO));
        }
    }
}
