﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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