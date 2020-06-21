using E_Learning_Prototype.App_Start;
using E_Learning_Prototype.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace E_Learning_Prototype.infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Information> Informations { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("name = Context")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new IdentityDbInit());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Information> Informations { get; set; }
        public virtual DbSet<Attachement> Attachements { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Course_Student> Course_Students { get; set; }
        public virtual DbSet<Devoir> Devoirs { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<Render_Devoir> Render_Devoirs { get; set; }
    }

    class IdentityDbInit : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            var role = roleManager.FindByName("Teacher");
            var roleB = roleManager.FindByName("Student");
            if (role == null)
            {
                role = new IdentityRole("Teacher");
                var roleresult = roleManager.Create(role);
            }
            if (roleB == null)
            {
                role = new IdentityRole("Student");
                var roleresult = roleManager.Create(role);
            }
            db.Themes.AddOrUpdate(x => x.Id,
                new Theme() { Id = 1, Nom = "Examen" },
                new Theme() { Id = 2, Nom = "Obliger" },
                new Theme() { Id = 3, Nom = "Optionnel" }
            );

        }
    }
}