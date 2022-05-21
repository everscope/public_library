using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB.Interfaces
{
    public interface IAttendanceAmount
    {
        public void Decrease();
        public void Increase();
        public int Get();
    }
}
