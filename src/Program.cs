namespace society_sim;

class Program
{
    static void Init()
    {
        Person.InitStaticPerson("src/Emotions.json");
    }
    static void Main(string[] args)
    {
        Program.Init();
        
        List<Person> persons = new();
        for(int i = 0; i < 5; i++)
        {
            var p = new Person();
            persons.Add(p);
            Console.WriteLine(p.ToString());
        }
        Console.WriteLine(Person.CoeffsToString());
    }
}
