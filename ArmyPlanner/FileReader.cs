using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;

namespace ArmyPlanner
{
    public static class FileReader
    {
        private static string GetPathToUnitsFile()
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{assemblyLocation}\\ThousandSonsUnits.txt";
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

        public static List<ArmyUnit> SelectAndLoadArmy()
        {
            var result = new List<ArmyUnit>();

            var dialogue = new OpenFileDialog();

            dialogue.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dialogue.Filter = "json files(*.json) | *.json";

            if (dialogue.ShowDialog().Value)
            {
                var filePath = dialogue.FileName;
                var fileStream = dialogue.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    var army = JsonConvert.DeserializeObject<JObject>(fileContent);
                    var units = army.GetValue("Units");
                    var unitsArray = units.Children();

                    var allUnits = GetAllUnits();

                    if (allUnits != null)
                    {
                        foreach (var unit in unitsArray)
                        {
                            var unitFound = allUnits.FirstOrDefault(u => u.Name == unit.Value<string>());
                            if (unitFound != null)
                            {
                                result.Add(unitFound);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
