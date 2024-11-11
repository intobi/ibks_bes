using IBKS.DataAccess.Entities;

namespace IBKS.Core.Models;

public class ReplyModel : Reply;

public class ReplyViewModel
{
    public string ReplyMessage { get; set; }
    public DateTime ReplyDate { get; set; }
}

public class ReplyPostModel
{
    public string ReplyMessage { get; set; }
}