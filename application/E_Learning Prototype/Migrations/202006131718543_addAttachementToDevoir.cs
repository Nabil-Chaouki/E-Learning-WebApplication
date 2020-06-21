namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttachementToDevoir : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachements", "Devoir_Id", c => c.Int());
            CreateIndex("dbo.Attachements", "Devoir_Id");
            AddForeignKey("dbo.Attachements", "Devoir_Id", "dbo.Devoirs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachements", "Devoir_Id", "dbo.Devoirs");
            DropIndex("dbo.Attachements", new[] { "Devoir_Id" });
            DropColumn("dbo.Attachements", "Devoir_Id");
        }
    }
}
