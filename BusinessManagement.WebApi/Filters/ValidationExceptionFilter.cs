using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace BusinessManagement.WebApi.Filters
{
    /// <summary>
    /// Filtro para capturar excepciones de validación y retornar un HTTP 400.
    /// </summary>
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException valEx)
            {
                var errors = valEx.Errors.Select(e => e.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Error de validación",
                    Errors = errors
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
