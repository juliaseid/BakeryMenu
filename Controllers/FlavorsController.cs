using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using SweetCakes.Models;

namespace SweetCakes.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly SweetCakesContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(UserManager<ApplicationUser> userManager, SweetCakesContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flav)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flav.User = currentUser;
      _db.Flavors.Add(flav);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFlav = _db.Flavors
          .Include(flav => flav.Treats)
          .ThenInclude(join => join.Treat)
          .FirstOrDefault(flav => flav.FlavorId == id);
      return View(thisFlav);
    }

    public ActionResult Edit(int id)
    {
      var thisFlav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      return View(thisFlav);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(Flavor flav)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flav.User = currentUser;
      _db.Entry(flav).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlav = _db.Flavors.FirstOrDefault(flav => flav.FlavorId == id);
      return View(thisFlav);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult> DeleteConfirmed(int id, Flavor flav)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flav.User = currentUser;
      var thisFlav = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      _db.Flavors.Remove(thisFlav);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}