using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVM.Data.Models.IdentityFeature;

public class Organizer
{
    public Guid OrganizerId { get; set; }

    public required string Name { get; set; }

    public required string ContactInfo { get; set; } // later rework to list
}
