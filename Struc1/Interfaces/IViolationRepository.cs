using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL .Entities;

namespace Struc.Interfaces
{
    public interface IViolationRepository
    {
        Task SaveAsync(Violation violation);
    }
}
