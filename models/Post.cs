using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatermemonSync.models
{
    public class Post : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id{ get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Subtitle { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Blog Blog { get; set; }
        public string BlogId { get; set; }
    }
}
