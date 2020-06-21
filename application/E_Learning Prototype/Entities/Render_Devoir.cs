using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Entities
{
    public class Render_Devoir
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateRmise { get; set; }
        public int Note { get; set; }

        public virtual Devoir Devoir { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Attachement> Attachements { get; set; }
    }
}