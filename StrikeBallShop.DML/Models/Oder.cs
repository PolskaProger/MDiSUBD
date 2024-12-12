using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.DML.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public string Status { get; set; }
        public DateTime DateOfOrder { get; set; }
    }
}
