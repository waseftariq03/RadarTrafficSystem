using DAL.Entities;
using Struc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struc.Interfaces
{
    public interface IRadarSimulator
    {
        Violation GenerateDetection();
    }
}
