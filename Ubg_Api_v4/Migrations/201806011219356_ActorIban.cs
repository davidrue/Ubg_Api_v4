namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorIban : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "firstIban", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "firstIban");
        }
    }
}
