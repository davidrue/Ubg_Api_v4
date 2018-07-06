namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateInHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DateOfPayment", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "DateOfPayment");
        }
    }
}
