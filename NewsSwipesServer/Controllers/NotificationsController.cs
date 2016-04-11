using DataContracts.Client;
using System;
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
            try
            {
                // TODO: 
                return new UserNotification() { Badge = 0, Notifications = new Notification[] { } };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("notifications/ClearAllNotifications/{userId}")]
        public bool ClearAllNotifications(string userId)
        {
            try
            {
                // TODO: 
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
