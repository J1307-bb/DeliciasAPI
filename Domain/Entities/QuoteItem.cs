using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuoteItem
    {
        [Key]
        public int IdQuoteItem { get; set; }

        [ForeignKey("Quote")]
        public int IdQuote { get; set; }
        [JsonIgnore]
        public Quote Quote { get; set; }

        [ForeignKey("Meal")]
        public int IdMeal { get; set; }
        public Meal Meal { get; set; }

        public int Quantity { get; set; }
    }
}
