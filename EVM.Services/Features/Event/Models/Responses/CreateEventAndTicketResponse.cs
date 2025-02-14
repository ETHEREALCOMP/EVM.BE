using EVM.Services.Features.Models.Responses;

namespace EVM.Services.Features.Event.Models.Responses;

public class CreateEventAndTicketResponse : BaseResponse
{
    public Guid? TicketId { get; set; }
}
