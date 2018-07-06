namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30_06_2018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false));
        }
    }
}
