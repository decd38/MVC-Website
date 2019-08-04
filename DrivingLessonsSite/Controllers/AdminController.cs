using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DrivingLessonsSite.Models;
using DrivingLessonsSite.ViewModels;
using System.Data.Entity;


namespace DrivingLessonsSite.Controllers
{
    
   [Authorize(Roles = "CanManageLessons")]
   public class AdminController : Controller
    {

        private ApplicationDbContext _context;

        public IEnumerable<DateTime> LessonDates { get; private set; }
        public IEnumerable<string> TimeSlots { get; private set; }

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }    
              
        public ActionResult ScheduleView(CustomerLesson viewModel)
        {
            if (_context.Customers.Any(d => d.LessonDates.Date == viewModel.LessonDates.Date)) {

                var dbDates = _context.Customers.Where(d => d.LessonDates.Date == viewModel.LessonDates.Date).Include(i => i.TimeSlots).Include(d=>d.LessonDates).OrderBy(t =>t.TimeSlotsID);
                                
                return View(dbDates);
            }
            else
            {
                return RedirectToAction("NoLessonInDbIndex", "Admin");
            }
         
        }

        public ActionResult NoLessonInDbIndex()
        {
            return View();
        }

        public ActionResult EditCancelList(CustomerLesson viewModel)
        {
            if (_context.Customers.Any(s => s.Surname == viewModel.Customers.Surname))
            {
                var SurnameInDb = _context.Customers.Where(s => s.Surname == viewModel.Customers.Surname).Include(i => i.TimeSlots).Include(d => d.LessonDates).OrderBy(d => d.LessonDates.Date);

                return View(SurnameInDb);
            }
            else
            {
                return RedirectToAction("NoNameFound", "Admin");
            }
        }

        public ActionResult NoNameFound()
        {
            return View();
        }

        public ActionResult Edit(int id, int lessonDateID)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.ID == id);

            var TimeSlots = _context.TimeSlots.ToList();

            var LessDate = _context.LessonDates.SingleOrDefault(d => d.ID == lessonDateID);

            if (customer == null)
            {
                return HttpNotFound();
            }                      

            var ViewModel = new CustomerLesson
            {
                TimeSlots = TimeSlots,
                Customers = customer,
                LessonDates = LessDate
            };
                     

            return View("Edit", ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookLesson(CustomerLesson viewModel)
        {
            viewModel.Customers.TimeSlots = _context.TimeSlots.First(t => t.ID == viewModel.Customers.TimeSlotsID);

            if (!ModelState.IsValid)
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

                        return View("EditTryBookAgain", viewModel);
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
            return RedirectToAction("EditConfirmed", "Admin");
        }

        public ActionResult EditTryBookAgain()
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

        public ActionResult EditConfirmed()
        {
            return View();
        }


        /*GET METHOD*/
        public ActionResult Delete(int? id, int? LessDateID)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.ID == id);

            var TimeSlots = _context.TimeSlots.ToList();

            var LessDate = _context.LessonDates.SingleOrDefault(d => d.ID == LessDateID);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var ViewModel = new CustomerLesson
            {
                TimeSlots = TimeSlots,
                Customers = customer,
                LessonDates = LessDate
            };

            return View("Delete", ViewModel);
        }

        /*POST METHOD*/
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id, int LessDateID)
        {            
            LessonDate date = _context.LessonDates.Find(LessDateID);
            _context.LessonDates.Remove(date);
            _context.SaveChanges();

           /* Customer customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();*/

            return RedirectToAction("Index", "Admin");

        }

    }
}


