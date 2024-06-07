using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Meal
    {
        [Key]
        public int IdMeal { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string UrlImage { get; set; }

        [ForeignKey("Category")]
        public int IdCategory { get; set; }
        public Category Category { get; set; }
    }
}
