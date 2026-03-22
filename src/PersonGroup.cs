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

    private void GetNewId()
    {
        this._id = ++PersonGroup._lastId;
    }
    
    public PersonGroup()
    {
        this._group = new();
        GetNewId();
    }
    
    public PersonGroup(List<Person> group)
    {
        this._group = group;
        foreach (Person p in group)
            p.BelongsToList.Add(this);
        GetNewId();
    }

    public PersonGroup(Person[] group)
    {
        this._group = new(group.Length);
        foreach (Person p in group)
            this.AddPerson(p);
        GetNewId();
    }

    public PersonGroup(int capacity, bool createPersons = false)
    {
        if(!createPersons)
        {
            this._group = new(capacity);
            GetNewId();
            return;
        }
        this._group = new(capacity);
        for (int i = 0; i < capacity; i++)
            this.AddPerson(new Person());
        GetNewId();
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

