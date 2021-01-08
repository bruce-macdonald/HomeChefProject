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
    public class ChefController : Controller
    {
        private ApplicationDbContext _context;
        public ChefController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ChefController
        public async Task<ActionResult> IndexAsync()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chef = _context.Chefs.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            if (chef ==  null)
            {
                return RedirectToAction("Create");
            }
            
            //IndexViewModel indexViewModel = new IndexViewModel();
            //indexViewModel.Chef = chef;
            ////indexViewModel.Recipes i want to set it equal to this recipes below
            var results = _context.Ingredients.Where(i => i.ChefId == chef.ChefId).ToList();
            var recipes = await GetRecipes(results);
            //if (results.Count == 0)
            //{
            //    return RedirectToAction("AddIngredient");
            //}

            return View(recipes);
        }
        private async Task<List<Recipe>> GetRecipes(List<Ingredient> ingredients)
        {
            //header1.key, header1.value
            string foodParameters = SetParameters(ingredients);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(APIKeys.header1key, APIKeys.header1value);
            client.DefaultRequestHeaders.Add(APIKeys.header2key, APIKeys.header2value);
            client.DefaultRequestHeaders.Add(APIKeys.header3key, APIKeys.header3value);
            string testURL = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?ingredients={foodParameters}";
            string apiURL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients";
            HttpResponseMessage response = await client.GetAsync(testURL);
            //Recipe recipe = new Recipe();
            List<Recipe> recipes = new List<Recipe>();
            if (response.IsSuccessStatusCode == true)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResult);
            }
            return recipes;
        }
        private string SetParameters(List<Ingredient> ingredients)
        {
            string returnURL = "";
            if (ingredients.Count != 0)
            {
                returnURL = ingredients[0].Name;
                for (int i = 1; i < ingredients.Count; i++)
                {
                    returnURL += "%2C" + ingredients[i].Name;
                }
            }
            else
            {
                returnURL="apples";
            }
            return returnURL;
        }
        //GET: ChefController/AddIngredient/IngredientName hopefully this works :|
        //public ActionResult AddIngredient(string? ingredientName)
        //{
        //    var newIngredient = RouteData.Values.Values.ToList();
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var chef = _context.Chefs.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    Ingredient ingredient = new Ingredient();
        //    ingredient.Name = (string)newIngredient[2];
        //    ingredient.ChefId = chef.ChefId;
        //    //_context.Add(ingredient);
        //    //_context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // GET: ChefController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChefController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChefController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chef chef)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                chef.IdentityUserId = userId;
                _context.Chefs.Add(chef);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        private async Task<Recipe> GetRecipeBySingleIngredient(Ingredient ingredient)
        {
            //header1.key, header1.value
            List<Ingredient> ingredientToSearchWith = new List<Ingredient>();
            ingredientToSearchWith.Add(ingredient);
            string foodParameters = SetParameters(ingredientToSearchWith);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(APIKeys.header1key, APIKeys.header1value);
            client.DefaultRequestHeaders.Add(APIKeys.header2key, APIKeys.header2value);
            client.DefaultRequestHeaders.Add(APIKeys.header3key, APIKeys.header3value);
            string testURL = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients?ingredients={foodParameters}";
            string apiURL = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/findByIngredients";
            HttpResponseMessage response = await client.GetAsync(testURL);
            Recipe recipe = new Recipe();
            //List<Recipe> recipes = new List<Recipe>();
            if (response.IsSuccessStatusCode == true)
            {
                string jsonResult = await response.Content.ReadAsStringAsync();
                recipe = JsonConvert.DeserializeObject<Recipe>(jsonResult);
            }
            return recipe;
        }
        // GET: ChefController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChefController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChefController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChefController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
