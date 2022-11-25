using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace gcplength_saas_dotnet.Controllers
{
    public class CopyrightMessage
    {
        public string copyright { get; set; } = @"Copyright (c) 2022 Yu Shi (Benjamin).";
        public string contact { get; set; } = @"Yu Shi (Benjamin Shi) shiyubnu@gmail.com";

        public string repository { get; set; } = @"https://github.com/benjamin-shi/gcplength-saas-dotnet";
    }

    [Route("api/copyright")]
    [Route("api/copy")]
    [ApiController]
    public class CopyrightController : ControllerBase
    {
        protected string COPYRIGHT = @"";

        // GET: api/<CopyrightController>
        [HttpGet]
        public CopyrightMessage Get()
        {
            return new CopyrightMessage();
        }

        // POST api/<CopyrightController>
        [HttpPost]
        public CopyrightMessage Post()
        {
            return new CopyrightMessage();
        }
    }
}
