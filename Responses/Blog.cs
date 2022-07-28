using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatermemonSync.Responses
{
    public class Blog
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class BlogMapper
    {
        public static Blog Map(models.Blog Blog)
        {

                return new Blog()
                {
                    Name = Blog.Name,
                    Id = Blog.Id,

                };

        }
    }
}
