using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Entities
{
    public class Incident
    {
        [Key]
        public string IncidentName { get; set; } = $"Incident-{Guid.NewGuid()}";

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; } = null!;
    }
}
