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
        public Book Book { get; set; }
        public Patron Patron { get; set; }
    }
}
