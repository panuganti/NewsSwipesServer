using DataContracts.Client;
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
        public UserNotification GetNotifications(string userId)
        {
            // TODO: 
            return new UserNotification() { Badge = 0, Notifications = new Notification[] { } };
        }

        [HttpGet]
        [Route("notifications/ClearAllNotifications/{userId}")]
        public bool ClearAllNotifications(string userId)
        {
            // TODO: 
            return true;
        }
    }
}
