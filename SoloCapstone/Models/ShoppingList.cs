using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoloCapstone.Models
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }

        public string Name { get; set; }

        [ForeignKey("Chef")]
        public int ChefId { get; set; }

        public Chef Chef { get; set; }
    }
}