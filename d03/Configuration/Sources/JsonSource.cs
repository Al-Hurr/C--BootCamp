using System;
using System.Collections;
using System.IO;
using System.Text.Json;

namespace d03.Configuration.Sources
{
    class JsonSource : IConfigurationSource
    {
        public Hashtable Params { get; set; }

        public string SourcePath { get; private set; }

        public int Priority { get; private set; }

        public JsonSource(string sourcePath, int priority = 0)
        {
            SourcePath = sourcePath;
            Priority = priority;
        }

        public void FillParamsFromSource()
        {
            try
            {
                string json = File.ReadAllText(SourcePath);
                var items = JsonSerializer.Deserialize<Hashtable>(json);
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
