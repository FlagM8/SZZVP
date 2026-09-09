namespace SZZVP;



using System;

internal static class Program
{
    private static void Main()
    {
/*
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
        Console.WriteLine($"-----------------------");
        IncidentType type = IncidentType.Security;

        IncidentCreator creator = IncidentCreator.GetCreator(type);

        Incident incident1 = creator.CreateIncident(
            1,
            IncidentPriority.Critical,
            "Unauthorized access detected"
        );

        Console.WriteLine(incident1);
        Console.WriteLine(incident1.GetType().Name);
*/
        HelpdeskConfig config = new HelpdeskConfig
        {
            ExitWhenAllResolved = true,
        };

        Helpdesk helpdesk = new Helpdesk(config);

        User jan = new User(
            "Jan",
            UserRole.Reporter
        );

        User peter = new User(
            "Peter",
            UserRole.Reporter
        );

        helpdesk.CreateIncident(
            IncidentType.Hardware,
            IncidentPriority.Low,
            "Keyboard does not work",
            jan
        );

        helpdesk.CreateIncident(
            IncidentType.Network,
            IncidentPriority.High,
            "Network connection unavailable",
            peter
        );

        helpdesk.CreateIncident(
            IncidentType.Security,
            IncidentPriority.Critical,
            "Unauthorized login detected",
            jan
        );

        helpdesk.Run();
    }
}

