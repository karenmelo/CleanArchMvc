using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {

            return Ok();
        }
    }
}
