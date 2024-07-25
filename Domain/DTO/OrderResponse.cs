using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class OrderItemResponse
    {
        public int IdMeal { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResponse
    {
        public int NumMeals { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public string Place { get; set; }
        public int IdUser { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }  
        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
