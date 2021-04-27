using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Agent.Shared.Models
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
