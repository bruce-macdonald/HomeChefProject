using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoloCapstone.Models
{
    
    public class IndexViewModel
    {
        [Key]
        public Chef Chef { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
