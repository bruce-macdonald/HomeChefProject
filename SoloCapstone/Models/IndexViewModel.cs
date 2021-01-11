using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoloCapstone.Models
{
    public class IndexViewModel
    {
        [Key]
        public Chef Chef { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}