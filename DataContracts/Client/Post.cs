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
    public class UnpublishedPost : PostableEntity
    {
        [DataMember]
        public ImageEntity Image { get; set; }
        [DataMember]
        public bool ShouldSkip { get; set; }
    }

    [DataContract]
    public class PostableEntity: PostEntity
    {
        [DataMember]
        public string[] Streams { get; set; }

        [DataMember]
        public string[] Tags { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string PostedBy { get; set; }
    }

    [DataContract]
    public class PublishedPost : PostableEntity
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public DateTime CreatedTime { get; set; }
        [DataMember]
        public string[] SharedBy { get; set; }
        [DataMember]
        public string[] LikedBy { get; set; }
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
