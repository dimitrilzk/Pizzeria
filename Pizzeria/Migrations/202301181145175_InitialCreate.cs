namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DettagliOrdini",
                c => new
                    {
                        IdDettaglioOrdine = c.Int(nullable: false, identity: true),
                        Quantita = c.Int(),
                        IdPizza = c.Int(),
                        IdOrdine = c.Int(),
                    })
                .PrimaryKey(t => t.IdDettaglioOrdine)
                .ForeignKey("dbo.Ordini", t => t.IdOrdine)
                .ForeignKey("dbo.Pizze", t => t.IdPizza)
                .Index(t => t.IdPizza)
                .Index(t => t.IdOrdine);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        Note = c.String(),
                        Confermato = c.Boolean(),
                        Evaso = c.Boolean(),
                        IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        Pass = c.String(maxLength: 50),
                        Ruolo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
            CreateTable(
                "dbo.Pizze",
                c => new
                    {
                        IdPizza = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                        Prezzo = c.Decimal(storeType: "money"),
                        Ingredienti = c.String(),
                        Foto = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdPizza);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DettagliOrdini", "IdPizza", "dbo.Pizze");
            DropForeignKey("dbo.Ordini", "IdUser", "dbo.Users");
            DropForeignKey("dbo.DettagliOrdini", "IdOrdine", "dbo.Ordini");
            DropIndex("dbo.Ordini", new[] { "IdUser" });
            DropIndex("dbo.DettagliOrdini", new[] { "IdOrdine" });
            DropIndex("dbo.DettagliOrdini", new[] { "IdPizza" });
            DropTable("dbo.Pizze");
            DropTable("dbo.Users");
            DropTable("dbo.Ordini");
            DropTable("dbo.DettagliOrdini");
        }
    }
}
