using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Devoir
    {
        [Key]
        public int Id { get; set; }
        public string Titre { get; set; }
        public string instructions { get; set; }
        public int Points { get; set; }
        public DateTime DateLimite { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<Render_Devoir> Render_Devoirs { get; set; }
        public virtual ICollection<Attachement> Attachements { get; set; }

    }
}