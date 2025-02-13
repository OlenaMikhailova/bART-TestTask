using bART_TestTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART_TestTask.Core.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident> CreateAsync(Incident incident);
        Task SaveChangesAsync();
    }
}
