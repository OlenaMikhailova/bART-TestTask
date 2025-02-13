using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.DTOs
{
    public class AccountDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<ContactDTO> Contacts { get; set; } = new();
        public List<IncidentDTO> Incidents { get; set; } = new();
    }
}
