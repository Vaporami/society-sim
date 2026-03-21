using System.Text.Json;
using System.Text;
using Utilities;
namespace society_sim;

internal class Person
{
    static private int _lastId = -1;
    static private List<string> _allEmotions = new();
    static private Dictionary<string, Dictionary<string, double>> _emotionsCoeffsDict = new();
    static private bool _isInitialized = false;

    private int _id;
    private string? _name;
    private int _age;
    private Dictionary<string, int> _emotionsDict = new();    
    
    public Person(string name = "UNNAMED", int age = 18)
    {
        if (!_isInitialized)
        {
            Console.WriteLine("Initialize the static fields first.");
            return;
        }
        this._id = ++Person._lastId;
        if (name == "UNNAMED")
        {
            
        }
        this._name = name;
        this._age = age;
        foreach (string emotion in Person._allEmotions)
            this._emotionsDict.Add(emotion, 0);            
    }

    static public void InitStaticPerson(string pathToEmotionsJSON)
    {
        if (Person._isInitialized)
        {
            Console.WriteLine("The \"Person\" class was already initialized!");
            return;
        }
        
        JsonElement root = JsonUtils.GetRootFromFile(pathToEmotionsJSON);
        JsonElement emotions = root.GetProperty("Emotions");
        if (emotions.ValueKind != JsonValueKind.Array)
        {
            Console.WriteLine("\"Emotions\" is not an array!");
            return;
        }
        
        foreach (JsonElement el in emotions.EnumerateArray())
        {
            Person._allEmotions.Add(el.GetString() ?? "NOT_EMOTION");
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

    public void AddToEmotion(string emotion, int value)
    {
        this._emotionsDict[emotion] += value;
        foreach (KeyValuePair<string, double> pair in _emotionsCoeffsDict[emotion])
            this._emotionsDict[pair.Key] += (int)(value * pair.Value);
    }

    public override string ToString()
    {
        return $"[{this._id} {this._name} {this._age} {this._emotionsDict.Count}]";
    }
}
