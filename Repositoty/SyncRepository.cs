using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatermemonSync.models;
using WatermemonSync.Request;

namespace WatermemonSync.Repositoty
{
    public class SyncRepository
    {
        private BlogDataContext context;

        public SyncRepository(BlogDataContext context)
        {
            this.context = context;
        }
        public SyncRequest Pull(DateTime? lastPulledAt)
        {

            var createdPosts = context.Posts.Where((post) => post.CreatedAt >= lastPulledAt)
                .Select(Responses.PostMapper.Map)
                .ToList();
            var updatedPosts = context.Posts.Where((post) => post.CreatedAt < lastPulledAt && post.UpdatedAt > lastPulledAt)
                .Select(Responses.PostMapper.Map)
                .ToList();
            var deletedPosts = context.Posts.Where((post) => post.CreatedAt < lastPulledAt && post.DeletedAt > lastPulledAt)
                .Select((p) => p.Id)
                .ToList();

            var createdComments = context.Comments.Where((post) => post.CreatedAt >= lastPulledAt).Select(Responses.CommentMapper.Map).ToList();
            var updatedComments = context.Comments.Where((post) => post.CreatedAt < lastPulledAt && post.UpdatedAt > lastPulledAt).Select(Responses.CommentMapper.Map).ToList();
            var deletedComments = context.Comments.Where((post) => post.CreatedAt < lastPulledAt && post.DeletedAt > lastPulledAt).Select(c => c.Id).ToList();

            var createdBlogs = context.Blogs.Where((post) => post.CreatedAt >= lastPulledAt)
                .Select(Responses.BlogMapper.Map)
                .ToList();
            var updatedBlogs = context.Blogs.Where((post) => post.CreatedAt < lastPulledAt && post.UpdatedAt > lastPulledAt)
                .Select(Responses.BlogMapper.Map)
                .ToList();
            var deletedBlogs = context.Blogs.Where((post) => post.CreatedAt < lastPulledAt && post.DeletedAt > lastPulledAt)
                .Select((blog) => blog.Id)
                .ToList();

            return new SyncRequest()
            {
                Blogs = new RequestMapper<Responses.Blog> { Created = createdBlogs, Updated = updatedBlogs, Deleted = deletedBlogs },
                Comments = new RequestMapper<Responses.Comment> { Created = createdComments, Updated = updatedComments, Deleted = deletedComments },
                Posts = new RequestMapper<Responses.Post> { Created = createdPosts, Updated = updatedPosts, Deleted = deletedPosts }

            };
        }
        public void Push(SyncRequest request)
        {
            if (request.Blogs.Created.Count > 0)
            {

                context.Blogs.AddRange(request.Blogs.Created.ConvertAll((blog) => new Blog() { Id = blog.Id, Name = blog.Name }));
            }
            if (request.Blogs.Updated.Count > 0)
            {

                context.Blogs.UpdateRange(request.Blogs.Updated.ConvertAll((blog) => new Blog() { Id = blog.Id, Name = blog.Name }));
            }
            if (request.Blogs.Deleted.Count > 0)
            {

                var deletedBlogs = context.Blogs.Where((blog) => request.Blogs.Deleted.Any(x => x == blog.Id)).ToList();
                context.Blogs.RemoveRange(deletedBlogs);
            }



            context.SaveChanges();

            if (request.Posts.Created.Count > 0)
            {

                context.Posts.AddRange(request.Posts.Created.ConvertAll((post) => new Post() { Id = post.Id, Body = post.Body, BlogId = post.BlogId, Title = post.Title, Subtitle = post.Subtitle }));
            }
            if (request.Posts.Updated.Count > 0)
            {

                context.Posts.UpdateRange(request.Posts.Updated.ConvertAll((post) => new Post() { Id = post.Id, Body = post.Body, BlogId = post.BlogId, Title = post.Title, Subtitle = post.Subtitle }));
            }
            if (request.Posts.Deleted.Count > 0)
            {
                var deletedPosts = context.Posts.Where((post) => request.Posts.Deleted.Any(x => x == post.Id)).ToList();

                context.Posts.RemoveRange(deletedPosts);

            }

            context.SaveChanges();
            if (request.Comments.Created.Count > 0)
            {

                context.Comments.AddRange(request.Comments.Created.ConvertAll((comment) => new Comment() { Id = comment.Id, Body = comment.Body, IsNasty = comment.IsNasty, PostId = comment.PostId }));
            }
            if (request.Comments.Updated.Count > 0)
            {

                context.Comments.UpdateRange(request.Comments.Updated.ConvertAll((comment) => new Comment() { Id = comment.Id, Body = comment.Body, IsNasty = comment.IsNasty, PostId = comment.PostId }));
            }
            if (request.Comments.Deleted.Count > 0)
            {

                var deletedComments = context.Comments.Where((comment) => request.Comments.Deleted.Any(x => x == comment.Id)).ToList();
                context.Comments.RemoveRange(deletedComments);
            }



            context.SaveChanges();


        }
    }
}
