namespace ProjetoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Tipo");
        }
    }
}
