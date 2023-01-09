
using System.Collections;

namespace d03.Configuration.Sources
{
    interface IConfigurationSource
    {
        string SourcePath { get; }
        int Priority { get; }
        Hashtable Params { get; set; }
        void FillParamsFromSource();
    }
}
