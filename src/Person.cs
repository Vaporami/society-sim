using System.Text.Json;
using System.Text;
using Utilities;
namespace society_sim;

internal class Person
{
    internal class Action
    {}
    
    static private int _lastId = -1;
    static private List<string> _allEmotions = new();
    static private Dictionary<string, Dictionary<string, double>> _emotionsCoeffsDict = new();
    static private List<string> _names = new();
    static private bool _isInitialized = false;

    private int _id;
    private string? _name;
    private int _age;
    private Dictionary<string, int> _emotionsDict;
    
    private List<PersonGroup> _belongsToList;
    public List<PersonGroup> BelongsToList
    {
        get { return this._belongsToList; }
    }
    
    public Person(string name = "UNNAMED", int age = 18)
    {
        if (!_isInitialized)
        {
            Console.WriteLine("Initialize the static fields first.");
            return;
        }
        
        this._id = ++Person._lastId;
        
        this._name = name;
        if(this._name == "UNNAMED")
            this._name = Person._names[Random.Shared.Next(Person._names.Count)];
        
        this._age = age;
        
        this._emotionsDict = new(Person._allEmotions.Count);
        foreach (string emotion in Person._allEmotions)
            this._emotionsDict.Add(emotion, 0);

        this._belongsToList = new();
    }

    static public void InitStaticPerson(string pathToEmotionsJSON, string pathToNamesJSON)
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
            Person._allEmotions.Add(el.GetString() ?? "NOT_EMOTION");

        JsonElement coeffs = root.GetProperty("CoeffsDict");
        foreach (JsonProperty el in coeffs.EnumerateObject())
        {
            Dictionary<string, double> innerDict = new();
            foreach (JsonProperty innerEl in el.Value.EnumerateObject())
                innerDict.Add(innerEl.Name, innerEl.Value.GetDouble());
            _emotionsCoeffsDict.Add(el.Name, innerDict);
        }

        root = JsonUtils.GetRootFromFile(pathToNamesJSON);
        JsonElement names = root.GetProperty("Names");
        foreach(JsonElement el in names.EnumerateArray())
        {
            string? elstr = el.GetString();
            if(!string.IsNullOrEmpty(elstr)) Person._names.Add(elstr);
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
        StringBuilder str = new();
        str.Append("[");

        str.Append($"{this._id} ");
        str.Append($"{this._name} ");
        str.Append($"{this._age} ");
        str.Append($"{this._emotionsDict.Count} ");
        str.Append($"{this._emotionsDict["Joy"]} ");
        str.Append($"{this._emotionsDict["Anger"]} ");
        str.Append($"{this._emotionsDict["Sadness"]}");
        
        str.Append("]");
        return str.ToString();
    }
}
