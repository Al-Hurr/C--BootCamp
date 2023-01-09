
using d03.Configuration.Sources;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace d03.Configuration
{
    class Configuration
    {
        public Hashtable Params { get; private set; }

        public Configuration(List<IConfigurationSource> configurationSourceList)
        {
            Params = new();
            foreach(var cfgSource in configurationSourceList.OrderByDescending(x => x.Priority))
            {
                foreach (DictionaryEntry param in cfgSource.Params)
                {
                    if (Params.ContainsKey(param.Key))
                    {
                        continue;
                    }

                    Params[param.Key] = param.Value;
                }
            }
        }

        public override string ToString()
        {
            if(Params?.Count == 0)
            {
                return "Parameters list is empty";
            }
            StringBuilder sb = new ();
            sb.AppendLine("Configuration");
            foreach (DictionaryEntry param in Params)
            {
                sb.AppendLine($"{param.Key}: {param.Value}");
            }

            return sb.ToString();
        }
    }
}
