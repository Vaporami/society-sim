using System.Text.Json;
namespace society_sim;

internal class Person
{
    static private int _lastId = -1;
    static List<string> _emotions;
    static private Dictionary<string, Dictionary<string, double>> _emotionsCoeffsDict;

    private int _id;
    private string _name;
    private int _age;

    static private void InitStaticPerson(string pathToEmotionsJSON)
    {
        string text = string.Empty;
        using (StreamReader reader = new(pathToEmotionsJSON))
            text = reader.ReadToEnd();
        JsonDocument json = JsonDocument.Parse(text);
        JsonElement root = JsonDocument.RootElement;

        JsonElement emotions = root.GetProperty("Emotions");
        if (emotions.ValueKind != JsonValueKind.Array)
        {
            Console.WriteLine("\"Emotions\" is not an array!");
            return;
        }
        
        foreach (string val in emotions.Item)
        {
            _emotions.Add(val);
        }

        JsonElement coeffs = root.GetProperty("CoeffsDict");
        foreach (string emotion in Person._emotions)
        {
            Dictionary<string, double> innerDict = new();
            Person._emotionsCoeffsDict.Add(emotion, innerDict)
        }
    }
        
    public Person(string name = "UNNAMED", int age = 18)
    {
        this._id = ++Person._lastId;
        this._name = name;
        this._age = age;
    }

    public override string ToString()
    {
        return $"[{this._id} {this._name} {this._age}]";
    }
}

