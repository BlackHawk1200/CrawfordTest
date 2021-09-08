using CrawfordTest.Models;
using CrawfordTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Activity = System.Diagnostics.Activity;

namespace CrawfordTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        InterviewContext interviewDbContext = new InterviewContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {

                var loginTest =
                    (from user in interviewDbContext.Users
                     where user.UserName == model.username &&
                             user.Password == model.password
                     select user).FirstOrDefault();

                if (loginTest is null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("DisplayLossTypes", "Home");
                }

                return View();

            }

            return View();

        }

        public IActionResult DisplayLossTypes()
        {

            List<LossTypeVM> lossTypes = (from lossTypeRow in
                           interviewDbContext.LossTypes
                           select new LossTypeVM
                           {
                               LossTypeID = lossTypeRow.LossTypeId,
                               LossTypeCode = lossTypeRow.LossTypeCode,
                               LossTypeDescription = lossTypeRow.LossTypeDescription
                           }).ToList();

            return View(lossTypes);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
