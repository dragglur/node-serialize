using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace NodeSerializer
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random;

        public string Data;
    }

    public interface IListSerializer
    {
        void Serialize(ListNode head, Stream s);

        ListNode Deserialize(Stream s);
    }

    public class ListSerializer : IListSerializer
    {
        public void Serialize(ListNode head, Stream s)
        {
            string json;
            ListNode currentRoot = head;

            using (StreamWriter streamWriter = new StreamWriter(s))
            {
                while (currentRoot != null)
                {
                    json = JsonSerializer.Serialize(currentRoot.Data);
                    currentRoot = currentRoot.Next;
                    streamWriter.WriteLine(json);
                }
            }
        }

        public ListNode Deserialize(Stream s)
        {
            string str;
            ListNode head = new ListNode();

            try
            {
                using (StreamReader streamReader = new StreamReader(s))
                {
                    ListNode nextRoot = new ListNode();

                    while ((str = streamReader.ReadLine()) != null)
                    {
                        if (head.Next == null && head.Data == null)
                        {
                            head.Data = JsonSerializer.Deserialize<string>(str);
                        }
                        else if (head.Next == null)
                        {
                            head.Next = nextRoot;
                            nextRoot.Previous = head;
                            nextRoot.Data = JsonSerializer.Deserialize<string>(str);
                        }
                        else
                        {
                            nextRoot.Next = new ListNode();
                            nextRoot.Next.Previous = nextRoot;
                            nextRoot = nextRoot.Next;
                            nextRoot.Data = JsonSerializer.Deserialize<string>(str);
                        }
                    }

                    return head;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
