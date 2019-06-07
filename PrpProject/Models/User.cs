using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrpProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Money { get; set; }
    }
}