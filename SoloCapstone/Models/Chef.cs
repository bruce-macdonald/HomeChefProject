using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoloCapstone.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}