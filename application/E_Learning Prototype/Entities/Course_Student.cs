using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Course_Student
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public string Id_student { get; set; }

        [ForeignKey("Course")]
        public int Id_Course { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}