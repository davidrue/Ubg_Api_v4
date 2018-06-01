namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyActorId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "Id", "dbo.Actors");
            DropIndex("dbo.BankAccounts", new[] { "Id" });
            AddColumn("dbo.BankAccounts", "ActorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BankAccounts", "ActorId");
            AddForeignKey("dbo.BankAccounts", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccounts", "ActorId", "dbo.Actors");
            DropIndex("dbo.BankAccounts", new[] { "ActorId" });
            DropColumn("dbo.BankAccounts", "ActorId");
            CreateIndex("dbo.BankAccounts", "Id");
            AddForeignKey("dbo.BankAccounts", "Id", "dbo.Actors", "Id");
        }
    }
}
