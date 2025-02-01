using EVM.Data.Models.EventFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Services.Features.Event.Models.Responses;
public class GetEventResponse
{ 
    public required string Name { get; set; }

    public required List<EventTask> ETask { get; set; }
 
}
