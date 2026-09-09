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
        if (Status == newStatus)
        {
            //Console.WriteLine($"------Incident #{Id} již má status {newStatus}");
            return;
        }
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

    public override string ToString()
    {
        return $"Incident #{Id} - Typ: {Type}, Priorita: {Priority}, Popis: {Description}, Status: {Status}";
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


public class OtherIncident : Incident
{
    public OtherIncident(int id, IncidentPriority priority, string description)
        : base(id, IncidentType.Other, priority, description)
    {
    }
}


public abstract class IncidentResolver
{
    public void Resolve(Incident incident)
    {
        Accept(incident);
        Analyze(incident);
        Fix(incident);
        Test(incident);
        Close(incident);
    }

    protected virtual void Accept(Incident incident)
    {
        Console.WriteLine($"Incident #{incident.Id} přijat.");
        incident.ChangeStatus(IncidentStatus.InProgress);
    }

    protected abstract void Analyze(Incident incident);

    protected abstract void Fix(Incident incident);

    protected virtual void Test(Incident incident)
    {
        Console.WriteLine($"Test řešení incidentu #{incident.Id}...");
    }

    protected virtual void Close(Incident incident)
    {
        Console.WriteLine($"Incident #{incident.Id} vyřešen.");
        incident.ChangeStatus(IncidentStatus.Resolved);
    }



}


public class HardwareResolver : IncidentResolver
{
    protected override void Analyze(Incident incident)
    {
        Console.WriteLine(
            $"Analáza hardware incidentu #{incident.Id}..."
        );
    }

    protected override void Fix(Incident incident)
    {
        Console.WriteLine(
            $"Analáza opravy hardware pro incident #{incident.Id}..."
        );
    }
}

public class SoftwareResolver : IncidentResolver
{
    protected override void Analyze(Incident incident)
    {
        Console.WriteLine(
            $"Analáza software incidentu #{incident.Id}..."
        );
    }

    protected override void Fix(Incident incident)
    {
        Console.WriteLine(
            $"Analáza opravy software pro incident #{incident.Id}..."
        );
    }
}

public class NetworkResolver : IncidentResolver
{
    protected override void Analyze(Incident incident)
    {
        Console.WriteLine(
            $"Analáza network incidentu #{incident.Id}..."
        );
    }

    protected override void Fix(Incident incident)
    {
        Console.WriteLine(
            $"Analáza opravy network pro incident #{incident.Id}..."
        );
    }
}

public class SecurityResolver : IncidentResolver
{
    protected override void Analyze(Incident incident)
    {
        Console.WriteLine(
            $"Analáza security incidentu #{incident.Id}..."
        );
    }

    protected override void Fix(Incident incident)
    {
        Console.WriteLine(
            $"Analáza opravy security pro incident #{incident.Id}..."
        );
    }
}






public abstract class IncidentCreator
{
    public abstract Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description
    );

public static IncidentCreator GetCreator(IncidentType type)
{
    return type switch
    {
        IncidentType.Hardware => new HardwareIncidentCreator(),
        IncidentType.Software => new SoftwareIncidentCreator(),
        IncidentType.Network => new NetworkIncidentCreator(),
        IncidentType.Security => new SecurityIncidentCreator(),
        IncidentType.Other => new OtherIncidentCreator(),
        _ => throw new ArgumentException("Neznamý typ incidentů")
    };
}

}

public class HardwareIncidentCreator : IncidentCreator
{
    public override Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description)
    {
        return new HardwareIncident(
            id,
            priority,
            description
        );
    }
}

public class SoftwareIncidentCreator : IncidentCreator
{
    public override Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description)
    {
        return new SoftwareIncident(
            id,
            priority,
            description
        );
    }
}

public class NetworkIncidentCreator : IncidentCreator
{
    public override Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description)
    {
        return new NetworkIncident(
            id,
            priority,
            description
        );
    }
}

public class SecurityIncidentCreator : IncidentCreator
{
    public override Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description)
    {
        return new SecurityIncident(
            id,
            priority,
            description
        );
    }
}

public class OtherIncidentCreator : IncidentCreator
{
    public override Incident CreateIncident(
        int id,
        IncidentPriority priority,
        string description)
    {
        return new OtherIncident(
            id,
            priority,
            description
        );
    }
}