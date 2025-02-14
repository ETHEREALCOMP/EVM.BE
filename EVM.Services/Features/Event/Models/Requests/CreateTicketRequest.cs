using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Services.Features.Event.Models.Requests;

public class CreateTicketRequest
{
    public decimal Price { get; set; }

    public string? Location { get; set; }

    public Data.Enums.TicketType? Type { get; set; }
}
