using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DrivingLessonsSite.Models
{
    [Bind(Exclude = "LessonDates, TimeSlots")]

    public class Customer
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [Phone]
        public string TelephoneNo { get; set; }

        public LessonDate LessonDates { get; set; }

        public int LessonDatesID { get; set; }

        public TimeSlot TimeSlots { get; set; }

        public int TimeSlotsID { get; set; }

    }
}