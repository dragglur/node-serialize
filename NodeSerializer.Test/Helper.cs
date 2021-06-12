using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSerializer.Test
{
    public class Helper
    {
        public ListNode GenerateRandomNode()
        {
            int x = 0;
            var list = new ListNode();
            ListNode nextRoot = new ListNode();

            while (x < 10)
            {
                if (list.Next == null && list.Data == null)
                {
                    list.Data = GenerateRandomString();
                }
                else if (list.Next == null)
                {
                    list.Next = nextRoot;
                    nextRoot.Previous = list;
                    nextRoot.Data = GenerateRandomString();
                }
                else
                {
                    nextRoot.Next = new ListNode();
                    nextRoot.Next.Previous = nextRoot;
                    nextRoot = nextRoot.Next;
                    nextRoot.Data = GenerateRandomString();
                }

                x++;
            }

            return list;
        }

        private static string GenerateRandomString()
        {
            Random random = new Random();
            const string symbols = "abcdefg";
            return new string(Enumerable.Repeat(symbols, 5).Select(x => x[random.Next(x.Length)]).ToArray());
        }
    }
}
