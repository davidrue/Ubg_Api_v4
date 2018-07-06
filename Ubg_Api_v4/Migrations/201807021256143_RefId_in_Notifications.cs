namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefId_in_Notifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "ref_id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "ref_id");
        }
    }
}
