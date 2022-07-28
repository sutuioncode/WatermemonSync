using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatermemonSync.models
{
    public class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
