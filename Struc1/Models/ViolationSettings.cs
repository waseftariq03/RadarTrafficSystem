using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struc.Models
{
    public class  ViolationSettings
    {
        public int MaxAllowedSpeed { get; set; }

        public List<string> AllowedDirections { get; set; } = new();

        public List<int> EmergencyLanes { get; set; } = new();
    }
}
