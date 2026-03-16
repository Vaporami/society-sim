namespace society_sim;

class Program
{
    static void Main(string[] args)
    {
        List<Person> persons = new();
        for(int i = 0; i < 5; i++)
        {
            var p = new Person();
            persons.Add(p);
            Console.WriteLine(p.ToString());
        }
    }
}
