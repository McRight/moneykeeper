using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrpProject.Models
{
    public class Hystory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoneyManagerItemId { get; set; }
        public string Name { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
    }
}