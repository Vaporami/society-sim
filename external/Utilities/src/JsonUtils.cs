using System.Text.Json;
using System.Text;

namespace Utilities;

public sealed class JsonUtils
{
    static public JsonDocument GetDocFromFile(string path)
    {
        string text = string.Empty;
        using (StreamReader reader = new(path))
        {
            text = reader.ReadToEnd();
        }
        return JsonDocument.Parse(text);
    }

    static public JsonElement GetRootFromFile(string path)
    {
        return JsonUtils.GetDocFromFile(path).RootElement;
    }
} // testing
