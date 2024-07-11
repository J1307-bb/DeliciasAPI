using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{

    public class OrderResponse
    {
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        public int IdUser { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }

    public class OrderItemResponse
    {
        public int IdMeal { get; set; }
        public int Quantity { get; set; }
    }
}
