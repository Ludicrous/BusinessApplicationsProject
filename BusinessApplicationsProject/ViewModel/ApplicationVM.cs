using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessApplicationsProject.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();
            _pages.Add(new BandsVM());
            _pages.Add(new ContactsVM());
            _pages.Add(new GeneralSettingsVM());
            _pages.Add(new LineUpVM());
            _pages.Add(new TicketsVM());
        }
        private IPage _currentPage;

        public IPage CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private ObservableCollection<IPage> _pages;

        public ObservableCollection<IPage> Pages
        {
            get { return _pages; }
            set { _pages = value; OnPropertyChanged("Pages"); }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
