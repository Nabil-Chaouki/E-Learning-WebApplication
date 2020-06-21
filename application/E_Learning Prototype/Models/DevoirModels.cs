using E_Learning_Prototype.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Models
{
    public class DevoirViewModel
    {
        public int Id_Devoir { get; set; }
        public string Titre { get; set; }
        public string instructions { get; set; }
        public int Assigned { get; set; }
        public int HandedIn { get; set; }
        public string CodeCourse { get; set; }
        public bool Done { get; set; }
        public string Theme { get; set; }
        public DateTime DateLimite { get; set; }
        public IEnumerable<Attachement> Attachements { get; set; }
    }

    public class AddDevoirViewModel
    {
        public string CodeCourse { get; set; }
        public string Titre { get; set; }
        public string instructions { get; set; }
        public int Points { get; set; }
        public DateTime DateLimite { get; set; }
        public int Theme_id { get; set; }
    }

    public class RendreDevoirViewModel
    {
        public bool Done { get; set; }
        public Render_Devoir Render_devoir { get; set; }
        public virtual Devoir Devoir { get; set; }
    }

    public class DetailDevoirModel {
        public string Last_Name { get; set; }
        public int Id { get; set; }
        public string First_Name { get; set; }
        public DateTime Date_Remis { get; set; }
        public int Note { get; set; }
    }

    public class DetailDevoirModelView
    {
        public IEnumerable<DetailDevoirModel> remis { get; set; }
        public IEnumerable<DetailDevoirModel> NoRemis { get; set; }
    }

    public class MarkDevoirViewModel
    {
        public int Id_rendreDevoire { get; set; }
        public int Note { get; set; }
        public int DevoirNote { get; set; }
        public string  FullName { get; set; }
        public IEnumerable<Attachement> Attachements { get; set; }

    }
}