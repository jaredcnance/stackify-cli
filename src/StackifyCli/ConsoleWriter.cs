using System;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace StackifyCli
{
    public interface IConsoleWriter
    {
        void Write(object content);
        void WritePretty(object content);
    }

    public class ConsoleWriter : IConsoleWriter
    {
        public void Write(object message) => Console.WriteLine(JsonConvert.SerializeObject(message));

        public void WritePretty(object content)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(content);
            Console.WriteLine(yaml);
        }
    }
}