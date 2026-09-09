namespace SZZVP;



using System;

internal static class Program
{
    private static void Main()
    {
        User reporter = new User(
            "Jan",
            UserRole.Reporter
        );

        User admin = new User(
            "Administrator",
            UserRole.Administrator
        );

        Incident incident = new HardwareIncident(
            1,
            IncidentPriority.High,
            "Notebook se nezapne"
        );

        incident.AddObserver(reporter);
        incident.AddObserver(admin);

        Console.WriteLine(incident);

        incident.ChangeStatus(IncidentStatus.InProgress);

        incident.ChangeStatus(IncidentStatus.Resolved);
    }
}

