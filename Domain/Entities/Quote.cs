using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quote
    {
        [Key]
        public int IdQuote { get; set; }
        public string Place { get; set; }
        public int NumMeals { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User User { get; set; }

        [ForeignKey("Meal")]
        public int IdMeal { get; set; }
        public Meal Meal { get; set; }
    }
}
