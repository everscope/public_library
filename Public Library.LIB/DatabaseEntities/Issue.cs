using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB
{
    public class Issue
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string PatronId { get; set; }
        public Patron Patron { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime ReturnDateTime { get; set; }
        public bool isClosed { get; set; }
        public bool isExpired { get; set; }
        public int AllowedDaysAmount { get; set; } = 14;
    }
}
