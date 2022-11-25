using Microsoft.AspNetCore.Mvc;

using benjaminshi.gs1;
using gcplength_saas_dotnet.Modal;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace gcplength_saas_dotnet.Controllers
{
    [Route("gcplength")]
    [Route("gcplen")]
    [Route("api/gcplength")]
    [Route("api/gcplen")]
    [ApiController]
    public class gcpLengthController : ControllerBase
    {
        // GET: api/<gcpLengthController>
        [HttpGet]
        public GCPResult Get()
        {
            GCPResult? result = null;

            string code = "";

            int gcplen;
            string prefix;

            var query = HttpContext.Request.Query;
            StringValues part;

            if (query.TryGetValue("Code", out part))
            {
                code = part.ToString();
            }
            else if (query.TryGetValue("code", out part))
            {
                code = part.ToString();
            }
            else if (query.TryGetValue("GCP", out part))
            {
                code = part.ToString();
            }
            else if (query.TryGetValue("gcp", out part))
            {
                code = part.ToString();
            }
            else if (query.TryGetValue("GTIN", out part))
            {
                code = part.ToString();
            }
            else if (query.TryGetValue("gtin", out part))
            {
                code = part.ToString();
            }

            if (Regex.IsMatch(@"^\d{14}$", code)) code = code.Substring(1, code.Length - 1);

            if (code.Length > 12) code = code.Substring(0, 12);

            gcplen = GCPLength.Find(code, out prefix);

            
            if (gcplen <= 0)
                result = new GCPResult(false, "Error", "Cannot detect GCP length with code \"" + code + "\"", null, null);
            else
                result = new GCPResult(true, "isOK", "", null, new GCPResultData(prefix, gcplen));

            return result;
        }

        // POST api/<gcpLengthController>
        [HttpPost]
        public GCPResult Post([FromBody] GCPInputVar value)
        {
            GCPResult? result = null;

            string code = "";

            int gcplen;
            string prefix;

            code = value.code;

            if (Regex.IsMatch(@"^\d{14}$", code)) code = code.Substring(1, code.Length - 1);

            if (code.Length > 12) code = code.Substring(0, 12);

            gcplen = GCPLength.Find(code, out prefix);


            if (gcplen <= 0)
                result = new GCPResult(false, "Error", "Cannot detect GCP length with code \"" + code + "\"", null, null);
            else
                result = new GCPResult(true, "isOK", "", null, new GCPResultData(prefix, gcplen));

            return result;
        }
    }
}
