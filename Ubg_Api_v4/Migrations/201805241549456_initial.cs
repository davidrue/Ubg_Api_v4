namespace Ubg_Api_v4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        SurName = c.String(nullable: false),
                        Email = c.String(),
                        UserName = c.String(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        IsCommercial = c.Boolean(nullable: false),
                        AuthToken = c.String(),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Iban = c.String(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(nullable: false),
                        Reference = c.String(nullable: false),
                        AvailableUntil = c.DateTime(nullable: false),
                        AdjustibleUp = c.Boolean(nullable: false),
                        AdjustibleDown = c.Boolean(nullable: false),
                        Status = c.String(nullable: false),
                        Actor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.Id)
                .ForeignKey("dbo.Actors", t => t.Actor_Id)
                .Index(t => t.Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.Transactions", "Id", "dbo.Actors");
            DropForeignKey("dbo.BankAccounts", "Id", "dbo.Actors");
            DropIndex("dbo.Transactions", new[] { "Actor_Id" });
            DropIndex("dbo.Transactions", new[] { "Id" });
            DropIndex("dbo.BankAccounts", new[] { "Id" });
            DropTable("dbo.Transactions");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.Actors");
        }
    }
}
