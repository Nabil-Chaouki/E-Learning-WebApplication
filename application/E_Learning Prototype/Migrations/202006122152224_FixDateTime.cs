namespace E_Learning_Prototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devoirs", "DateLimite", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devoirs", "DateLimite", c => c.String());
        }
    }
}
