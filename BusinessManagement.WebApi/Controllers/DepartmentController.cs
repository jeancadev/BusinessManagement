using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        // Solo usuarios con Claim "Department" = "IT" pueden acceder
        [Authorize(Policy = "ITDepartmentOnly")]
        [HttpGet("it-secret")]
        public IActionResult ItSecret()
        {
            return Ok("Solo personal de IT puede acceder a este recurso.");
        }
    }
}
