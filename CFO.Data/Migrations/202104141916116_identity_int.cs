namespace CFO.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identity_int : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserRoleTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        ShortName = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoleTypes", t => t.UserRoleTypeId, cascadeDelete: true)
                .Index(t => t.UserRoleTypeId);
            
            CreateTable(
                "dbo.UserRoleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FName = c.String(),
                        LName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Inactive = c.Boolean(nullable: false),
                        ModDate = c.DateTime(),
                        UserRoleId = c.Int(nullable: false),
                        ReqPwdChange = c.Boolean(),
                        RetireDate = c.DateTime(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoles", "UserRoleTypeId", "dbo.UserRoleTypes");
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserRoleTypeId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoleTypes");
            DropTable("dbo.UserRoles");
        }
    }
}
