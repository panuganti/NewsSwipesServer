using DataContracts.Client;
using System.Collections.Generic;

namespace NewsSwipesLibrary
{
    public class Config
    {
        private Dictionary<string, Dictionary<string, string>> _langToLabels 
                        = new Dictionary<string, Dictionary<string, string>>();

        //private Dictionary<string, List<string>> _streams = new Dictionary<string, List<string>>();

        public Config()
        {
            BuildEnglishLabelDictionary();
            BuildHindiLabelDictionary();
            BuildMarathiLabelDictionary();
            BuildTeluguLabelDictionary();
            //BuildStreams();
        }

        #region Streams
        /*
        public List<string> GetStreams(string lang)
        {
            return _streams[lang];
        }
        */
        public List<Stream> AllStreams { get { return streams; } }

        private List<Stream> streams = new List<Stream>()
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

        /*
        private void BuildStreams()
        {
            _streams.Add("english", new List<string>());
            _streams.Add("hindi", new List<string>());
            _streams.Add("marathi", new List<string>());
            _streams.Add("telugu", new List<string>());

            _streams["english"].Add("english_politics");
            _streams["english"].Add("english_entertainment");
            _streams["english"].Add("english_sports");
            _streams["english"].Add("english_international");
            _streams["english"].Add("english_food");
            _streams["english"].Add("english_fashion");
            _streams["english"].Add("english_health");
            _streams["english"].Add("english_economy");
            _streams["english"].Add("english_other");

            _streams["hindi"].Add("hindi_politics");
            _streams["hindi"].Add("hindi_entertainment");
            _streams["hindi"].Add("hindi_sports");
            _streams["hindi"].Add("hindi_international");
            _streams["hindi"].Add("hindi_food");
            _streams["hindi"].Add("hindi_fashion");
            _streams["hindi"].Add("hindi_health");
            _streams["hindi"].Add("hindi_economy");
            _streams["hindi"].Add("hindi_other");

            _streams["marathi"].Add("marathi_politics");
            _streams["marathi"].Add("marathi_entertainment");
            _streams["marathi"].Add("marathi_sports");
            _streams["marathi"].Add("marathi_international");
            _streams["marathi"].Add("marathi_food");
            _streams["marathi"].Add("marathi_fashion");
            _streams["marathi"].Add("marathi_health");
            _streams["marathi"].Add("marathi_economy");
            _streams["marathi"].Add("marathi_other");

            _streams["telugu"].Add("telugu_politics");
            _streams["telugu"].Add("telugu_entertainment");
            _streams["telugu"].Add("telugu_sports");
            _streams["telugu"].Add("telugu_international");
            _streams["telugu"].Add("telugu_food");
            _streams["telugu"].Add("telugu_fashion");
            _streams["telugu"].Add("telugu_health");
            _streams["telugu"].Add("telugu_economy");
            _streams["telugu"].Add("telugu_other");
        }
        */
        #endregion Streams

        #region Labels
        public Dictionary<string, string> GetLabels(string lang)
        {
            return _langToLabels[lang.ToLower()];
        }

        private void BuildEnglishLabelDictionary()
        {
            Dictionary<string, string> labels = new Dictionary<string, string>();
            labels.Add("Email", "Email");
            labels.Add("Password", "Password");
            labels.Add("Terms", "Terms");
            _langToLabels.Add("english",labels);
        }

        private void BuildHindiLabelDictionary()
        {
            Dictionary<string, string> labels = new Dictionary<string, string>();
            labels.Add("Email", "Email");
            labels.Add("Password", "Password");
            labels.Add("Terms", "Terms");
            _langToLabels.Add("hindi", labels);
        }

        private void BuildMarathiLabelDictionary()
        {
            Dictionary<string, string> labels = new Dictionary<string, string>();
            labels.Add("Email", "Email");
            labels.Add("Password", "Password");
            labels.Add("Terms", "Terms");
            _langToLabels.Add("marathi", labels);
        }

        private void BuildTeluguLabelDictionary()
        {
            Dictionary<string, string> labels = new Dictionary<string, string>();
            labels.Add("Email", "Email");
            labels.Add("Password", "Password");
            labels.Add("Terms", "Terms");
            _langToLabels.Add("telugu", labels);
        }
        #endregion Labels
    }
}
