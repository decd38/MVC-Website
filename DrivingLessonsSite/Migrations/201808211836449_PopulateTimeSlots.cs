namespace DrivingLessonsSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTimeSlots : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[TimeSlots] ON");
            Sql(@"INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (1, N'09:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (2, N'10:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (3, N'11:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (4, N'12:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (5, N'13:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (6, N'14:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (7, N'15:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (8, N'16:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (9, N'17:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (10, N'18:00 One Hour Lesson')
INSERT INTO [dbo].[TimeSlots] ([ID], [TimeOfLesson]) VALUES (11, N'19:00 One Hour Lesson')");
            Sql("SET IDENTITY_INSERT [dbo].[TimeSlots] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
