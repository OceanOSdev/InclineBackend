namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HealthRelatedMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyComposition",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        BodyFat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CardiovascularFitness",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        HalfMileTime = c.Time(nullable: false, precision: 7),
                        Pacer = c.Int(nullable: false),
                        MileTime = c.Time(nullable: false, precision: 7),
                        StepTestSteps = c.Int(nullable: false),
                        StepTestHeartRate = c.Int(nullable: false),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Flexibility",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        SitAndReach = c.Int(nullable: false),
                        ArmAndShoulder = c.Int(nullable: false),
                        TrunkLift = c.Int(nullable: false),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MuscularStrengthAndEndurance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        CurlUps = c.Int(nullable: false),
                        RightAnglePushUps = c.Int(nullable: false),
                        MaxBench = c.Int(nullable: false),
                        MaxLegPress = c.Int(nullable: false),
                        PullUps = c.Int(nullable: false),
                        FlexedArmHang = c.Time(nullable: false, precision: 7),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MuscularStrengthAndEndurance");
            DropTable("dbo.Flexibility");
            DropTable("dbo.CardiovascularFitness");
            DropTable("dbo.BodyComposition");
        }
    }
}
