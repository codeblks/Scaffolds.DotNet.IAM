using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
