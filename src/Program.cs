namespace society_sim;

class Program
{
    static void Main(string[] args)
    {
        Person.InitStaticPerson("src/Emotions.json");
        Person.InitStaticPerson("src/Emotions.json");
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
