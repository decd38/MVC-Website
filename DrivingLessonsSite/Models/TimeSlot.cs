using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace DrivingLessonsSite.Models
{
    public class TimeSlot
    {
        public int ID { get; set; }

        [Required]
        public string TimeOfLesson { get; set; }
    }
}