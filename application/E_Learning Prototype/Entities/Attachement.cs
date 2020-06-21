using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Attachement
    {
        [Key]
        public int Id { get; set; }

        public string url { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }

        public string FileName { get; set; }

        public virtual Information Information { get; set; }
        public virtual Render_Devoir Render_Devoir { get; set; }
        public virtual Devoir Devoir { get; set; }
    }
}