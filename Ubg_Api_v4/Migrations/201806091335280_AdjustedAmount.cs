namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Adjusted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Adjusted");
        }
    }
}
