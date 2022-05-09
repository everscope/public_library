using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB
{
    public static class Id
    {
        private static readonly char[] _charsForId = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();

        public static string Generate()
        {
            Random random = new Random();
            string id = string.Empty;

            for(int i = 0; i < 15; i++)
            {
                id+= _charsForId[random.Next(0, _charsForId.Length)];
            }

            return id;
        }
    }
}
