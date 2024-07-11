using Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        public User User { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
