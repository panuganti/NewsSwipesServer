using System;
using System.Web.Http;
using DataContracts.Client;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigController : Controller
    {        
        public ConfigController()
        { }

        [HttpGet]
        [Route("config/GetLabels/{lang}")]
        public Dictionary<string, string> GetLabels(string lang)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("config/GetStreams/{lang}")]
        public IEnumerable<Stream> GetStreams(string lang)
        {
            return _streams.Where(s => s.Lang.ToLower() == lang.ToLower());
        }

        [Route("config/GetAllStreams")]
        public IEnumerable<Stream> GetAllStreams(string lang)
        {
            return _streams;
        }

        [HttpGet]
        [Route("user/GetVersionInfo")]
        public VersionInfo GetVersionInfo()
        {
            return _versionInfo;
        }

        private VersionInfo _versionInfo = new VersionInfo {
            MinSupportedVersion = "0.0.9",
            LatestVersion = "0.0.9"
        };

        private List<Stream> _streams = new List<Stream>()
        {
            // English
            new Stream { Id = "1", Text = "Politics", Lang = "English", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Politics.jpeg"},
            new Stream { Id = "2", Text = "Entertainment", Lang = "English", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Entertainment.jpeg"},
            new Stream { Id = "3", Text = "Sports", Lang = "English", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Sports.jpeg"},
            // Hindi
            new Stream { Id = "4", Text = "Politics", Lang = "Hindi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Politics.jpeg"},
            new Stream { Id = "5", Text = "Entertainment", Lang = "Hindi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Entertainment.jpeg"},
            new Stream { Id = "6", Text = "Sports", Lang = "Hindi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Sports.jpeg"},
            // Telugu
            new Stream { Id = "7", Text = "Politics", Lang = "Telugu", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Politics.jpeg"},
            new Stream { Id = "8", Text = "Entertainment", Lang = "Telugu", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Entertainment.jpeg"},
            new Stream { Id = "9", Text = "Sports", Lang = "Telugu", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Sports.jpeg"},
            // Marathi
            new Stream { Id = "10", Text = "Politics", Lang = "Marathi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Politics.jpeg"},
            new Stream { Id = "11", Text = "Entertainment", Lang = "Marathi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Entertainment.jpeg"},
            new Stream { Id = "12", Text = "Sports", Lang = "Marathi", IsAdmin = true, UserSelected = true, backgroundImageUrl = "Sports.jpeg"},
        };

    }
}
