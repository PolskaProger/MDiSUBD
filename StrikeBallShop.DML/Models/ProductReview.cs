using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.DML.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReview { get; set; }
    }
}
