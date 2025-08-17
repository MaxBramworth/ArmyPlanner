using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyPlanner
{
    public class ArmyUnit
    {
        public string Name { get; set; }
        public int PointsCost { get; set; }

        public ArmyUnit(string name, int pointsCost)
        {
            Name = name;
            PointsCost = pointsCost;
        }
    }
}
