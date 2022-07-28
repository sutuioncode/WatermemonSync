using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatermemonSync.Responses;

namespace WatermemonSync.Request
{
    public class PushRequest
    {
        public SyncRequest Changes { get; set; }
    }public class PullResponse
    {
        public SyncRequest Changes { get; set; }
        public long Timestamp {get;set;}
    }
    public class SyncRequest
    {
        public RequestMapper<Post> Posts { get; set; } 
        public RequestMapper<Comment> Comments{ get; set; } 
        public RequestMapper<Blog> Blogs{ get; set; } 
    }

    public class RequestMapper<T>
    {
        public List<T> Created;
        public List<T> Updated;
        public List<string> Deleted;
    }
}
