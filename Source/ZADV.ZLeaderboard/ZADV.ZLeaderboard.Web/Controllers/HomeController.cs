using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zadv.ZLeaderboard.Domain.IRepositories;

namespace ZADV.ZLeaderboard.Web.Controllers
{
    public class HomeController : Controller
    {
        private IEventRepository _eventRepository;
        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
