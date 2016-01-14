using System;
using Microsoft.AspNet.Mvc;
using OnlineLearningResoursesApp.ViewModels;
using OnlineLearningResoursesApp.Models;
using System.Linq;
using Microsoft.AspNet.Authorization;

namespace OnlineLearningResoursesApp.Controllers.Web
{
    public class AppController : Controller
    {
        private ILearningRepository _repository;

        public AppController(ILearningRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Plans()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Courses()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            return View();
        }
    }
}
