using E_Learning_Prototype.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning_Prototype.Models
{
    public class InfomationViewModel
    {
        public string Message { get; set; }
        public string Course_code { get; set; }
        public string Classname { get; set; }
        public string ClassSubject { get; set; }
        public List<Information> infos { get; set; }
        public List<DateTime> LatestDatesPerDay { get; set; }

    }
}