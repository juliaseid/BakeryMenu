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
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly SweetCakesContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public TreatsController(UserManager<ApplicationUser> userManager, SweetCakesContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.Flavors)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddTag(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).Collection(t => t.Flavors).IsModified = true;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // [HttpGet("/search")]
    // public ActionResult Search(string search, string searchParam)
    // {

    //   if (!string.IsNullOrEmpty(search))
    //   {
    //     if (searchParam == "Tags")
    //     {
    //       var thisTag = _db.Tags
    //       .Include(tag => tag.Recipes)
    //       .ThenInclude(join => join.Recipe)
    //       .FirstOrDefault(tag => tag.Word == search);
    //       return View(thisTag.Recipes);

    //     }
    //     else if (searchParam == "Name")
    //     {
    //       var model = from r in _db.Recipes select r;
    //       model = model.Where(r => r.Name.Contains(search));
    //       List<Recipe> matches = model.ToList();
    //       return View(matches);
    //     }
    //     else
    //     {
    //       var model = from m in _db.Recipes select m;
    //       List<Recipe> allRecipes = new List<Recipe> { };
    //       allRecipes = model.ToList();
    //       return View(allRecipes);
    //     }
    //   }
    //   else
    //   {
    //     return RedirectToAction("Index");
    //   }
    // }
    // [HttpGet("/sort")]
    // public ActionResult Sort(string sortParam)
    // {
    //   List<Recipe> model = _db.Recipes.ToList();
    //   if (sortParam == "Rating")
    //   {
    //     model = model.OrderByDescending(r => r.Rating).ToList();
    //   }
    //   else
    //   {
    //     model = model.OrderBy(r => r.Minutes).ToList();
    //   }
    //   return View(model);
    // }



  }
}