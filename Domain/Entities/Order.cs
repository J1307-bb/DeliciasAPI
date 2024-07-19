using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class OrderItem
    {

       [Key]
        public int IdOrderItem { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Meal")]
        public int IdMeal { get; set; }
        public Meal Meal { get; set; }
    }

    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        public int NumMeals { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User User { get; set; }

        public string Date { get; set; }
        public string Hour { get; set; }
        public string Place { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
