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
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line Up"; }
        }
        public LineUpVM()
        {

            Fill();
        }

        private void Fill()
        {
            FillTime();
            Dates = Festival.GetDates();
            Stages = Stage.GetStages();
            Bands = Band.GetBands();
            
           
            
            
        }
        private ObservableCollection<LineUp> _performances;

        public ObservableCollection<LineUp> Performances
        {
            get { return _performances; }
            set { _performances = value; OnPropertyChanged("Performances"); OnPropertyChanged("SelectedDate"); OnPropertyChanged("SelectedStage"); }
        }
        
        private void FillTime()
        {
            Startmin = 0;
            Startuur = DateTime.Now.Hour;
            Einduur = Startuur;
            Eindmin = 5;
            EndTime = StartTime.AddMinutes(5);
        }
        private string _totInfo;

        public string TotInfo
        {
            get { return _totInfo; }
            set { _totInfo = value; }
        }
        
        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; OnPropertyChanged("StartTime"); }
        }
        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; OnPropertyChanged("EndTime"); }
        }
        private int _startmin;

        public int Startmin
        {
            get { return _startmin; }
            set { _startmin = value; OnPropertyChanged("Startmin"); }
        }
        private int _startuur;

        public int Startuur
        {
            get { return _startuur; }
            set { _startuur = value; OnPropertyChanged("Startuur"); }
        }
        private int _eindmin;

        public int Eindmin
        {
            get { return _eindmin; }
            set { _eindmin = value; OnPropertyChanged("Eindmin"); }
        }
        private int _einduur;

        public int Einduur
        {
            get { return _einduur; }
            set { _einduur = value; OnPropertyChanged("Einduur"); }
        }
        private LineUp _selectedPerf;

        public LineUp SelectedPerf
        {
            get { return _selectedPerf; }
            set { _selectedPerf = value; OnPropertyChanged("SelectedPerf"); }
        }
        
        
        

        
        private ObservableCollection<Festival> _dates;

        public ObservableCollection<Festival> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }
        
        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }
        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }
        private Festival _selectedDate;

        public Festival SelectedDate
        {
            get { return _selectedDate;  }
            set { _selectedDate = value; OnPropertyChanged("SelectedDate"); }
        }
        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set { _selectedStage = value; OnPropertyChanged("SelectedStage"); }
        }
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(Refresh); }
        }

        private void Refresh()
        {
            Fill();
        }
        #region TimeCommands
#region StartTijdCommands
        public ICommand Add5S
        {
            get { return new RelayCommand(Add5StartSec); }
        }
        private void Add5StartSec()
        {
            if (Startmin >= 60)
            {
                Startmin = 0;
            }
            if (Startuur >= 24)
            {
                Startuur = 0;
            }
            if (Startmin + 5 >= 60)
            {
                Startmin = 0;
                if (Startuur + 1 >= 24)
                {
                    Startuur = 0;
                }
                else
                {
                    Startuur += 1;
                }
            }
            else
            {
                Startmin = Startmin + 5;
            }
        }
        public ICommand Rem5S
        {
            get { return new RelayCommand(Rem5StartSec); }
        }

        private void Rem5StartSec()
        {
            if (Startmin - 5 < 0)
            {
                Startmin = 55;
                if (Startuur - 1 < 0)
                {
                    Startuur = 0;
                }
                else
                {
                    Startuur -= 1;
                }
            }
            else
            {
                Startmin = Startmin - 5;
            }
        }
        public ICommand Add1S
        {
            get { return new RelayCommand(Add1StartUur); }
        }

        private void Add1StartUur()
        {
            if (Startuur + 1 >= 24)
            {
                Startuur = 0;
            }
            else
            {
                Startuur += 1;
            }
        }
        public ICommand Rem1S
        {
            get { return new RelayCommand(Rem1StartUur); }
        }

        private void Rem1StartUur()
        {
            if (Startuur - 1 >= 24)
            {
                Startuur = 0;
            }
            else
            {
                Startuur -= 1;
            }
        }
#endregion
        #region EindTijdCommands
        public ICommand Add5E
        {
            get { return new RelayCommand(Add5EindSec); }
        }
        private void Add5EindSec()
        {
            if (Eindmin >= 60)
            {
                Eindmin = 0;
            }
            if (Einduur >= 24)
            {
                Einduur = 0;
            }
            if (Eindmin + 5 >= 60)
            {
                Eindmin = 0;
                if (Einduur + 1 >= 24)
                {
                    Einduur = 0;
                }
                else
                {
                    Einduur += 1;
                }
            }
            else
            {
                Eindmin = Eindmin + 5;
            }
        }
        public ICommand Rem5E
        {
            get { return new RelayCommand(Rem5EindSec); }
        }

        private void Rem5EindSec()
        {
            if (Eindmin - 5 < 0)
            {
                Eindmin = 55;
                if (Einduur - 1 < 0)
                {
                    Einduur = 0;
                }
                else
                {
                    Einduur -= 1;
                }
            }
            else
            {
                Eindmin = Eindmin - 5;
            }
        }
        public ICommand Add1E
        {
            get { return new RelayCommand(Add1EindUur); }
        }

        private void Add1EindUur()
        {
            if (Einduur + 1 >= 24)
            {
                Einduur = 0;
            }
            else
            {
                Einduur += 1;
            }
        }
        public ICommand Rem1E
        {
            get { return new RelayCommand(Rem1EindUur); }
        }

        private void Rem1EindUur()
        {
            if (Einduur - 1 >= 24)
            {
                Einduur = 0;
            }
            else
            {
                Einduur -= 1;
            }
        }
        #endregion
        #endregion
        public ICommand ToonLineUpCommand
        {
            get { return new RelayCommand(ToonLineUp,()=>(SelectedStage != null && SelectedDate != null)); }
        }

        private void ToonLineUp()
        {
            Performances = LineUp.GetPerformances(SelectedDate, SelectedStage);
        }
        public ICommand AddPerfCommand
        {
            get { return new RelayCommand(AddPerf,()=> SelectedBand != null && SelectedStage != null && SelectedDate != null && Check() == true); }
        }

        private bool Check()
        {
            if ((Startuur * 60 + Startmin) >= (Einduur * 60 + Eindmin))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        private void AddPerf()
        {
            LineUp.AddPerformance(SelectedBand, SelectedDate, SelectedStage, Startmin, Startuur, Eindmin, Einduur);
            Performances = LineUp.GetPerformances(SelectedDate, SelectedStage);
        }
        public ICommand DeletePerfCommand
        {
            get { return new RelayCommand(DeletePerf, ()=> SelectedPerf!=null);}
        }

        private void DeletePerf()
        {
            LineUp.DeleteDate(SelectedPerf);
            Performances = LineUp.GetPerformances(SelectedDate, SelectedStage);
        }



    }


}
