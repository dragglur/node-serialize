using System.IO;
using Xunit;

namespace NodeSerializer.Test
{
    public class SerializerTest
    {
        [Fact]
        public void TestSerialize()
        {
            var serializer = new ListSerializer();
            var randomNode = new Helper();
            FileStream fs = new FileStream("SerializedData.json", FileMode.OpenOrCreate);
            FileNotFoundException ex = new FileNotFoundException();

            var nodes = randomNode.GenerateRandomNode();
            serializer.Serialize(nodes, fs);
            fs.Dispose();

            var exception = Record.Exception(() => File.OpenRead("SerializedData.json"));

            Assert.Null(exception);
        }

        [Fact]
        public void TestDeserialize()
        {
            var fs = File.OpenRead("DeserializedData.json");
            var serializer = new ListSerializer();
            ListNode nodes;

            nodes = serializer.Deserialize(fs);
            fs.Dispose();

            Assert.NotNull(nodes);
        }
    }
}
