using Utilities;
using System.Text;

namespace society_sim;

internal class PersonGroup
{
    static private int _lastId = -1;

    private int _id;
    private List<Person> _group;
    public List<Person> Group
    {
        get { return this._group; }
        set { this._group = value; }
    }
    
    public PersonGroup()
    {
        this._group = new();
        this._id = ++PersonGroup._lastId;
    }
    
    public PersonGroup(List<Person> group)
    {
        this._group = group;
        foreach (Person p in group)
            p.BelongsToList.Add(this);
        this._id = ++PersonGroup._lastId;
    }

    public PersonGroup(Person[] group)
    {
        this._group = new(group.Length);
        foreach (Person p in group)
            this.AddPerson(p);
        this._id = ++PersonGroup._lastId;
    }

    public PersonGroup(int capacity, bool createPersons = false)
    {
        if(!createPersons)
        {
            this._group = new(capacity);
            this._id = ++PersonGroup._lastId;
            return;
        }
        this._group = new(capacity);
        for (int i = 0; i < capacity; i++)
            this.AddPerson(new Person());
        this._id = ++PersonGroup._lastId;
    }

    public void AddPerson(Person p)
    {
        this._group.Add(p);
        p.BelongsToList.Add(this);
    }

    public override string ToString()
    {
        StringBuilder ret = new(_group.Count * 10);
        foreach (Person p in this._group)
            ret.Append($"{p.ToString()}\n");
        return ret.ToString();
    }
}

