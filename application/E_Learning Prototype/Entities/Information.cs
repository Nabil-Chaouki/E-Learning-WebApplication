using E_Learning_Prototype.infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Information
    {
        [Key]
        public int Id { get; set; }
        public string Object { get; set; }
        public string Message { get; set; }
        public DateTime Publish_Date { get; set; }

        public virtual ApplicationUser user { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<Attachement> Attachements { get; set; }
    }
}