using System.Text.Json;
using System.Text;
namespace society_sim;

internal class Person
{
    static private int _lastId = -1;

    static private Dictionary<string, int> _emotions = new();
    static private Dictionary<string, Dictionary<string, double>> _emotionsCoeffsDict = new();
    static private bool _isInitialized = false;

    private int _id;
    private string _name;
    private int _age;
    
    public Person(string name = "UNNAMED", int age = 18)
    {
        this._id = ++Person._lastId;
        this._name = name;
        this._age = age;
    }

    static public void InitStaticPerson(string pathToEmotionsJSON)
    {
        if (Person._isInitialized)
        {
            Console.WriteLine("The \"Person\" class was already initialized!");
            return;
        }
        
        string text = string.Empty;
        using (StreamReader reader = new(pathToEmotionsJSON))
        {
            text = reader.ReadToEnd();
        }
        JsonDocument json = JsonDocument.Parse(text);
        JsonElement root = json.RootElement;

        JsonElement emotions = root.GetProperty("Emotions");
        if (emotions.ValueKind != JsonValueKind.Array)
        {
            Console.WriteLine("\"Emotions\" is not an array!");
            return;
        }
        
        foreach (JsonElement el in emotions.EnumerateArray())
        {
            _emotions.Add(el.GetString() ?? "NO_EMOTION", 0);
        }

        JsonElement coeffs = root.GetProperty("CoeffsDict");
        foreach (JsonProperty el in coeffs.EnumerateObject())
        {
            Dictionary<string, double> innerDict = new();
            foreach (JsonProperty innerEl in el.Value.EnumerateObject())
            {
                innerDict.Add(innerEl.Name, innerEl.Value.GetDouble());
            }
            _emotionsCoeffsDict.Add(el.Name, innerDict);
        }
        Person._isInitialized = true;
    }

    static public string CoeffsToString()
    {
        StringBuilder ret = new();
        foreach (KeyValuePair<string, Dictionary<string, double>> pair in Person._emotionsCoeffsDict)
        {
            ret.Append($"{pair.Key}: {{\n");
            foreach (KeyValuePair<string, double> innerPair in pair.Value)
                ret.Append($"    {innerPair.Key}: {innerPair.Value}\n");
            ret.Append("}\n");
        }
        return ret.ToString();
    }

    public override string ToString()
    {
        return $"[{this._id} {this._name} {this._age} {Person._emotions.Count}]";
    }
}
