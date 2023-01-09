using System;
using System.Collections;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace d03.Configuration.Sources
{
    class YamlSource : IConfigurationSource
    {
        public string SourcePath { get; private set; }
        private IDeserializer _deserializer;

        public Hashtable Params { get; set; }

        public int Priority { get; private set; }

        public YamlSource(string yamlSource, int priority = 0)
        {
            SourcePath = yamlSource;
            Priority = priority;
            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        public void FillParamsFromSource()
        {
            try
            {
                string yaml = File.ReadAllText(SourcePath);
                var items = _deserializer.Deserialize<Hashtable>(yaml);
                if(items.Count > 0)
                {
                    Params = items;
                }
            }
            catch
            {
                Console.WriteLine("Invalid data. Check your input and try again.");
            }
        }
    }
}
