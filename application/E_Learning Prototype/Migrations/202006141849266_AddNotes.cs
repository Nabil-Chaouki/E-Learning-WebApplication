namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Render_Devoir", "Note", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Render_Devoir", "Note");
        }
    }
}
