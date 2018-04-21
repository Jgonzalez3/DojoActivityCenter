using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BeltExam.Controllers{
    public class ActivityCenterController : Controller{
        private BeltExamContext _context;
        public ActivityCenterController(BeltExamContext context){
            _context = context;
        }
        [HttpGet]
        [Route("/Home")]
        public IActionResult Dashboard(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Register", "Home");
            }
            
            User username = _context.Users.SingleOrDefault(x=>x.UserId == UserId);
            ViewBag.UserName = username.firstname;
            ViewBag.UserId = username.UserId;
            List<Event> AllEvents = _context.Events.Include(User=> User.User).Include(Event=>Event.participants).ThenInclude(Participant=>Participant.User).OrderBy(x=>x.date).ToList();
            ViewBag.Events = AllEvents;
            return View("Dashboard");
        }
        [HttpGet]
        [Route("/New")]
        public IActionResult NewActivity(){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                return RedirectToAction("Register", "Home");
            }
            return View("NewActivity");
        }
        [HttpPost]
        [Route("/createactivity")]
        public IActionResult CreateActivity(CreateActivityViewModel model, Event NewEvent){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(ModelState.IsValid){
                if(model.date < DateTime.Today){
                    TempData["dateinvalid"] = "Date must be in future";
                    return View("NewActivity");
                }
                NewEvent.UserId = (int)UserId;
                _context.Events.Add(NewEvent);
                _context.SaveChanges();
                Event newevent = _context.Events.SingleOrDefault(Event=> Event.description == NewEvent.description);
                HttpContext.Session.SetInt32("eventid", (int)newevent.EventId);
                int? EventId = HttpContext.Session.GetInt32("eventid");
                return RedirectToAction("DisplayEvent", new{ActID=(int)EventId});
            }
            return View("NewActivity");
        }

        [HttpGet]
        [Route("/activity/{ActID}")]
        public IActionResult DisplayEvent(int ActID){
            int? UserId = HttpContext.Session.GetInt32("userid");
            if(UserId == null){
                RedirectToAction("Register", "Home");
            }
            ViewBag.UserId = UserId;
            Event Showevent = _context.Events.Include(U=> U.User).SingleOrDefault(Event=> Event.EventId == ActID);
            ViewBag.Eventname = Showevent.title;
            ViewBag.Eventdesc = Showevent.description;
            ViewBag.Participants = _context.Participants.Include(Part=> Part.User).Include(Part=> Part.Event).Where(Part=> Part.EventId == ActID);
            ViewBag.Eventcoordinator = Showevent;
            List<Event> AllEvents = _context.Events.Where(x=> x.EventId == ActID).Include(User=> User.User).Include(Event=>Event.participants).ThenInclude(Participant=>Participant.User).ToList();
            ViewBag.AllEvents = AllEvents;
            int? EventId = HttpContext.Session.GetInt32("eventid");
            ViewBag.EventId = EventId;
            return View("Event");
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int activityid){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Event thisevent = _context.Events.SingleOrDefault(Event=> Event.EventId == activityid);
            _context.Events.Remove(thisevent);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost]
        [Route("join")]
        public IActionResult Join(int activityid){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Participant NewParticipant = new Participant{
                UserId = (int)UserId,
                EventId = activityid,
            };
            _context.Participants.Add(NewParticipant);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpPost]
        [Route("unjoin")]
        public IActionResult Unjoin(int activityid){
            int? UserId = HttpContext.Session.GetInt32("userid");
            Participant Deletepart = _context.Participants.SingleOrDefault(x=> x.UserId == UserId);
            _context.Participants.Remove(Deletepart);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

    }
}