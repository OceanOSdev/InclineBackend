namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StepTestDecouple : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StepTestHeartRateModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StepTestHeartRate = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.StepTestModel", "StepTestHeartRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StepTestModel", "StepTestHeartRate", c => c.Int(nullable: false));
            DropTable("dbo.StepTestHeartRateModel");
        }
    }
}
