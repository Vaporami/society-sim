namespace society_sim;

internal class Person
{
    static private int _lastId = -1;

    private int _id;
    private string _name;
    private int _age;
    
    public Person(string name = "UNNAMED", int age = 18)
    {
        this._id = ++Person._lastId;
        this._name = name;
        this._age = age;
        this._emotions = new Dictionary<EmotionType, int>();

        foreach (EmotionType v in Enum.GetValues<EmotionType>())
            this._emotions.Add(v, 1);
    }

    public override string ToString()
    {
        return $"[{this._id} {this._name} {this._age} {this._emotions[EmotionType.Sadness]}]";
    }
}
