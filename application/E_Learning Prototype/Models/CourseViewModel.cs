using E_Learning_Prototype.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Models
{

    public class CourseModel
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public int StudentsCount { get; set; }
        public string CodeCourse { get; set; }
    }

    public class CourseViewModel
    {
        public IEnumerable<CourseModel> courses { get; set; }
        public Course Course { get; set; }
    }
}