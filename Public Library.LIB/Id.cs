using System.Text;

namespace Public_Library.LIB
{
    public static class Id
    {
        private static readonly char[] _charsForId = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();

        public static string Generate()
        {
            Random random = new Random();
            StringBuilder id = new StringBuilder();
            //string id = string.Empty;

            for(int i = 0; i < 15; i++)
            {
                id.Append(_charsForId[random.Next(0, _charsForId.Length)]);
            }

            return id.ToString();
        }
    }
}
