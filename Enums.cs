namespace SZZVP;
public enum IncidentType
{
    Hardware,
    Software,
    Network,
    Security,
    Other
}

public enum IncidentPriority
{
    Low,
    Medium,
    High,
    Critical
}

public enum IncidentStatus
{
    New,
    InProgress,
    Escalated,
    Resolved
}

public enum UserRole
{
    Reporter,
    L1,
    L2,
    L3,
    Administrator
}