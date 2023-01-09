using d03.Configuration.Sources;
using System;
using System.Collections.Generic;
using System.IO;

namespace d03
{
    class Program
    {
        const string jsonSourcePath = @"C:\Users\Азат\Desktop\azat\Development_books\C#\C_sharp_Day03-0\src\config.json";
        const string yamlSourcePath = @"C:\Users\Азат\Desktop\azat\Development_books\C#\C_sharp_Day03-0\src\config.yml";

        static void Main(string[] args)
        {
            if (true)
            {
                args = new string[] { jsonSourcePath, "1", yamlSourcePath, "2" };
            }
            switch (args.Length)
            {
                case 1:
                    string sourcePath = args[0];
                    if (Path.GetFileName(sourcePath).Contains("json"))
                    {
                        JsonSource jsonSource = CreateSource<JsonSource>(sourcePath);
                        var jsonConfig = new Configuration.Configuration(new List<IConfigurationSource> { jsonSource });
                        Console.WriteLine(jsonConfig);
                    }
                    else
                    {
                        YamlSource yamlSource = CreateSource<YamlSource>(sourcePath);
                        var yamlConfig = new Configuration.Configuration(new List<IConfigurationSource> { yamlSource });
                        Console.WriteLine(yamlConfig);
                    }
                    break;
                case 4:
                    string source1Path = args[0];
                    int source1Priority = Convert.ToInt32(args[1]);
                    string source2Path = args[2];
                    int source2Priority = Convert.ToInt32(args[3]);

                    JsonSource jsonSrc = Path.GetFileName(source1Path).Contains("json")
                        ?
                        CreateSource<JsonSource>(source1Path, source1Priority)
                        :
                        CreateSource<JsonSource>(source2Path, source2Priority);

                    YamlSource yamlSrc = Path.GetFileName(source1Path).Contains("yml")
                        ?
                        CreateSource<YamlSource>(source1Path, source1Priority)
                        :
                        CreateSource<YamlSource>(source2Path, source2Priority);

                    var config = new Configuration.Configuration(new List<IConfigurationSource> { jsonSrc, yamlSrc });
                    Console.WriteLine(config);
                    break;
            }

            Console.ReadLine();
        }

        private static T CreateSource<T>(string sourcePath, int priority = 0) where T : IConfigurationSource
        {
            IConfigurationSource source = null;
            switch (typeof(T).Name)
            {
                case nameof(YamlSource):
                    source = new YamlSource(sourcePath, priority);
                    break;
                case nameof(JsonSource):
                    source = new JsonSource(sourcePath, priority);
                    break;
            }
            source?.FillParamsFromSource();
            return (T)source;
        }
    }
}
