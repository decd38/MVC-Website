using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrivingLessonsSite.Models;

namespace DrivingLessonsSite.ViewModels
{
    public class CustomerLesson
    {
        public LessonDate LessonDates { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        public Customer Customers { get; set; }
    }
}