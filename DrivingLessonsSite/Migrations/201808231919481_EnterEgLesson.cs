namespace DrivingLessonsSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnterEgLesson : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT[dbo].[LessonDates] ON
INSERT INTO[dbo].[LessonDates]
        ([ID], [Date]) VALUES(1, N'2018-11-30 00:00:00')
SET IDENTITY_INSERT[dbo].[LessonDates] OFF");


           Sql(@"SET IDENTITY_INSERT[dbo].[Customers]ON
INSERT INTO[dbo].[Customers]
        ([ID], [FirstName], [Surname], [TelephoneNo], [LessonDatesID], [TimeSlotsID]) VALUES(1, N'Mark', N'Doherty', 02830267868, 1, 1)
SET IDENTITY_INSERT[dbo].[Customers] OFF");
    }
        
        public override void Down()
        {
        }
    }
}
