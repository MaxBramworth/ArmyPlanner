using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ArmyPlanner
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public int SelectedAllUnitsIndex
        {
            get => _selectedAllunitsIndex;
            set => SetProperty(ref _selectedAllunitsIndex, value);
        }

        public int SelectedArmyIndex
        {
            get => _selectedArmyIndex;
            set => SetProperty(ref _selectedArmyIndex, value);
        }

        public string PointsTotal
        {
            get => _pointsTotal;
            set => SetProperty(ref _pointsTotal, value);
        }

        public ObservableCollection<ArmyUnit> AvailableArmyUnits { get; set; } = [];

        public ObservableCollection<ArmyUnit> CurrentArmy { get; set; } = [];

        public MainWindowViewModel()
        {
            foreach (var unit in FileReader.GetAllUnits())
            {
                AvailableArmyUnits.Add(unit);
            }

            CurrentArmy.CollectionChanged += (_, _) => RecalculatePointsTotal();
        }

        public RelayCommand AddToArmyCommand => new(AddToArmy);
        public RelayCommand RemoveFromArmyCommand => new(RemoveFromArmy);

        void AddToArmy()
        {
            CurrentArmy.Add(AvailableArmyUnits[SelectedAllUnitsIndex]);
        }

        void RemoveFromArmy()
        {
            CurrentArmy.RemoveAt(SelectedArmyIndex);
        }

        void RecalculatePointsTotal()
        {
            int points = 0;

            foreach (var unit in CurrentArmy)
            {
                points += unit.PointsCost;
            }

            PointsTotal = $"Total: {points}";
        }

        private string _title = "Army", _pointsTotal = "Total: 0";
        private int _selectedAllunitsIndex, _selectedArmyIndex;
    }
}
