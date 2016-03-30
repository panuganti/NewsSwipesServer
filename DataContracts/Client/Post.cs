using System;
using System.Runtime.Serialization;
using DataContracts.Search;

namespace DataContracts.Client
{
    [DataContract]
    public class DbImage : ImageEntity
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
     }

    [DataContract]
    public class ImageEntity
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string[] Tags { get; set; }
    }

    [DataContract]
    public class PostPreview : PostEntity
    {
        [DataMember]
        public DbImage[] ImagesFromDb { get; set; }
        [DataMember]
        public string[] Images { get; set; }
    }

    [DataContract]
    public class UnpublishedPost : PostEntity
    {
        [DataMember]
        public ImageEntity Image { get; set; }

        [DataMember]
        public string[] Streams { get; set; }

        [DataMember]
        public string[] Tags { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string PostedBy { get; set; }

        public FeedsIndexDoc ToFeedsIndexDoc()
        {
            var doc = new FeedsIndexDoc();

            doc.Id = Guid.NewGuid().ToString();
            doc.Title = Heading.ToLower();
            doc.Text = Snippet.ToLower();
            doc.CreatedTime = DateTime.UtcNow;
            doc.ImageUrl = Image.Url.ToLower();
                doc.LandingPageUrl = OriginalLink.ToLower();
            doc.CardStyle = CardStyle.ToLower();
            if (doc.PostedBy != null) // TODO: Remove
            {
                doc.PostedBy = PostedBy.ToLower();
            }
            doc.Language = Language.ToLower();
            return doc;
        }
    }

    [DataContract]
    public class PostEntity
    {
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string CardStyle { get; set; }
        [DataMember]
        public string Heading { get; set; }
        [DataMember]
        public string Snippet { get; set; }
        [DataMember]
        public string OriginalLink { get; set; }
    }
}
