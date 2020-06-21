namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Course_CourseStudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Section = c.String(),
                        Description = c.String(),
                        Salle = c.String(),
                        Code = c.String(),
                        Teacher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Course_Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_student = c.String(maxLength: 128),
                        Id_Course = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Id_Course, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Id_student)
                .Index(t => t.Id_student)
                .Index(t => t.Id_Course);
            
            AddColumn("dbo.Information", "Course_Id", c => c.Int());
            CreateIndex("dbo.Information", "Course_Id");
            AddForeignKey("dbo.Information", "Course_Id", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Information", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Course_Student", "Id_student", "dbo.Students");
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Course_Student", "Id_Course", "dbo.Courses");
            DropIndex("dbo.Course_Student", new[] { "Id_Course" });
            DropIndex("dbo.Course_Student", new[] { "Id_student" });
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.Information", new[] { "Course_Id" });
            DropColumn("dbo.Information", "Course_Id");
            DropTable("dbo.Course_Student");
            DropTable("dbo.Courses");
        }
    }
}
