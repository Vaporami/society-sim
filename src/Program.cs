using Utilities;

namespace society_sim;

class Program
{
    static void Init()
    {
        Person.InitStaticPerson("src/jsons/Emotions.json", "src/jsons/Names.json");
    }

    static void PrintPersonGroup(List<Person> persons)
    {
        foreach(Person p in persons)
            Console.WriteLine(p.ToString());
    }
    
    static void Main(string[] args)
    {
        Program.Init();
        int amount = int.Parse(IO.Input("Enter the size of the group: "));
        List<Person> persons = new(amount);
        for(int i = 0; i < amount; i++)
        {
            var p = new Person();
            persons.Add(p);
        }
        foreach(Person p in persons)
            p.AddToEmotion("Joy", Random.Shared.Next(99));
        PrintPersonGroup(persons);
    }
}
