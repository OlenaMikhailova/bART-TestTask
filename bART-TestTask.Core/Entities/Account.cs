using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; } 

        [Required, StringLength(255)]
        public string Name { get; set; } = string.Empty; 

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
