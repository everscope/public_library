using Public_Library.LIB.Interfaces;

namespace Public_Library.LIB
{
    public class AttendanceAmount: IAttendanceAmount
    {
        private static int Amount;
        
        public void Decrease()
        {
            if(Amount - 1 < 0)
            {
                Amount = 0;
            }
            else
            {
                Amount -= 1;
            }
        }

        public void Increase()
        {
            Amount += 1;
        }

        public int Get()
        {
            return Amount;
        }

    }
}
