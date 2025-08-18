using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyPlanner
{
    public static class FileReader
    {
        private static string GetPathToUnitsFile()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{assemblyLocation}\\ThousandSonsUnits.txt";
        }

        private static string GetPathToArmiesFile()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{assemblyLocation}\\SavedArmies.Json";
        }

        public static ArmyUnit[]? GetAllUnits()
        {
            var path = GetPathToUnitsFile();
            if (!File.Exists(path)) return null;

            List<ArmyUnit> units = [];
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                // FUTURE: check lines with RegEx?
                var segments = line.Split(',');
                if (segments.Length == 2)
                {
                    units.Add(new(segments[0].Trim(), int.Parse(segments[1].Trim())));
                }
            }

            return units.ToArray();
        }

        public static List<ArmyUnit[]> GetAllSavedArmies()
        {
            List<ArmyUnit[]> toReturn = [];

            toReturn.Add([new("test name", 60)]);
            toReturn.Add([new("test name2", 60)]);

            return toReturn;
        }
    }
}
