using System.Text.Json;
namespace society_sim;

internal class Person
{
    static private int _lastId = -1;

    static private List<string> _emotions;
    static private Dictionary<string, Dictionary<string, double>> _emotionsCoeffsDict;

    private int _id;
    private string _name;
    private int _age;
    
    public Person(string name = "UNNAMED", int age = 18)
    {
        this._id = ++Person._lastId;
        this._name = name;
        this._age = age;
    }

    static private void InitStaticPerson(string pathToEmotionsJSON)
    {
        Person._emotions = new List<string>();
        Person._emotionsCoeffsDict =
            new Dictionary<string, Dictionary<string, double>>(Person._emotions.Count);
        
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
            _emotions.Add(el.GetString());
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
    }

    public override string ToString()
    {
        return $"[{this._id} {this._name} {this._age}]";
    }
}







