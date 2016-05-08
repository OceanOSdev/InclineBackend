namespace TodoListWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IssValue = c.String(),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        AdminConsented = c.Boolean(nullable: false),
                        TempCleanup = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Todo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UPN = c.String(nullable: false, maxLength: 128),
                        TenantID = c.String(),
                    })
                .PrimaryKey(t => t.UPN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Todo");
            DropTable("dbo.Tenant");
        }
    }
}
