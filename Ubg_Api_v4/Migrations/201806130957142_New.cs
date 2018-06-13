namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Actors", "Expiration_AuthToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Expiration_AuthToken", c => c.DateTime(nullable: false));
        }
    }
}
