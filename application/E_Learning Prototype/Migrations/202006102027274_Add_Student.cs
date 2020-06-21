namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "Id" });
            DropTable("dbo.Students");
        }
    }
}
