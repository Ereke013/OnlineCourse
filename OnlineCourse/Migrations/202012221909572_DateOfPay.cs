namespace OnlineCourse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOfPay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 255),
                        LevelId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Course_Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.CourseLevels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                        PriceId = c.Int(nullable: false),
                        Course_Duration = c.Int(nullable: false),
                        DateOfPay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prices", t => t.PriceId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.PriceId);
            
            CreateTable(
                "dbo.CourseSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentId = c.Int(nullable: false),
                        Check = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "LevelId", "dbo.CourseLevels");
            DropForeignKey("dbo.Payments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Payments", "PriceId", "dbo.Prices");
            DropForeignKey("dbo.CourseSelections", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.CourseSelections", new[] { "PaymentId" });
            DropIndex("dbo.Payments", new[] { "PriceId" });
            DropIndex("dbo.Payments", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.Courses", new[] { "LevelId" });
            DropTable("dbo.Prices");
            DropTable("dbo.CourseSelections");
            DropTable("dbo.Payments");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseLevels");
        }
    }
}
