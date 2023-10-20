using System;

[Serializable]
public class SchoolTopic : IComparable<SchoolTopic>
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;

    public int CompareTo(SchoolTopic other)
    {
        if (other == null)
        {
            return 1;
        }
        string domainThis = domain;
        string domainOther = other.domain;
        if (domainThis.StartsWith("The"))
        {
            domainThis = domainThis.Remove(0, 4);
        }
        if (domainOther.StartsWith("The"))
        {
            domainOther = domainOther.Remove(0, 4);
        }
        int domainTest = string.Compare(domainThis, domainOther);
        int clusterTest = domainTest != 0 ? domainTest : string.Compare(cluster, other.cluster);
        int standardIdTest = clusterTest != 0 ? clusterTest : string.Compare(standardid, other.standardid);
        return standardIdTest;
    }
}
