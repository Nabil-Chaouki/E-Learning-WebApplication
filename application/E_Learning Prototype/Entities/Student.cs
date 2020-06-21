using E_Learning_Prototype.infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Student
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Course_Student> Course_Students { get; set; }
        public virtual ICollection<Render_Devoir> Render_Devoirs { get; set; }
    }
}