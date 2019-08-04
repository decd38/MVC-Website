namespace DrivingLessonsSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {

            Sql(@"
INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'0f0a612c-8273-47ef-a5c6-d2ed2c7c4b78', N'guest@mvctest01.com', 0, N'APLtNtbUCVB6aDRgygtHVZsWncnhu9Q1Di/fpjiFTY3xunZ8laWt78hWjbxOIYy52g==', N'8f9196f6-499d-4a4a-a21f-a1236c5ccd19', NULL, 0, 0, NULL, 1, 0, N'guest@mvctest01.com')
INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'76d9b0d3-3e6c-4d7c-96e2-4bc8198e8ca4', N'admin@mdds.com', 0, N'AAoEsy9nh7jWbJNmpxHBTaZFgX/bj38/Bwuwko+VfGfCOa5hG/1KtO53HcH9fMOCXw==', N'9a676f1c-39e5-46c9-8c22-69a8e9730168', NULL, 0, 0, NULL, 1, 0, N'admin@mdds.com')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'a661b8da-2e66-447a-87cf-8b8b2e88ca84', N'CanManageLessons')
INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(N'76d9b0d3-3e6c-4d7c-96e2-4bc8198e8ca4', N'a661b8da-2e66-447a-87cf-8b8b2e88ca84')
");

        }

    public override void Down()
        {
        }
    }
}
