namespace Search
{
    public class IndexFactory
    {
        public static SearchIndex FeedsIndex
        {
            get
            {
                return new SearchIndex("feeds", "newsswipes", "2F8F3CE4F1F440B7E792E51CDE103B17");
            }
        }

        public static SearchIndex ImagesIndex
        {
            get
            {
                return new SearchIndex("images", "newsswipes", "2F8F3CE4F1F440B7E792E51CDE103B17"); 
            }
        }

        public static SearchIndex CredentialsIndex
        {
            get
            {
                return new SearchIndex("users", "newsswipesprod", "FCCE9E142A7AD2BC492E1C9DB9F650FA");
            }
        }

        public static SearchIndex SkippedUrlsIndex
        {
            get
            {
                return new SearchIndex("skippedurls", "newsswipesprod", "FCCE9E142A7AD2BC492E1C9DB9F650FA");
            }
        }
    }
}
