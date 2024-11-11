using IBKS.Core.Models.Enums;
using IBKS.DataAccess.Entities;

namespace IBKS.Core.Models;

public class TicketModel : Ticket;
public class TicketPostModel : TicketGeneralInfo;

public class TicketGeneralInfo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Module { get; set; }
    public string Type { get; set; }
    public string State { get; set; }
}

public class TicketListModel : TicketGeneralInfo
{
    public long Id { get; set; }
    public string UrgentLvl { get; set; }
}

public class TicketViewModel : TicketGeneralInfo
{
    public long Id { get; set; }
    public string UrgentLvl { get; set; }
}
