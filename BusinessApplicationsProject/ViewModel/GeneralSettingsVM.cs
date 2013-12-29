using GalaSoft.MvvmLight.Command;
using ProjectBussinessApplications.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessApplicationsProject.ViewModel
{
    class GeneralSettingsVM : ObservableObject, IPage  {
        public string Name
        {
            get { return "General Settings"; }
        }
        public GeneralSettingsVM()
        {
            Dates = Festival.GetDates();
            Genres = Genre.GetGenres();
            AddEnabled = false;
            AddNewEnabled = true;
            UpdateEnabled = true;
            ListGEnabled = true;
            
            
        }
        private bool _updateEnabled;

        public bool UpdateEnabled
        {
            get { return _updateEnabled; }
            set { _updateEnabled = value; OnPropertyChanged("UpdateEnabled"); }
        }
        private bool _listGEnabled;

        public bool ListGEnabled
        {
            get { return _listGEnabled; }
            set { _listGEnabled = value; OnPropertyChanged("ListGEnabled"); }
        }
        
        private bool _addEnabled;

        public bool AddEnabled
        {
            get { return _addEnabled; }
            set { _addEnabled = value; OnPropertyChanged("AddEnabled"); }
        }
        private bool _addNewEnabled;

        public bool AddNewEnabled
        {
            get { return _addNewEnabled; }
            set { _addNewEnabled = value; OnPropertyChanged("AddNewEnabled"); }
        }
        
        
        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }
        
        private Festival _sd;

        public Festival SD
        {
            get { return _sd; }
            set { _sd = value; OnPropertyChanged("SD"); }
        }
        
        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged("SelectedDate"); }
        }
                
        private ObservableCollection<Festival> _dates;
       
        public ObservableCollection<Festival> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged("Genres"); }
        }

        public ICommand UpdateGenreCommand
        {
            get { return new RelayCommand(UpdateGenre, () => { return SelectedGenre != null; }); }
        }

        private void UpdateGenre()
        {
            Genre.UpdateGenre(SelectedGenre);
        }
        public ICommand AddNewGenreCommand
        {
            get { return new RelayCommand(AddNewGenre); }
        }

        private void AddNewGenre()
        {
            Genre nieuw = new Genre();
            nieuw.Name = "(leeg)";
            nieuw.Id = Festival.ZoekId();
            SelectedGenre = nieuw;
            Genres.Add(SelectedGenre);
            AddNewEnabled = false;
            AddEnabled = true;
            UpdateEnabled = false;
            ListGEnabled = false;
        }
        public ICommand AddCommand
        {
            get { return new RelayCommand(Add); }
        }

        private void Add()
        {
            Genre.AddGenre(SelectedGenre);
            AddNewEnabled = true;
            AddEnabled = false;
            UpdateEnabled = true;
            ListGEnabled = true;
        }
        public ICommand AnnuleerCommand
        {
            get { return new RelayCommand(Annuleer); }
        }

        private void Annuleer()
        {
            Genres.Remove(SelectedGenre);
            AddNewEnabled = true;
            AddEnabled = false;
            UpdateEnabled = true;
            ListGEnabled = true;
        }
        public ICommand AddDateCommand
        {
            get { return new RelayCommand(AddDate,() => { return SelectedDate.Date.ToString() != "1/01/0001 0:00:00" ; }); }
        }

        private void AddDate()
        {
            if(Check())
            {
                Festival.AddDate(SelectedDate);
                
            }
            Dates = Festival.GetDates();
            AddNewEnabled = true;
            AddEnabled = true;
            UpdateEnabled = true;
            
        }
        public ICommand RemoveDateCommand
        {
            get { return new RelayCommand(RemoveDate,() => { return SD != null ; }); }
        }

        private void RemoveDate()
        {
            Festival.DeleteDate(SD);
            Dates = Festival.GetDates();
        }

        private bool Check()
        {
            foreach (Festival d in Festival.GetDates())
            {
                if (d.Date == SelectedDate.Date)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }


        
    }
}
