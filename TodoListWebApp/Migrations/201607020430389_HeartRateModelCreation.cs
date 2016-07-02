namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeartRateModelCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeartRateModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeartRate = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HeartRateModel");
        }
    }
}
