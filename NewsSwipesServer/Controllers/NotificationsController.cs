using System.Web.Http;
using System.Web.Http.Cors;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NotificationsController : Controller
    {

        public NotificationsController()
        { }


        [HttpGet]
        [Route("notifications/GetNotifications/{userId}")]
        public string GetNotifications(string userId)
        {
            return "hello world";
        }

    }
}
