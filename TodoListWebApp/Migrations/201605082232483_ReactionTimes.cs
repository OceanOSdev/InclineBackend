namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReactionTimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReactionTime",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        TennisBallDrop = c.Int(nullable: false),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReactionTime");
        }
    }
}
