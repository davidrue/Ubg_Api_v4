namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeysrecipientSenderId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Id", "dbo.Actors");
            DropIndex("dbo.Transactions", new[] { "Id" });
            AddColumn("dbo.Transactions", "RecipientId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Transactions", "SenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "RecipientId");
            CreateIndex("dbo.Transactions", "SenderId");
            AddForeignKey("dbo.Transactions", "RecipientId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "SenderId", "dbo.Actors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "SenderId", "dbo.Actors");
            DropForeignKey("dbo.Transactions", "RecipientId", "dbo.Actors");
            DropIndex("dbo.Transactions", new[] { "SenderId" });
            DropIndex("dbo.Transactions", new[] { "RecipientId" });
            DropColumn("dbo.Transactions", "SenderId");
            DropColumn("dbo.Transactions", "RecipientId");
            CreateIndex("dbo.Transactions", "Id");
            AddForeignKey("dbo.Transactions", "Id", "dbo.Actors", "Id");
        }
    }
}
