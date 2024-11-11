namespace IBKS.Core.Models.Enums;

public enum TicketStatus
{
    New = 1,
    Open,
    Awaiting_Response_User,
    Awaiting_Response_Development,
    Awaiting_Response_Vendor,
    Closed
}

public static class TicketStatusValues
{
    public static string New => "New";
    public static string Open => "Open";
    public static string Awaiting_Response_User => "Awaiting Response - User";
    public static string Awaiting_Response_Development => "Awaiting Response - Development";
    public static string Awaiting_Response_Vendor => "Awaiting Response - Vendor";
    public static string Closed => "Closed";
    public static string Default_Response(string responseName) => responseName.ToString().Replace('_', ' ');
}
