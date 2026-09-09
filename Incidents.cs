namespace SZZVP;

public abstract class Incident
{   
    private readonly List<IIncidentObserver> observers = new();
    public int Id { get; }
    public IncidentType Type { get; }

    public IncidentPriority Priority { get; }

    public string Description { get; }

    public IncidentStatus Status { get; private set; }

    //public User LastUpdatedBy { get; set; }

    public Incident(int id, IncidentType type, IncidentPriority priority, string description)
    {
        Id = id;
        Type = type;
        Priority = priority;
        Description = description;
        Status = IncidentStatus.New;
    }

    public void ChangeStatus(IncidentStatus newStatus)
    {
        Status = newStatus;
        NotifyObservers();
        //LastUpdatedBy = user;
    }
    public void AddObserver(IIncidentObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IIncidentObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(this);
        }
    }

}


public class HardwareIncident : Incident
{
    public HardwareIncident(int id, IncidentPriority priority, string description)
        : base(id, IncidentType.Hardware, priority, description)
    {
    }
}

public class SoftwareIncident : Incident
{
    public SoftwareIncident(int id, IncidentPriority priority, string description)
        : base(id, IncidentType.Software, priority, description)
    {
    }
}

public class NetworkIncident : Incident
{
    public NetworkIncident(int id, IncidentPriority priority, string description)
        : base(id, IncidentType.Network, priority, description)
    {
    }
}

public class SecurityIncident : Incident
{
    public SecurityIncident(int id, IncidentPriority priority, string description)
        : base(id, IncidentType.Security, priority, description)
    {
    }
}
