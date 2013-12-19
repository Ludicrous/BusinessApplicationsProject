using ProjectBussinessApplications.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplicationsProject.ViewModel
{
    class GeneralSettingsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "General Settings"; }
        }
        public GeneralSettingsVM()
        {
            //_dates = Festival.GetDates();
        }
        private ObservableCollection<Festival> _dates;
       
        public ObservableCollection<Festival> Dates
        {
            get { return _dates; }
            set { _dates = value; }
        }
        
    }
}
