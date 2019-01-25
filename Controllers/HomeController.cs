using System;
using System.Collections.Generic;
using System.Linq;
using BeltExam.Data;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers {
    public class HomeController : Controller {
        public DataContext dbContext;
        public HomeController (DataContext context) {
            dbContext = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            if (CheckLogin ()) {
                return RedirectToAction ("Dashboard");
            } else {
                return RedirectToAction ("Access", "UserAccess");
            }
        }
        public bool CheckLogin () {
            //Additional tests here, as needed
            if (HttpContext.Session.GetInt32 ("LoggedInUserID") != null) {
                return true;
            } else {
                return false;
            }
        }

        [HttpGet ("Home")]
        public IActionResult Dashboard () {
            if (CheckLogin ()) {
                List<Activity> allActs = dbContext.Activities
                    .OrderBy (a => a.StartDate)
                    .Include (a => a.Coordinator)
                    .Include (a => a.Participations)
                    .ThenInclude (p => p.User)
                    .ToList ();
                ViewBag.allActs = allActs;
                return View ();
            } else {
                return RedirectToAction ("Access", "UserAccess");
            }
        }

        [HttpGet ("activity/{actId}")]
        public IActionResult ActivityDetails (int actId) {
            if (CheckLogin ()) {
                Activity curAct = dbContext.Activities
                    .Include (a => a.Coordinator)
                    .Include (a => a.Participations)
                    .ThenInclude (p => p.User)
                    .FirstOrDefault (a => a.ActivityId == actId);
                ViewBag.act = curAct;
                return View ();
            }
            return RedirectToAction ("Access", "UserAccess");
        }

        [HttpGet ("New")]
        public IActionResult ActivityBuilder () {
            if (CheckLogin ()) {
                return View ("ActivityAdd");
            }
            return RedirectToAction ("Access", "UserAccess");
        }

        [HttpPost ("New")]
        public IActionResult ActivityAdd (Activity newAct) {
            if (ModelState.IsValid) {
                if (newAct.Duration <= 0) {
                    ModelState.AddModelError ("Duration", "Duration can't turn back time");
                    return View ();
                } else {
                    int minute = newAct.StartTime.Minute;
                    int hour = newAct.StartTime.Hour;
                    newAct.StartDate = newAct.StartDate.AddHours (hour);
                    newAct.StartDate = newAct.StartDate.AddMinutes (minute);
                    newAct.CoordinatorId = (int) HttpContext.Session.GetInt32 ("LoggedInUserID");
                    dbContext.Add (newAct);
                    dbContext.SaveChanges ();
                    return RedirectToAction ("ActivityDetails", new { actId = newAct.ActivityId });
                }
            }
            return View ();
        }

        [HttpGet ("leave/{pId}")]
        public IActionResult LeaveAct (int pId) {
            Participation part = dbContext.Participations.FirstOrDefault (
                p => p.ParticipationId == pId
            );
            dbContext.Remove (part);
            dbContext.SaveChanges ();

            return RedirectToAction ("Dashboard");
        }

        [HttpGet ("join/{aId}")]
        public IActionResult JoinAct (int aId) {
            int userId = (int) HttpContext.Session.GetInt32 ("LoggedInUserID");

            Activity act = dbContext.Activities.FirstOrDefault (a => a.ActivityId == aId);

            User user = dbContext.Users
                .Include (u => u.Participations)
                .ThenInclude (p => p.Activity)
                .FirstOrDefault (u => u.UserId == userId);

            DateTime actStart = act.StartDate;
            DateTime actEmd = act.StartDate;
            if (act.TimeMeasure == "days") {
                actEmd = actEmd.AddDays (act.Duration);
            } else if (act.TimeMeasure == "hours") {
                actEmd = actEmd.AddHours (act.Duration);
            } else {
                actEmd = actEmd.AddMinutes (act.Duration);

            }

            bool overlap = false;
            foreach (Participation datespan in user.Participations) {
                DateTime aStart = datespan.Activity.StartDate;
                DateTime aEnd = datespan.Activity.StartDate;
                if (datespan.Activity.TimeMeasure == "days") {
                    aEnd = aEnd.AddDays (datespan.Activity.Duration);
                } else if (datespan.Activity.TimeMeasure == "hours") {
                    aEnd = aEnd.AddHours (datespan.Activity.Duration);
                } else {
                    aEnd = aEnd.AddMinutes (datespan.Activity.Duration);
                }
                if ((actStart < aEnd) && (actEmd > aStart)) {
                    overlap = true;
                    break;
                }
            }
            if (!overlap) {
                Participation part = new Participation ();
                part.ActivityId = aId;
                part.UserId = userId;
                dbContext.Add (part);
                dbContext.SaveChanges ();
            }

            return RedirectToAction ("Dashboard");

        }

        [HttpGet ("deleteactivity/{aId}")]
        public IActionResult DeleteAct (int aId) {
            Activity act = dbContext.Activities.FirstOrDefault (a => a.ActivityId == aId);
            dbContext.Remove (act);
            dbContext.SaveChanges ();
            return RedirectToAction ("Dashboard");
        }
    }

}