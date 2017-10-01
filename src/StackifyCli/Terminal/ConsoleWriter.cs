using System;
using System.Threading;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace StackifyCli.Terminal
{
    public interface IConsoleWriter
    {
        void Write(object content);
        void WritePretty(object content);
    }

    public class ConsoleWriter : IConsoleWriter
    {
        public void Write(object message)
        {
            ConsoleSpiner.StopLoading();
            Console.WriteLine(JsonConvert.SerializeObject(message,
            new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        public void WritePretty(object content)
        {
            ConsoleSpiner.StopLoading();
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(content);
            Console.WriteLine(yaml);
        }
    }
}