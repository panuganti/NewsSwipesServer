using System.Web.Http;
using DataContracts.Client;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;
using NewsSwipesLibrary;

namespace NewsSwipesServer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigController : Controller
    {
        Config _config;

        public ConfigController(): this(new Config())
        { }

        private ConfigController(Config config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("config/GetLabels/{lang}")]
        public Dictionary<string, string> GetLabels(string lang)
        {
            return _config.GetLabels(lang);
        }

        [HttpGet]
        [Route("config/GetStreams/{lang}")]
        public IEnumerable<Stream> GetStreams(string lang)
        {
            return _config.AllStreams.Where(t => t.Lang.ToLower() == lang);
        }

        [HttpGet]
        [Route("config/GetAllStreams")]
        public IEnumerable<Stream> GetAllStreams()
        {
            return _config.AllStreams;
        }

        [HttpGet]
        [Route("config/GetVersionInfo")]
        public VersionInfo GetVersionInfo()
        {
            return _versionInfo;
        }

        private VersionInfo _versionInfo = new VersionInfo {
            MinSupportedVersion = "0.0.9",
            LatestVersion = "0.0.9"
        };

    }
}
