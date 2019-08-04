using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DrivingLessonsSite.Models;
using DrivingLessonsSite.ViewModels;
using System.Data.SqlClient;
using System.Data.Entity;

namespace DrivingLessonsSite.Controllers
{
    public class LessonController : Controller
    {
        private ApplicationDbContext _context;

        public IEnumerable<DateTime> LessonDates { get; private set; }
        public IEnumerable<string> TimeSlots { get; private set; }

        //constructor 
        public LessonController()
        {
            _context = new ApplicationDbContext();
        }
        //disposable object
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Lesson/New
        public ActionResult New()
        {
            var TimeSlots = _context.TimeSlots.ToList();

            var customer = new Customer();

            var LessDate = new LessonDate();

            var ViewModel = new CustomerLesson
            {
                TimeSlots = TimeSlots,
                Customers = customer,
                LessonDates = LessDate
            };

            return View(ViewModel);
        }
             

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookLesson(CustomerLesson viewModel)
        {
            viewModel.Customers.TimeSlots = _context.TimeSlots.First(t => t.ID == viewModel.Customers.TimeSlotsID);
            
            if(!ModelState.IsValid)
            {
                viewModel = new CustomerLesson
                {
                    TimeSlots = _context.TimeSlots.ToList(),
                    LessonDates = viewModel.LessonDates,
                    Customers = viewModel.Customers
                };

                return View("New", viewModel);
            }

            if ((_context.Customers.Any(d => d.LessonDates.Date.Equals(viewModel.LessonDates.Date)) && (_context.Customers.Any(t => t.TimeSlotsID.Equals(viewModel.Customers.TimeSlotsID)))))
            {
                var custInDB = _context.Customers.Include(d => d.LessonDates);
                foreach (Customer c in custInDB)
                {                
                    if ((c.LessonDates.Date == viewModel.LessonDates.Date) && (c.TimeSlotsID == viewModel.Customers.TimeSlotsID))
                        {
                             viewModel = new CustomerLesson
                             {
                               TimeSlots = _context.TimeSlots.ToList(),
                               LessonDates = viewModel.LessonDates,
                               Customers = viewModel.Customers
                             };

                         return View("TryBookAgain", viewModel);
                        }                   
                }                
            }
            /* NEW CUSTOMER LESSON*/
            if (viewModel.Customers.ID == 0)
            {
                LessonDate newLessonDate = new LessonDate();

                newLessonDate.Date = viewModel.LessonDates.Date;
                newLessonDate.ID = viewModel.LessonDates.ID;

                _context.LessonDates.Add(newLessonDate);
                _context.SaveChanges();

                viewModel.Customers.LessonDatesID = newLessonDate.ID;

                _context.Customers.Add(viewModel.Customers);
                _context.SaveChanges();
                _context.Customers.Add(viewModel.Customers);
            }
            /* EXISITING CUSTOMER LESSON*/
            else
            {
                var existingBkdLesson = _context.Customers.Single(c => c.ID == viewModel.Customers.ID);
                var LessDateID = existingBkdLesson.LessonDatesID;
                var existingLessDate = _context.LessonDates.Single(d => d.ID == LessDateID);

                existingLessDate.Date = viewModel.LessonDates.Date;
                _context.SaveChanges();

                existingBkdLesson.FirstName = viewModel.Customers.FirstName;
                existingBkdLesson.Surname = viewModel.Customers.Surname;
                existingBkdLesson.TelephoneNo = viewModel.Customers.TelephoneNo;
                existingBkdLesson.TimeSlotsID = viewModel.Customers.TimeSlotsID;

                _context.SaveChanges();               
            }
            return RedirectToAction("BookingSuccess", "Lesson");
        }

        public ActionResult TryBookAgain()
        {
            var TimeSlots = _context.TimeSlots.ToList();

            var customer = new Customer();

            var LessDate = new LessonDate();

            var ViewModel = new CustomerLesson
            {
                TimeSlots = TimeSlots,
                Customers = customer,
                LessonDates = LessDate
            };

            return View(ViewModel);
        }

        public ActionResult BookingSuccess()
        {
            return View();
        }
                 
        
    }
}