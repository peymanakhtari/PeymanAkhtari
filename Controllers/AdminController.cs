using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeymanAkhtari.Data;
using PeymanAkhtari.Data.Repository;
using PeymanAkhtari.Models;
using System.Drawing;
using System.Security.Claims;
using System.Text;

namespace PeymanAkhtari.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "AdminAuth")]
    public class AdminController : Controller
    {
        private MemoryStream ms = new MemoryStream();
        private AppDbContext appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private UnitOfWork db;
        public AdminController(AppDbContext _context, UserManager<ApplicationUser> userManager)
        {
            db = new UnitOfWork(_context);
            appDbContext = _context;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            if (db.SiteSettingRepository.Get(c => c.Key == "" && c.Value == username).Any() && db.SiteSettingRepository.Get(c => c.Key == "" && c.Value == password).Any())
            {
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, username ),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var identity = new ClaimsIdentity(claims, "AdminAuth");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AdminAuth", principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(db.ProjectRepository.Get());

        }
        protected override void Dispose(bool disposing)
        {
            ms.Dispose();
            db.Dispose();
            base.Dispose(disposing);
        }
        public IActionResult EditProject(int id)
        {
            return View(db.ProjectRepository.GetByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProject(Project model, IFormFile img)
        {
            var proj = db.ProjectRepository.GetByID(model.Id);
            if (img != null)
            {
                string img1Name = proj.img ?? Guid.NewGuid().ToString() + ".jpg";
                model.img = Utilities.Utility.uploadImage(img, "images//projects", img1Name);
            }
            else
            {
                model.img = proj.img;
            }
            db.ProjectRepository.Update(model);
            db.Save();
            return RedirectToAction("Index");
        }
        public IActionResult RemoveImg1(int id, int projectId)
        {
            var feature = db.FeactureRepository.GetByID(id);
            Utilities.Utility.deleteFile("images//features" ,feature.img1);
            feature.img1 = null;
            db.FeactureRepository.Update(feature);
            db.Save();
            return RedirectToAction("Features", new { id = projectId });
        }
        public IActionResult RemoveImg2(int id, int projectId)
        {
            var feature = db.FeactureRepository.GetByID(id);
            feature.img2 = null;
            Utilities.Utility.deleteFile("images//features" , feature.img2);
            db.FeactureRepository.Update(feature);
            db.Save();
            return RedirectToAction("Features", new { id = projectId });
        }
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(Project model, IFormFile img)
        {
            model.img = Utilities.Utility.uploadImage(img, "images//projects");
            db.ProjectRepository.Insert(model);
            db.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Features(int id)
        {
            ViewBag.projectId = id;
            return View(db.FeactureRepository.Get(C => C.ProjectId == id).OrderBy(c => c.Sequence));
        }
        public IActionResult CreateFeature(int id)
        {
            ViewBag.projectId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFeature(Feature model, int projectid, IFormFile img1, IFormFile img2)
        {
            if (img1 != null)
                model.img1 = Utilities.Utility.uploadImage(img1, "images//features");
            ms.SetLength(0);
            if (img2 != null)
                model.img2 = Utilities.Utility.uploadImage(img2, "images//features");

            db.FeactureRepository.Insert(model);
            db.Save();
            return RedirectToAction("Features", new { id = projectid });
        }
        public IActionResult EditFeature(int id)
        {
            return View(db.FeactureRepository.GetByID(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFeature(Feature model, int projectid, IFormFile img1, IFormFile img2)
        {
            var feature = db.FeactureRepository.GetByID(model.Id);
            if (img1 != null)
            {
                string img1Name = feature.img1 ?? Guid.NewGuid().ToString() + ".jpg";
                model.img1 = Utilities.Utility.uploadImage(img1, "images//features", img1Name); ;
            }
            else
            {
                model.img1 = db.FeactureRepository.GetByID(model.Id).img1;
            }
            if (img2 != null)
            {
                string img2Name = feature.img2 ?? Guid.NewGuid().ToString() + ".jpg";
                model.img2 = Utilities.Utility.uploadImage(img2, "images//features", img2Name); ;
            }
            else
            {
                model.img2 = db.FeactureRepository.GetByID(model.Id).img2;
            }
            db.FeactureRepository.Update(model);
            db.Save();
            return RedirectToAction("Features", new { id = projectid });
        }

        public IActionResult DeleteFeature(int id, int projectId)
        {
            Utilities.Utility.deleteFile("images//features" , db.FeactureRepository.GetByID(id).img1);
            Utilities.Utility.deleteFile("images//features" , db.FeactureRepository.GetByID(id).img2);
            db.FeactureRepository.Delete(id);
            db.Save();
            return RedirectToAction("Features", new { id = projectId });
        }
     
    }
}
