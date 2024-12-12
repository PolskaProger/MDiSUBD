using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.DML.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NameOfProduct { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public bool InStorage { get; set; }
    }
}
