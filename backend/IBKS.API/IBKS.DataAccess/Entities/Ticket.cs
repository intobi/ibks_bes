using IBKS.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace IBKS.DataAccess.Entities;

[Table("Ticket", Schema = "Support")]
public class Ticket : Entity
{
    public string Title { get; set; }
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; }
    public string Description { get; set; }
    public int PriorityId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
    public string UserOID { get; set; }
    public int InstalledEnvironmentId { get; set; }
    public int TicketTypeId { get; set; }
    public DateTime Date {  get; set; }
    public bool? Deleted { get; set; }
    public DateTime LastModified { get; set; }
    public string? CreatedByOID { get; set; }

    public Ticket()
    {
        this.ApplicationId = 12;
        this.PriorityId = 2;
        this.UserId = 37;
        this.UserOID = "2860f009-4fe4-412a-8c2a-ee25b5ffa991";
        this.CreatedByOID = "2860f009-4fe4-412a-8c2a-ee25b5ffa991";
        this.InstalledEnvironmentId = 1;
        this.LastModified = DateTime.UtcNow;
        this.Date = DateTime.UtcNow;
    }
}
