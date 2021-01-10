using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoloCapstone.Models
{
    public class RecipeFavorites
    {
        [Key]
        public int RecipeId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public int Likes { get; set; }
        [ForeignKey("Chef")]
        public int ChefId { get; set; }
        public Chef Chef { get; set; }
    }
}
