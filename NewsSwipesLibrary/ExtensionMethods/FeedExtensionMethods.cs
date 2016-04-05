using System;
using System.Collections.Generic;
using DataContracts.Search;
using DataContracts.Client;
using DataContracts.Server;

namespace NewsSwipesLibrary
{
    public static class FeedExtensionMethods
    {
        public static SkippedUrlsIndexDoc ToSkippedUrlsIndexDoc(this UnpublishedPost post)
        {
            var doc = new SkippedUrlsIndexDoc
            {
                Id = Guid.NewGuid().ToString(),
                Url = post.OriginalLink.ToLower(),
                Date = DateTime.UtcNow
            };
            return doc;
        }

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
                CreatedTime = indexDoc.CreatedTime.HasValue ? indexDoc.CreatedTime.Value : DateTime.UtcNow,
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

        public static PostPreview ToPostPreview(this Feed feed, Utils utils)
        {
            var images = utils.ExtractImages(feed.Link);
            var postPreview = new PostPreview()
            {
                Date = feed.PublishedDate.ToString(),
                CardStyle = "Horizontal",
                Heading = feed.Title,
                Snippet = feed.Description,
                OriginalLink = feed.Link,
                Images = images,
                ImagesFromDb = new List<DbImage>().ToArray()
            };
            return postPreview;
        }

        public static FeedsIndexDoc ToFeedIndexDoc(this UserReaction reaction, string[] likedby, string[] sharedby)
        {
            var doc = new FeedsIndexDoc
            {
                Id = reaction.ArticleId,
                LikedBy = likedby,
                SharedBy = sharedby
            };
            return doc;
        }
    }
}
