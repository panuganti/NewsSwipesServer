using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using DataContracts.Search;

namespace DataContracts.Client
{
    [DataContract]
    public class Image
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public bool isFromDb { get; set; }
    }

    [DataContract]
    public class PostPreview : Post
    {
        [DataMember]
        public Image[] Images { get; set; }
    }

    [DataContract]
    public class UnpublishedPost : Post
    {
        [DataMember]
        public Image Image { get; set; }
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
