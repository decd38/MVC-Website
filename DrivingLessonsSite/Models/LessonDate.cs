using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace DrivingLessonsSite.Models
{
    public class LessonDate
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yy}")]
        public DateTime Date { get; set; }   
        
    }
}