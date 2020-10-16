using System.Web.Http;

namespace data_handler_app.Controllers
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "Running!";
        }
    }
}
