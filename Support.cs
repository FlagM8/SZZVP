namespace SZZVP;
public abstract class SupportHandler 
{
    protected SupportHandler? NextHandler { get; private set; }

    public SupportHandler SetNext(SupportHandler nextHandler)
    {
        NextHandler = nextHandler;
        return nextHandler;
    }

    public abstract void Handle(Incident incident);

    public void EscalateToSomeoneNotStupid(Incident incident)
    {
        incident.ChangeStatus(IncidentStatus.Escalated);

        if (NextHandler != null)
        {
            NextHandler.Handle(incident);
        }
        else
        {
            Console.WriteLine(
                $"Incident #{incident.Id} nemohl být vyřešen"
            );
        }
    }
}

class L1Handler : SupportHandler
{
    public override void Handle(Incident incident)
    {
        Console.WriteLine(
                $"Incident #{incident.Id} řešen L1"
            );
        if (incident.Priority <= IncidentPriority.Medium)
        {
            incident.ChangeStatus(IncidentStatus.InProgress);
            Console.WriteLine(
                $"Incident #{incident.Id} akceptován L1."
            );
        }
        else
        {
            Console.WriteLine(
                $"Incident #{incident.Id} eskalován dál."
            );
            EscalateToSomeoneNotStupid(incident);
        }
    }
}


class L2Handler : SupportHandler
{
    public override void Handle(Incident incident)
    {
        Console.WriteLine(
                $"Incident #{incident.Id} řešen L2"
            );
        if (incident.Priority <= IncidentPriority.High)
        {
            incident.ChangeStatus(IncidentStatus.InProgress);
            Console.WriteLine(
                $"Incident #{incident.Id} akceptován L2."
            );
        }
        else
        {
            Console.WriteLine(
                $"Incident #{incident.Id} eskalován dál."
            );
            EscalateToSomeoneNotStupid(incident);
        }
    }
}


class L3Handler : SupportHandler
{
    public override void Handle(Incident incident)
    {
        Console.WriteLine(
                $"Incident #{incident.Id} řešen L3"
            );
        if (incident.Priority <= IncidentPriority.High)
        {
            incident.ChangeStatus(IncidentStatus.InProgress);
            Console.WriteLine(
                $"Incident #{incident.Id} akceptován L3."
            );
        }
        else
        {
            Console.WriteLine(
                $"Incident #{incident.Id} eskalován dál."
            );
            EscalateToSomeoneNotStupid(incident);
        }
    }
}

public class AdminHandler : SupportHandler
{
    public override void Handle(Incident incident)
    {
        Console.WriteLine(
            $"Administrator přijat incident #{incident.Id}."
        );

        incident.ChangeStatus(IncidentStatus.InProgress);
    }
}