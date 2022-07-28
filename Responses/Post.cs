using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WatermemonSync.Responses
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Subtitle { get; set; }
        public string BlogId { get; set; }
    }

    public class PostMapper
    {
        public static Post Map(models.Post post)
        {
            return new Post()
            {
                Title = post.Title,
                Body = post.Body,
                Id = post.Id,
                BlogId = post.BlogId,
                Subtitle = post.Subtitle,

            };
        }
    }
}
