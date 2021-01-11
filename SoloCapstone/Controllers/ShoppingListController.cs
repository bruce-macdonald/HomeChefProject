using Microsoft.AspNetCore.Mvc;
using SoloCapstone.Data;
using SoloCapstone.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SoloCapstone.Controllers
{
    public class ShoppingListController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingListController
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            List<ShoppingList> shoppingLists = new List<ShoppingList>();
            shoppingLists = _context.ShoppingList.Where(r => r.ChefId == chef.ChefId).ToList();
            return View(shoppingLists);
        }

        // GET: ShoppingListController/Details/5
        public ActionResult Details(int ShoppingListId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ShoppingList> shoppingList = new List<ShoppingList>();
            shoppingList = _context.ShoppingList.Where(s => s.ShoppingListId.ToString() == userId).ToList();
            return View(shoppingList);
        }

        // GET: ShoppingListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingListController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingList shoppingListItem)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Chef chef = _context.Chefs.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                shoppingListItem.ChefId = chef.ChefId;
                _context.ShoppingList.Add(shoppingListItem);
                _context.SaveChanges();
                return RedirectToAction("Index", "ShoppingList");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingListController/Edit/5
        public ActionResult Edit(int id)
        {
            ShoppingList shoppingList = _context.ShoppingList.Where(s => s.ShoppingListId == id).FirstOrDefault();
            return View();
        }

        // POST: ShoppingListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ShoppingListid, ShoppingList shoppingList)
        {
            try
            {
                _context.ShoppingList.Update(shoppingList);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingListController/Delete/5
        public ActionResult Delete(int id)
        {
            var shopItemToRemove = _context.ShoppingList.Where(i => i.ShoppingListId == id).FirstOrDefault();
            return View(shopItemToRemove);
        }

        // POST: ShoppingListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShoppingList shoppingList)
        {
            try
            {
                shoppingList = _context.ShoppingList.Where(s => s.ShoppingListId == shoppingList.ShoppingListId).FirstOrDefault();
                _context.Remove(shoppingList);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}