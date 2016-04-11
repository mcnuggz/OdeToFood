using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    [Route("[controller]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "+1-555-555-5551";
        }
        [Route("[action]")]
        public string Country()
        {
            return "USA";
        }
    }
}
