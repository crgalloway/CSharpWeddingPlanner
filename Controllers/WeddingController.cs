using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner
{
    public class WeddingController : Controller
    {
        private WeddingPlannerContext _context;
        public WeddingController(WeddingPlannerContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if (activeId != null)
            {
                ViewBag.allWeddings = _context.weddings.Include( w => w.Guests).ThenInclude( g => g.User).ToList();
                ViewBag.activeId = activeId;
                User activeUser = _context.users.Single( u => u.userid == (int)activeId);
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        [Route("newWedding")]
        public IActionResult NewWedding()
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        [Route("addWedding")]
        public IActionResult AddWedding(WeddingViewModel model)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId != null)
            {
                if(ModelState.IsValid)
                {
                    Wedding newWedding = new Wedding{
                        wedder_one = model.wedder_one,
                        wedder_two = model.wedder_two,
                        date = (DateTime)model.date,
                        address = model.address,
                        createdbyid = (int)activeId
                    };
                    _context.Add(newWedding);
                    _context.SaveChanges();
                }
                return View("NewWedding");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        [Route("add/guest/{weddingId}")]
        public IActionResult AddGuest(int weddingId)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId != null)
            {
                GuestList newGuest = new GuestList{
                    eventid = weddingId,
                    guestid = (int)activeId
                };
                _context.Add(newGuest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        [Route("remove/guest/{weddingId}")]
        public IActionResult RemoveGuest(int weddingId)
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId != null)
            {
                GuestList canceledGuest = _context.guestlist.SingleOrDefault( g => g.eventid == weddingId && g.guestid == (int)activeId);
                _context.guestlist.Remove(canceledGuest);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        [Route("remove/wedding/{weddingId}")]
        public IActionResult DeleteWedding(int weddingId) //Remove wedding and associated guest RSVP's
        {
            int? activeId = HttpContext.Session.GetInt32("activeUser");
            if(activeId != null)
            {
                Wedding canceledWedding = _context.weddings.SingleOrDefault( w => w.weddingid == weddingId);
                var invitedGuests = _context.guestlist.Where( g => g.eventid == weddingId );
                foreach(var each in invitedGuests)
                {
                    _context.guestlist.Remove(each);
                }
                _context.weddings.Remove(canceledWedding);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        [Route("wedding/{weddingId}")]
        public IActionResult ViewWedding(int weddingId)
        {
            List<Wedding> viewableWedding = _context.weddings.Where( w => w.weddingid == weddingId).Include( w => w.Guests).ThenInclude( g => g.User).ToList();
            if(viewableWedding.Count == 1)
            {
                ViewBag.wedding = viewableWedding[0];
                return View();
            }
            return RedirectToAction("Dashboard");
        }
    }
}