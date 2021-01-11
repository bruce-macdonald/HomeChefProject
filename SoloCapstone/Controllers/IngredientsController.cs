using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloCapstone.Data;
using SoloCapstone.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SoloCapstone.Controllers
{
    public class IngredientsController : Controller
    {
        private ApplicationDbContext _context;

        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IngredientsController
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            List<ChefIngredient> ingredients = new List<ChefIngredient>();
            ingredients = _context.Ingredients.Where(r => r.ChefId == chef.ChefId).ToList();
            return View(ingredients);
        }

        // GET: IngredientsController/Details/5
        public ActionResult Details(int IngredientId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ChefIngredient> ingredients = new List<ChefIngredient>();
            ingredients = _context.Ingredients.Where(s => s.IngredientId.ToString() == userId).ToList();
            return View(ingredients);
        }

        // GET: IngredientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChefIngredient ingredient)
        {
            try
            {
                //ingredient.ChefId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //set the ingredient chefId equal to the chef's primary key
                //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                if (chef == null)
                {
                    return RedirectToAction("Create", "Chef");
                }
                ingredient.ChefId = chef.ChefId;
                _context.Ingredients.Add(ingredient);
                _context.SaveChanges();
                return RedirectToAction("Index", "Chef");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientsController/Delete/5
        public ActionResult Delete(int id)
        {
            var ingredient = _context.Ingredients.Where(i => i.IngredientId == id).FirstOrDefault();
            return View(ingredient);
        }

        // POST: IngredientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ChefIngredient ingredient)
        {
            try
            {
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();
                return RedirectToAction("Index", "Ingredients");
            }
            catch
            {
                return View();
            }
        }
    }
}