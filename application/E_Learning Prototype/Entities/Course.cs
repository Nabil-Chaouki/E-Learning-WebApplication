using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Section{ get; set; }
        public string Description { get; set; }
        public string Salle { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Course_Student> Course_Students { get; set; }
        public virtual ICollection<Devoir> Devoirs { get; set; }
        public virtual ICollection<Information> Informations { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}