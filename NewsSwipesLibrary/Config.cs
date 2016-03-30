using System.Collections.Generic;

namespace NewsSwipesLibrary
{
    public class Config
    {
        private Dictionary<string, Dictionary<string, string>> _langToLabels 
                        = new Dictionary<string, Dictionary<string, string>>();

        public Config()
        {
            BuildEnglishLabelDictionary();
            BuildHindiLabelDictionary();
            BuildMarathiLabelDictionary();
            BuildTeluguLabelDictionary();
        }

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

        #region 

        #endregion
    }
}
