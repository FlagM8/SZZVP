namespace SZZVP;


public interface IIncidentObserver
{
    void Update(Incident incident);
}

public class User: IIncidentObserver
{
    public string Username { get; }
    public UserRole Role { get; }

    public User(string username, UserRole role)
    {
        Username = username;
        Role = role;
    }

    public void Update(Incident incident)
    {
        Console.WriteLine($"[NOTIFIKACE] pro uživatele {Username}: Status incidentu {incident.Id} zmenen na {incident.Status}");
    }

}