using EVM.Data.Models.PaymentsFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Data.Models.EventFeature;

public class Event : IDBConfigurableModel
{
    public string? Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string ProjectFounderId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public required bool SoldOut { get; set; } = false;

    public virtual List<Ticket> Tickets { get; set; } = [];

    public virtual List<TicketTypes> TicketTypes { get; set; } = [];


    public static void BuildModel(ModelBuilder builder)
    {
        builder.Entity<Event>()
            .ToTable(nameof(Event));
    }
}
