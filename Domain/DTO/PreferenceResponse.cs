using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PreferenceResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

    }
}
