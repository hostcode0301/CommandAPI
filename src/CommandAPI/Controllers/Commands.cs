using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Commands : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return new string[] { "this", "is", "hard", "coded" };
        }
    }
}