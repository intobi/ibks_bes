using AutoMapper;
using IBKS.Core.Models;
using IBKS.Core.Models.Enums;
using IBKS.DataAccess.Entities;

namespace IBKS.Shared.Mapper;

public static class MyMapper
{
    public static string FormatTicketStatus(TicketStatus status)
    {
        return status switch
        {
            TicketStatus.Awaiting_Response_User => TicketStatusValues.Awaiting_Response_User,
            TicketStatus.Awaiting_Response_Development => TicketStatusValues.Awaiting_Response_Development,
            TicketStatus.Awaiting_Response_Vendor => TicketStatusValues.Awaiting_Response_Vendor,
            _ => TicketStatusValues.Default_Response(status.ToString())
        };
    }

    public static int GetTicketTypeId(string value)
    {
        return value switch
        {
            "Question" => (int)TicketTypes.Question,
            "Issue" => (int)TicketTypes.Issue,
            "Suggestion" => (int)TicketTypes.Suggestion,
            "Feedback" => (int)TicketTypes.Feedback,
            _ => (int)TicketTypes.Question
        };
    }

    public static int GetTicketStatusId(string value)
    {
        return value switch
        {
            "New" => (int)TicketStatus.New,
            "Open" => (int)TicketStatus.Open,
            "Awaiting Response - User" => (int)TicketStatus.Awaiting_Response_User,
            "Awaiting Response - Development" => (int)TicketStatus.Awaiting_Response_Development,
            "Awaiting Response - Vendor" => (int)TicketStatus.Awaiting_Response_Vendor,
            "Closed" => (int)TicketStatus.Closed,
            _ => (int)TicketStatus.New
        };
    }

    public static int GetTicketPriorityId(string value)
    {
        return value switch
        {
            "Low" => (int)TicketPriority.Low,
            "Medium" => (int)TicketPriority.Medium,
            "Heigh" => (int)TicketPriority.Heigh,
            "Priority" => (int)TicketPriority.Priority,
            "None" => (int)TicketPriority.None,
            _ => (int)TicketPriority.Low
        };
    }

    public static IMapper GetDefaultMapper()
    {
        return new MapperConfiguration(mapper =>
        {
            mapper.CreateMap<Ticket, TicketModel>();

            mapper.CreateMap<TicketModel, TicketListModel>()
            .ForMember(t => t.Type, opt => opt.MapFrom(item => (TicketTypes)item.TicketTypeId))
            .ForMember(t => t.Module, opt => opt.MapFrom(item => item.ApplicationName))
            .ForMember(t => t.UrgentLvl, opt => opt.MapFrom(item => (TicketPriority)item.PriorityId))
            .ForMember(t => t.State, opt => opt.MapFrom(item => FormatTicketStatus((TicketStatus)item.StatusId)));

            mapper.CreateMap<TicketModel, TicketViewModel>()
            .ForMember(t => t.Type, opt => opt.MapFrom(item => (TicketTypes)item.TicketTypeId))
            .ForMember(t => t.Module, opt => opt.MapFrom(item => item.ApplicationName))
            .ForMember(t => t.UrgentLvl, opt => opt.MapFrom(item => (TicketPriority)item.PriorityId))
            .ForMember(t => t.State, opt => opt.MapFrom(item => FormatTicketStatus((TicketStatus)item.StatusId)));

            mapper.CreateMap<TicketPostModel, Ticket>()
            .ForMember(t => t.TicketTypeId, opt => opt.MapFrom(item => GetTicketTypeId(item.Type)))
            .ForMember(t => t.ApplicationName, opt => opt.MapFrom(item => item.Module))
            .ForMember(t => t.StatusId, opt => opt.MapFrom(item => GetTicketStatusId(item.State)));

            mapper.CreateMap<TicketViewModel, Ticket>()
            .ForMember(t => t.TicketTypeId, opt => opt.MapFrom(item => GetTicketTypeId(item.Type)))
            .ForMember(t => t.ApplicationName, opt => opt.MapFrom(item => item.Module))
            .ForMember(t => t.PriorityId, opt => opt.MapFrom(item => GetTicketPriorityId(item.UrgentLvl)))
            .ForMember(t => t.StatusId, opt => opt.MapFrom(item => GetTicketStatusId(item.State)));

            mapper.CreateMap<Ticket, TicketViewModel>()
            .ForMember(t => t.Type, opt => opt.MapFrom(item => (TicketTypes)item.TicketTypeId))
            .ForMember(t => t.Module, opt => opt.MapFrom(item => item.ApplicationName))
            .ForMember(t => t.UrgentLvl, opt => opt.MapFrom(item => (TicketPriority)item.PriorityId))
            .ForMember(t => t.State, opt => opt.MapFrom(item => FormatTicketStatus((TicketStatus)item.StatusId)));

            mapper.CreateMap<ReplyPostModel, Reply>();
        }).CreateMapper();
    }
}
