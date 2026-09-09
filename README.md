# Helpdesk

Jednoduchá konzolová aplikace, která simuluje IT helpdesk. Eviduje technické incidenty, zpracovává je a podle potřeby předává vyšší úrovni podpory. Uživatelům vypisuje oznámení o změnách stavu jejich incidentů.

## Struktura projektu

- `Program.cs` – spuštění aplikace a vytvoření ukázkových uživatelů a incidentů.
- `Enums.cs` – typy, priority a stavy incidentů, role uživatelů a úrovně podpory.
- `Incidents.cs` – incidenty, jejich vytváření a postup řešení.
- `Users.cs` – uživatelé a příjem oznámení.
- `Support.cs` – jednotlivé úrovně podpory a eskalace incidentů.
- `Helpdesk.cs` – evidence incidentů, konfigurace, hlavní smyčka a výpis přehledu.

## Návrhové vzory

- **Observer** (`Incidents.cs`, `Users.cs`) incident informuje registrované observery přes rozhraní `IIncidentObserver`. Uživatel při změně stavu obdrží oznámení do konzole.
- **Chain of Responsibility** (`Support.cs`) incident postupuje řetězcem `L1 → L2 → L3 → Administrator`. Pokud ho daná úroveň nemůže vyřešit nebo oprava selže, předá ho dál.
- **Template Method** (`Incidents.cs`) `IncidentResolver.Resolve()` určuje postup: přijetí, analýza, oprava, test a uzavření. Konkrétní resolvery přepisují jednotlivé kroky. Incident se uzavře pouze po úspěšné opravě a testu.
- **Factory Method** (`Incidents.cs`) Potomci `IncidentCreator` redefinuji `CreateIncident()` a vytvářejí konkrétní typy incidentů, například `HardwareIncident` nebo `SoftwareIncident`. Metoda `GetCreator()` vybírá odpovídajícího tvůrce podle typu.

## Spuštění

Pro spuštění je potřeba .NET SDK. Spustitelné přes:

```bash
dotnet run
```

## Použití

Po spuštění se vytvoří dva uživatelé a tři ukázkové incidenty.

Aplikace v každé iteraci vypíše seznam incidentů, jejich typy, priority, stavy a počet otevřených incidentů. Potom zpracuje jeden incident. V konzoli je vidět, která úroveň podpory ho řeší, případné předání dál a oznámení uživateli.

Při výchozím nastavení aplikace po vyřešení všech incidentů vypíše finální přehled a skončí. Pokud incident nezvládne ani administrátor, zůstane otevřený pro externí podporu.

Aktuální verze běží automaticky bez uživatelského menu. Ukázkové incidenty se přidávají nebo upravují v `Program.cs` pomocí `helpdesk.CreateIncident()`. Data se uchovávají pouze v paměti po dobu běhu aplikace.
