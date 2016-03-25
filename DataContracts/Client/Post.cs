using System;
using System.Runtime.Serialization;
using DataContracts.Search;

namespace DataContracts.Client
{
    [DataContract]
    public class DbImage
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
     }

    [DataContract]
    public class PostPreview : Post
    {
        [DataMember]
        public DbImage[] ImagesFromDb { get; set; }
        [DataMember]
        public string[] Images { get; set; }
    }

    [DataContract]
    public class UnpublishedPost : Post
    {
        [DataMember]
        public DbImage Image { get; set; }

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
            var doc = new FeedsIndexDoc
            {
                Id = Guid.NewGuid().ToString(),
                Title = Heading,
                Text = Snippet,
                CreatedTime = DateTime.UtcNow,
                ImageUrl = Image.Url,
                LandingPageUrl = OriginalLink,
                CardStyle = CardStyle, // TODO:
                PostedBy = PostedBy,
                SharedBy = new string[] { },
                LikedBy = new string[] { }
            };
            return doc;
        }
    }

    [DataContract]
    public class Post
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

    [DataContract]
    public class PublishedPost : UnpublishedPost
    {
        [DataMember]
        public string[] Likes { get; set; }
        [DataMember]
        public string[] ReTweets { get; set; }

        public FeedsIndexDoc ToFeed(string user)
        {
            try
            {
                var feed = new FeedsIndexDoc
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Heading,
                    CreatedTime = DateTime.Now,
                    ImageUrl = Image.Url, // TODO: Here, upload to g cloud and get new url
                    LandingPageUrl = OriginalLink,
                    CardStyle = CardStyle,
                    PostedBy = user,
                    SharedBy = new string[] { },
                    LikedBy = new string[] { }
                };
                return feed;
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}
