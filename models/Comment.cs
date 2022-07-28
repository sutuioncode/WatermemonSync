using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatermemonSync.models
{
    public class Comment : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Body { get; set; }
        public bool IsNasty { get; set; }
        public Post Post { get; set; }
        public string PostId { get; set; }
    }
}
