namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondTry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Expiration_AuthToken", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "Expiration_AuthToken");
        }
    }
}
