using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatermemonSync.Responses
{
    public class Comment
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public bool IsNasty { get; set; }
        public string PostId { get; set; }
    }

    public class CommentMapper
    {
        public static  Comment Map(models.Comment Comment)
        {       return new Comment()
                {

                    Body = Comment.Body,
                    Id = Comment.Id,
                    PostId = Comment.PostId,
                    IsNasty = Comment.IsNasty,

            };

        }
    }
}
