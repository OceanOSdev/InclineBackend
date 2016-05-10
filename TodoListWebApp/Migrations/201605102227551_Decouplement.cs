namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decouplement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArmAndShoulderModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArmAndShoulder = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CurlUpModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurlUps = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FlexedArmHangModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FlexedArmHang = c.Time(nullable: false, precision: 7),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HalfMileTimeModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HalfMileTime = c.Time(nullable: false, precision: 7),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HeightModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Height = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaxBenchModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaxBench = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaxLegPressModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaxLegPress = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MileTimeModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MileTime = c.Time(nullable: false, precision: 7),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PacerModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pacer = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PercentBodyFatModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BodyFat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PullUpModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PullUps = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RightAnglePushUpModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RightAnglePushUps = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SitAndReachModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SitAndReach = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StepTestModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StepTestSteps = c.Int(nullable: false),
                        StepTestHeartRate = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrunkLiftModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TrunkLift = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WeightModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        Owner = c.String(),
                        Logged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WeightModel");
            DropTable("dbo.TrunkLiftModel");
            DropTable("dbo.StepTestModel");
            DropTable("dbo.SitAndReachModel");
            DropTable("dbo.RightAnglePushUpModel");
            DropTable("dbo.PullUpModel");
            DropTable("dbo.PercentBodyFatModel");
            DropTable("dbo.PacerModel");
            DropTable("dbo.MileTimeModel");
            DropTable("dbo.MaxLegPressModel");
            DropTable("dbo.MaxBenchModel");
            DropTable("dbo.HeightModel");
            DropTable("dbo.HalfMileTimeModel");
            DropTable("dbo.FlexedArmHangModel");
            DropTable("dbo.CurlUpModel");
            DropTable("dbo.ArmAndShoulderModel");
        }
    }
}
