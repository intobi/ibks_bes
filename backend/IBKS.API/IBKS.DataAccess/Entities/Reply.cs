using IBKS.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBKS.DataAccess.Entities;

[Table("TicketReply", Schema = "Support")]
public class Reply : Entity
{
    [Key]
    [Column("ReplyId")]
    public int Id { get; set; }
    [Column("TId")]
    public long TicketId { get; set; }
    [Column("Reply")]
    public string ReplyMessage { get; set; }
    [Column("ReplyDate")]
    public DateTime ReplyDate { get; set; }

    public Reply()
    {
        this.ReplyDate = DateTime.UtcNow;
    }
}
