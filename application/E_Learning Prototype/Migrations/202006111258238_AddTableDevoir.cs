namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableDevoir : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Render_Devoir",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRmise = c.DateTime(nullable: false),
                        Devoir_Id = c.Int(),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devoirs", t => t.Devoir_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Devoir_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Devoirs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        instructions = c.String(),
                        Points = c.Int(nullable: false),
                        DateLimite = c.String(),
                        Course_Id = c.Int(),
                        Teacher_Id = c.String(maxLength: 128),
                        Theme_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Theme_Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Attachements", "Render_Devoir_Id", c => c.Int());
            CreateIndex("dbo.Attachements", "Render_Devoir_Id");
            AddForeignKey("dbo.Attachements", "Render_Devoir_Id", "dbo.Render_Devoir", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Render_Devoir", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Devoirs", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Devoirs", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Render_Devoir", "Devoir_Id", "dbo.Devoirs");
            DropForeignKey("dbo.Devoirs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Attachements", "Render_Devoir_Id", "dbo.Render_Devoir");
            DropIndex("dbo.Devoirs", new[] { "Theme_Id" });
            DropIndex("dbo.Devoirs", new[] { "Teacher_Id" });
            DropIndex("dbo.Devoirs", new[] { "Course_Id" });
            DropIndex("dbo.Render_Devoir", new[] { "Student_Id" });
            DropIndex("dbo.Render_Devoir", new[] { "Devoir_Id" });
            DropIndex("dbo.Attachements", new[] { "Render_Devoir_Id" });
            DropColumn("dbo.Attachements", "Render_Devoir_Id");
            DropTable("dbo.Themes");
            DropTable("dbo.Devoirs");
            DropTable("dbo.Render_Devoir");
        }
    }
}
