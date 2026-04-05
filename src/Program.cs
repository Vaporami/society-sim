using Utilities;

namespace society_sim;

class Program
{
    static void Init()
    {
        Person.InitStaticPerson("sim-settings/Emotions.json", "sim-settings/Names.json");
    }

    static void Main(string[] args)
    {
        Init();
        int amount = int.Parse(IO.Input("Enter the size of the group: "));
        PersonGroup humanity = new(amount, true);
        foreach(Person p in humanity.Group)
            p.AddToEmotion("Joy", Random.Shared.Next(99));
        IO.Output(humanity.ToString());
    }
}
