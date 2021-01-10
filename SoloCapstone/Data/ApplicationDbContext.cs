using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoloCapstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoloCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chef> Chefs { get; set; }
        public DbSet<ChefIngredient> Ingredients { get; set; }
        public DbSet<RecipeFavorites> RecipeFavorites { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
    }
}
