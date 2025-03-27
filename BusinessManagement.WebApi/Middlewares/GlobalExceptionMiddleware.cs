using BusinessManagement.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessManagement.WebApi.Middlewares
{
    /// <summary>
    /// Middleware para capturar cualquier excepción no manejada y retornar un error HTTP 500.
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Error de dominio");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = dex.Message });
            }
            catch (ValidationException vex)
            {
                _logger.LogWarning(vex, "Error de validación");
                context.Response.StatusCode = 400;
                var errors = vex.Errors.Select(e => e.ErrorMessage).ToList();
                await context.Response.WriteAsJsonAsync(new { Message = "Error de validación", Errors = errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno del servidor");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { Message = "Error interno del servidor" });
            }
        }
    }

}
