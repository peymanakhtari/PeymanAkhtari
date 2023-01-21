using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PeymanAkhtari.Data;
using PeymanAkhtari.Data.Repository;
using PeymanAkhtari.Models;
using PeymanAkhtari.Utilities;
using System.Diagnostics;
using System.Globalization;

namespace PeymanAkhtari.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UnitOfWork db;
        private MemoryStream ms = new MemoryStream();


        public HomeController(ILogger<HomeController> logger, AppDbContext _context)
        {
            _logger = logger;
            db = new UnitOfWork(_context);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public IActionResult Index()
        {
            //Email.SendEmailAsync("peyman.akhtari94@gmail.com", "sub", "message", false);
            var lang = CultureInfo.CurrentCulture.Name;
            ViewBag.lang = lang;
            ViewBag.Websevice = db.ContentRepository.Get(c => c.Key == "web service" && c.lang == lang).FirstOrDefault().Value;
            if (lang == "fa")
            {
                return View(db.ProjectRepository.Get(c => c.Language == 1).OrderBy(c=>c.Sequence));
            }
            else
            {
                return View(db.ProjectRepository.Get(c => c.Language == 2).OrderBy(c => c.Sequence));
            }

        }
        public IActionResult Project(int id)
        {
            var proj = db.ProjectRepository.GetByID(id);
            var projList = db.ProjectRepository.Get(c => c.linkHref == proj.linkHref).ToList();
            var persian = projList.Where(c => c.Language == 1).FirstOrDefault();
            var english = projList.Where(c => c.Language == 2).FirstOrDefault();
            if (CultureInfo.CurrentCulture.Name == "fa")
            {
                ViewBag.ProjectName = persian.Title;
                return View(db.FeactureRepository.Get(c => c.ProjectId == persian.Id).OrderBy(c=>c.Sequence));
            }
            else
            {
                ViewBag.ProjectName = english.Title;
                return View(db.FeactureRepository.Get(c => c.ProjectId == english.Id).OrderBy(c => c.Sequence));
            }
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
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return Redirect(returnUrl);
            // return LocalRedirect(returnUrl);
        }
        //[Route("abc/bcd/signin-google/{Title?}/{Text1?}/{Text2?}")]
        // [Route("{Title?}")]
        public IActionResult test(Feature feature)
        {
            ViewBag.val = feature.Title;
            return View();
        }
        public IActionResult Sendmessage(string username,string message)
        {
            Email.SendEmailAsync("peyman.firelance@gmail.com", username, message);
            return Json(username);
        }
        public IActionResult Skills()
        {
            return View();
        }
        public IActionResult AboutMe()
        {
            return View();
        }
        public IActionResult ContactmePage()
        {
            return View();
        }
    }
}