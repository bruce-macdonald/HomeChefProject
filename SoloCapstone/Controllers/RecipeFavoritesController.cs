using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloCapstone.Data;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            var recipes = _context.RecipeFavorites.Where(r => r.ChefId == chef.ChefId);
            return View(recipes);
        }

        // GET: RecipeFavoritesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeFavoritesController/Create
        public async Task<ActionResult> Create(int id)
        {
            try
            {
                
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(APIKeys.header1key, APIKeys.header1value);
                client.DefaultRequestHeaders.Add(APIKeys.header2key, APIKeys.header2value);
                client.DefaultRequestHeaders.Add(APIKeys.header3key, APIKeys.header3value);
                string testURL = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/{id}/information";
                string apiURL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients";
                HttpResponseMessage response = await client.GetAsync(testURL);
                //Recipe recipe = new Recipe();
                OneRecipe recipe = new OneRecipe();
                if (response.IsSuccessStatusCode == true)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    recipe = JsonConvert.DeserializeObject<OneRecipe>(jsonResult);
                }
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                RecipeFavorites recipeFavorite = new RecipeFavorites();
                recipeFavorite.Id = id.ToString();
                recipeFavorite.ChefId = chef.ChefId;
                recipeFavorite.Title = recipe.title;
                recipeFavorite.Image = recipe.image;
                recipeFavorite.ImageType = recipe.imageType;
                recipeFavorite.Likes = recipe.aggregateLikes;
                _context.RecipeFavorites.Add(recipeFavorite);
                _context.SaveChanges();
                return RedirectToAction("Index", "Chef");
            }
            catch
            {
                return RedirectToAction("Chef", "Index");
            }
        }

        // POST: RecipeFavoritesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult> Create(Recipe recipe)
        //{
        //    try
        //    {
        //        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
        //        RecipeFavorites recipeFavorite = new RecipeFavorites();
        //        //recipeFavorite.Item = id.ToString();
        //        recipeFavorite.ChefId = chef.ChefId;
        //        _context.RecipeFavorites.Add(recipeFavorite);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index", "Chef");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Chef", "Index");
        //    }
        //}

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
            return View(recipeFavorites);
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
                return RedirectToAction("Index", "RecipeFavorites");
            }
            catch
            {
                return View();
            }
        }
    }
}
