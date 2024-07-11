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
    public class OrderItem
    {
        [Key]
        public int IdOrderItem { get; set; }

        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        [ForeignKey("Meal")]
        public int IdMeal { get; set; }
        public Meal Meal { get; set; }

        public int Quantity { get; set; }
    }
}
