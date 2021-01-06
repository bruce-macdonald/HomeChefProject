using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloCapstone.Data;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoloCapstone.Controllers
{
    public class RecipeFavoritesController : Controller
    {
        private ApplicationDbContext _context;
        public RecipeFavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: RecipeFavoritesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecipeFavoritesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeFavoritesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeFavoritesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeFavorites recipeFavorites)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();                
                recipeFavorites.ChefId = chef.ChefId;
                _context.RecipeFavorites.Add(recipeFavorites);
                _context.SaveChanges();
                return RedirectToAction("Chef", "Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeFavoritesController/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: RecipeFavoritesController/Edit/5
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

        // GET: RecipeFavoritesController/Delete/5
        public ActionResult Delete(int id)
        {
            RecipeFavorites recipeFavorites = _context.RecipeFavorites.Where(r => r.RecipeId == id).FirstOrDefault();
            return View();
        }

        // POST: RecipeFavoritesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RecipeFavorites recipeFavorites)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                recipeFavorites.ChefId = chef.ChefId;
                _context.RecipeFavorites.Remove(recipeFavorites);
                _context.SaveChanges();
                return RedirectToAction("Chef", "Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
