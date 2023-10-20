using System;
using System.Collections.Generic;
/// <summary>
/// To hold the subject grades info 
/// </summary>
[Serializable]
public class Subject
{
    public string Name;
    public Dictionary<string, Grade> Grades = new Dictionary<string, Grade>();

    public Subject(string name)
    {
        this.Name = name;
    }
}
