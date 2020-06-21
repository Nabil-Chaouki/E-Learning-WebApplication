using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public ICollection<Devoir> Devoirs { get; set; }
    }
}