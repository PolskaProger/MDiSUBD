using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeBallShop.DML.Models
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string passwordhash { get; set; }
        public int roleid { get; set; }
        public DateTime regdate { get; set; }
    }
}
