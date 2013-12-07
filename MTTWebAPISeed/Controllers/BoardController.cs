using System.Collections.Generic;
using System.Web.Http;

namespace MTTWebAPI.WebUI.Controllers
{
    public class BoardController : ApiController
    {
        [HttpPost]
        public IEnumerable<string> Retrieve()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public void Remove()
        {
        }
    }
}
