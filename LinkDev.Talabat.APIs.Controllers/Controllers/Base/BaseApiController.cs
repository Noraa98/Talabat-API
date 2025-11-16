using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }

}

// project flow : HTTP Request → BaseApiController → ProductsController → ServiceManager → ProductService → UnitOfWork → Repository → DbContext → Database → DTOs → Response
