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

        public string SearchbarText
        {
            get => _searchBarText;
            set
            {
                SetProperty(ref _searchBarText, value);
                if (_searchBarText.Length > 1)
                {
                    SetAvailableArmyUnits(value);
                }
                else
                {
                    SetAvailableArmyUnits();
                }
            }
        }

        public ObservableCollection<ArmyUnit> AvailableArmyUnits { get; set; } = [];

        public ObservableCollection<ArmyUnit> CurrentArmy { get; set; } = [];

        public MainWindowViewModel()
        {
            _armyUnits = FileReader.GetAllUnits();
            SetAvailableArmyUnits();

            CurrentArmy.CollectionChanged += (_, _) => RecalculatePointsTotal();
        }

        public RelayCommand AddToArmyCommand => new(AddToArmy);
        public RelayCommand RemoveFromArmyCommand => new(RemoveFromArmy);  
        public RelayCommand SaveCommand => new(SaveArmy);  
        public RelayCommand LoadCommand => new(LoadArmy);
        public RelayCommand SearchbarTextChangedCommand => new(SearchbarTextChanged);

        void AddToArmy()
        {
            CurrentArmy.Add(AvailableArmyUnits[SelectedAllUnitsIndex]);
        }

        void RemoveFromArmy()
        {
            CurrentArmy.RemoveAt(SelectedArmyIndex);
        }

        void SaveArmy()
        {
            FileReader.SaveCurrentArmy(CurrentArmy.ToList());
        }

        void LoadArmy()
        {
            CurrentArmy.Clear();
            foreach (var unit in FileReader.SelectAndLoadArmy())
            {
                CurrentArmy.Add(unit);
            }
        }

        void SearchbarTextChanged()
        {

        }

        void SetAvailableArmyUnits(string searchCriterion = "")
        {
            if (string.IsNullOrWhiteSpace(searchCriterion))
            {
                foreach (var unit in _armyUnits)
                {
                    AvailableArmyUnits.Add(unit);
                }
            }
            else
            {
                AvailableArmyUnits.Clear();
                searchCriterion = searchCriterion.ToLower();
                foreach (var unit in _armyUnits.Where(u => u.Name.ToLower().Contains(searchCriterion)))
                {
                    AvailableArmyUnits.Add(unit);
                }
            }
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

        private string _title = "Army", _pointsTotal = "Total: 0", _searchBarText;
        private int _selectedAllunitsIndex, _selectedArmyIndex;
        private readonly ArmyUnit[] _armyUnits = [];
    }
}
