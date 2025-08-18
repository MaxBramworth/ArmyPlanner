using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ArmyPlanner
{
    public class MainWindowViewModel
    {
        public ObservableCollection<UserControl> Armies { get; set; } = new();

        public MainWindowViewModel()
        {
            foreach (var army in FileReader.GetAllSavedArmies())
            {
                Armies.Add(new ArmyView());
            }
        }
    }
}
