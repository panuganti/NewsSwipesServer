using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts.Search;
using DataContracts.Client;

namespace NewsSwipesLibrary
{
    public static class FeedExtensionMethods
    {
        public static FeedsIndexDoc ToFeedsIndexDoc(this UnpublishedPost unpublishedPost)
        {
            var doc = new FeedsIndexDoc
            {
                Id = Guid.NewGuid().ToString(),
                Title = unpublishedPost.Heading,
                Text = unpublishedPost.Snippet,
                CreatedTime = DateTime.UtcNow,
                ImageUrl = unpublishedPost.Image.Url,
                LandingPageUrl = unpublishedPost.OriginalLink.ToLower(),
                OriginalLink = unpublishedPost.OriginalLink,
                CardStyle = unpublishedPost.CardStyle.ToLower(), // TODO:
                PostedBy = unpublishedPost.PostedBy.ToLower(),
                SharedBy = new string[] { },
                LikedBy = new string[] { },
                Language = unpublishedPost.Language.ToLower(),
                Tags = unpublishedPost.Tags,
                Streams = unpublishedPost.Streams
            };
            return doc;
        }

        public static PublishedPost ToPublishedPost(this FeedsIndexDoc indexDoc)
        {
            var publishedPost = new PublishedPost
            {
                Id = indexDoc.Id,
                Heading = indexDoc.Title,
                Snippet = indexDoc.Text,
                CreatedTime = indexDoc.CreatedTime,
                ImageUrl = indexDoc.ImageUrl,
                OriginalLink = indexDoc.OriginalLink,
                CardStyle = indexDoc.CardStyle,
                PostedBy = indexDoc.PostedBy,
                SharedBy = indexDoc.SharedBy,
                LikedBy = indexDoc.LikedBy,
                Streams = indexDoc.Streams,
                Language = indexDoc.Language,
                Tags = indexDoc.Tags
            };
            return publishedPost;
        }
    }
}
