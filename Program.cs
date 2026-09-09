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

        User adminUser = new User(
            "Administrator",
            UserRole.Administrator
        );

        Incident incident = new SecurityIncident(
            1,
            IncidentPriority.Critical,
            "Possible unauthorized access detected"
        );

        incident.AddObserver(reporter);
        incident.AddObserver(adminUser);


        var l1 = new L1Handler();
        var l2 = new L2Handler();
        var l3 = new L3Handler();
        var admin = new AdminHandler();

        l1.SetNext(l2)
        .SetNext(l3)
        .SetNext(admin);


        l1.Handle(incident);
    }
}

