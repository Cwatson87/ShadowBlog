using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShadowBlog.Data;
using ShadowBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShadowBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailService;
        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IEmailSender emailService, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? page)
        {
            //page might be null...if it is ill update to be 1
            var pageNumber = page ?? 1;
            var pageSize = 5;

            var blogs = await _dbContext.Blogs.OrderBy(b => b.Created).ToPagedListAsync(pageNumber, pageSize);
            return View(blogs);
        }

        public IActionResult ContactMe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactMe(string name, string email, string phone, string message)
        {
            var subject = $"{name} has reached out to you from the ShadowBlog Application";

            var body = $"{message}.<br/><br/>{name} can be called at {phone} or emailed at {email} if follow up is required.";

            var myEmail = _configuration["SmtpSettings:Email"];
            await _emailService.SendEmailAsync(myEmail, subject, body);
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}