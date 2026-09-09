namespace SZZVP;


public class HelpdeskConfig
{
    public bool ExitWhenAllResolved { get; set; }
}


public class Helpdesk
{
    private readonly List<Incident> incidents = new(); //místo nějaké db

    private readonly HelpdeskConfig config;

    private readonly SupportHandler supportChain;


    private int nextIncidentId = 1; 
    private SupportHandler CreateSupportChain()
    {
        var l1 = new L1Handler();
        var l2 = new L2Handler();
        var l3 = new L3Handler();
        var admin = new AdminHandler();

        l1.SetNext(l2)
        .SetNext(l3)
        .SetNext(admin);

        return l1;
    }
    public Helpdesk(HelpdeskConfig config)
    {
        this.config = config;

        supportChain = CreateSupportChain();


    }


    public Incident CreateIncident(
        IncidentType type,
        IncidentPriority priority,
        string description,
        User reporter)
    {
        var creator = IncidentCreator.GetCreator(type);

        var incident = creator.CreateIncident(
            nextIncidentId++,
            priority,
            description
        );

        incident.AddObserver(reporter);

        incidents.Add(incident);

        Console.WriteLine();
        Console.WriteLine($"Incident #{incident.Id} vytvořen.");

        return incident;
    }

    public void ShowIncidents()
    {
        Console.WriteLine();
        Console.WriteLine("===== INCIDENTY =====");

        if (incidents.Count == 0)
        {
            Console.WriteLine("Žádné incidenty.");
            return;
        }

        foreach (var incident in incidents)
        {
            Console.WriteLine(incident);
        }
    }

    public void ProcessIncidents()
    {
        var incidentsToProcess = incidents
            .Where(i =>
                !i.RequiresExternalSupport &&
                (i.Status == IncidentStatus.New ||
                 i.Status == IncidentStatus.Escalated))
            .Take(1) //zpracovávání jde po jednom, možno přidat do configu
            .ToList();

        if (incidentsToProcess.Count == 0)
        {
            Console.WriteLine("Žádné incidenty k zpracování.");
            return;
        }

        foreach (var incident in incidentsToProcess)
        {
            Console.WriteLine();
            Console.WriteLine($"Processing incident #{incident.Id}...");

            supportChain.Handle(incident);
        }
    }


    public void ShowOverview()
    {
        int openCount = incidents.Count(
            i => i.Status != IncidentStatus.Resolved
        );

        Console.WriteLine();
        Console.WriteLine("===== Statistika =====");
        Console.WriteLine($"Otevřených incidentů: {openCount}");
        Console.WriteLine($"Celk. incidentů: {incidents.Count}");
    }

public void Run()
{
    bool running = true;

    while (running)
    {
        Console.WriteLine();

        ShowOverview();
        ShowIncidents();

        ProcessIncidents();

        if (incidents.Any(i => i.RequiresExternalSupport) &&
            incidents.All(i => i.Status == IncidentStatus.Resolved || i.RequiresExternalSupport))
        {
            Console.WriteLine("Automatické zpracování skončilo. Některé incidenty čekají na externí podporu.");
            break;
        }

        if (config.ExitWhenAllResolved &&
            incidents.Count > 0 &&
            incidents.All(i =>
                i.Status == IncidentStatus.Resolved))
        {
            Console.WriteLine();
            Console.WriteLine("Vše bylo vyřešeno");
            running = false;
        }
    }

    Console.WriteLine();
    Console.WriteLine("===== Finální stav =====");

    ShowOverview();
    ShowIncidents();
}


}