using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ProjectBussinessApplications.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using BusinessApplicationsProject.View;
using System.ComponentModel.DataAnnotations;

namespace BusinessApplicationsProject.ViewModel
{
    class ContactsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contacts"; }
        }
        public ContactsVM()
        {
            Fill();            
        }

        private  void Fill()
        {
            _contacten = Contactperson.GetContacts();
            Types = ContactpersonType.GetFunction();
            ListEnabled = true;
            ButtonEnabled = true;
            FunctionButtonEnabled = true;
            FunctionListEnabled = true;
            FunctionAdd = false;
            ContactAdd = false;
           
            
            
        }
        #region Props
        private bool _functionButtonEnabled;

        public bool FunctionButtonEnabled
        {
            get { return _functionButtonEnabled; }
            set { _functionButtonEnabled = value; OnPropertyChanged("FunctionButtonEnabled"); }
        }
        
        private ObservableCollection<Contactperson> _contacten;   
        public ObservableCollection<Contactperson> Contacten
        {
            get { return _contacten; }
            set { _contacten = value; OnPropertyChanged("Contacten"); }
            
        }
        private ObservableCollection<ContactpersonType> _types;

        public ObservableCollection<ContactpersonType> Types
        {
            get { return _types; }
            set { _types = value; OnPropertyChanged("Types"); }
            
        }
        
        
        private Contactperson _selectedCont;

        public Contactperson SelectedCont
        {
            get { return _selectedCont; }
            set { _selectedCont = value; OnPropertyChanged("SelectedCont"); }
        }
       
        
        private ContactpersonType _selectedFunctie;

        public ContactpersonType SelectedFunctie
        {
            get { return _selectedFunctie; }
            set { _selectedFunctie = value; OnPropertyChanged("SelectedFunctie"); }
        }
        private bool _listEnabled;

        public bool ListEnabled
        {
            get { return _listEnabled; }
            set { _listEnabled = value;  OnPropertyChanged("ListEnabled");}
        }
        private bool _buttonEnabled;

        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { _buttonEnabled = value; OnPropertyChanged("ButtonEnabled");}
        }
        private bool _contactAdd;

        public bool ContactAdd
        {
            get { return _contactAdd; }
            set { _contactAdd = value; OnPropertyChanged("ContactAdd"); }
        }
        private bool _functionAdd;

        public bool FunctionAdd
        {
            get { return _functionAdd; }
            set { _functionAdd = value; OnPropertyChanged("FunctionAdd"); }
        }
        private bool _functionListEnabled;

        public bool FunctionListEnabled
        {
            get { return _functionListEnabled; }
            set { _functionListEnabled = value; OnPropertyChanged("FunctionListEnabled"); }
        }
        private bool _nameChecked;

        public bool NameChecked
        {
            get { return _nameChecked; }
            set { _nameChecked = value; OnPropertyChanged("NameChecked"); }
        }
        private bool _functionChecked;

        public bool FunctionChecked
        {
            get { return _functionChecked; }
            set { _functionChecked = value; OnPropertyChanged("FunctionChecked"); }
        }
        private bool _mailChecked;

        public bool MailChecked
        {
            get { return _mailChecked; }
            set { _mailChecked = value; OnPropertyChanged("MailChecked"); }
        }
        private bool _phoneChecked;

        public bool PhoneChecked
        {
            get { return _phoneChecked; }
            set { _phoneChecked = value; OnPropertyChanged("PhoneChecked");}
        }
        private bool _companyChecked;

        public bool CompanyChecked
        {
            get { return _companyChecked; }
            set { _companyChecked = value; OnPropertyChanged("CompanyChecked"); }
        }
        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; OnPropertyChanged("SearchName"); }
        }
        private string _searchCompany;

        public string SearchCompany
        {
            get { return _searchCompany; }
            set { _searchCompany = value; OnPropertyChanged("SearchCompany"); }
        }
        private string _searchMail;

        public string SearchMail
        {
            get { return _searchMail; }
            set { _searchMail = value; OnPropertyChanged("SearchMail"); }
        }
        private string _searchPhone;

        public string SearchPhone
        {
            get { return _searchPhone; }
            set { _searchPhone = value; OnPropertyChanged("SearchPhone"); }
        }
        private string _selectedFunctionSearch;

        public string SelectedFunctionSearch
        {
            get { return _selectedFunctionSearch; }
            set { _selectedFunctionSearch = value; OnPropertyChanged("SelectedFunctionSearch"); }
        }
        private ObservableCollection<Contactperson> _gevonden;

        public ObservableCollection<Contactperson> Gevonden
        {
            get { return _gevonden; }
            set { _gevonden = value; OnPropertyChanged("Gevonden"); }
        }
        #endregion
        #region Commands
        #region ICommands
        #endregion
        #region Methods
        #endregion
        public ICommand AddNewFunctionCommand { get { return new RelayCommand(AddNewFunction); } }
        #endregion
        private void AddNewFunction()
        {
            ContactpersonType nieuw = new ContactpersonType();
            nieuw.Name = "(leeg)";
            Types.Add(nieuw);
            FunctionButtonEnabled = false;
            FunctionListEnabled = false;
            FunctionAdd = true;
            SelectedFunctie = nieuw;
            

        }

        public ICommand AnnuleerCommand
        {
            get { return new RelayCommand(Annuleer); }
        }

        private void Annuleer()
        {
            ListEnabled = true;
            ButtonEnabled = true;
            ContactAdd = false;
            Contacten.Remove(SelectedCont);
        }
        public ICommand AnnuleerFCommand
        {
            get { return new RelayCommand(AnnuleerF); }
        }

        private void AnnuleerF()
        {
            Fill();
            Types.Remove(SelectedFunctie);
        }
       
        public ICommand AddContactCommand
        {

            get
            {
                    return new RelayCommand(AddContact); 
            } 
        }

        private void AddContact()
        {
           
                Contactperson.VoegContactToe(SelectedCont);
                ListEnabled = true;
                ButtonEnabled = true;
                ContactAdd = false;
            
            
            
        }
        public ICommand AddFunctionCommand
        { get { return new RelayCommand(AddFunction); } }

        private void AddFunction()
        {
            ContactpersonType.AddFunction(SelectedFunctie);
            Fill();
        }
        public ICommand AddNewContactCommand
        {
            get { return new RelayCommand(AddNewContact); }
        }

        private void AddNewContact()
        {
            Contactperson nieuw = new Contactperson();
            nieuw.Name = "(leeg)";
            nieuw.Id = ZoekId();
            ContactpersonType nieuweT = new ContactpersonType();
            nieuweT.Id = "1";
            nieuweT.Name = "Onbekend";
            SelectedCont = nieuw;
            SelectedCont.JobRole = nieuweT;
           
            Contacten.Add(SelectedCont);
            ListEnabled = false;
            ButtonEnabled = false;
            ContactAdd = true;
            

            
        }

        private string ZoekId()
        {
            int Idnr = 1;
            foreach (Contactperson Cperson in Contacten)
            {
                if (int.Parse(Cperson.Id) >= Idnr)
                {
                    Idnr = int.Parse(Cperson.Id);
                }
            }
            return (Idnr + 1).ToString();
        }
            


        

        public ICommand UpdateContactCommand
        {


            get { return new RelayCommand(UpdateContact, () => { return SelectedCont != null; }); }
        }

        private void UpdateContact()
        {
            Contactperson.BewerkContact(SelectedCont);
        }
        public ICommand DeleteContactCommand
        {
            get { return new RelayCommand(DeleteContact, () => { return SelectedCont != null; }); }
        }

        private void DeleteContact()
        {
            Contactperson.DeleteContact(SelectedCont);
            Contacten.Remove(SelectedCont);
        
        }
        public ICommand SearchCommand {
            get { return new RelayCommand(Search); }
        }

        

        private void Search()
        {
            

            if (CompanyChecked == false && NameChecked == false && PhoneChecked == false && FunctionChecked == false && MailChecked == false)
            {

            }
            else
            {
                SearchContact(CompanyChecked, NameChecked, PhoneChecked, FunctionChecked, MailChecked);
            }
        }
        internal void SearchContact(bool com, bool nam, bool phon, bool fun, bool mail)
        {
            if (com == true)
            {
                 Gevonden=Contactperson.SearchContact("Company",SearchCompany);
               
            }
            else if (nam == true)
            {
                Gevonden = Contactperson.SearchContact("Name", SearchName);
            }
            else if (phon == true)
            {
                Gevonden = Contactperson.SearchContact("Phone", SearchPhone);
            }
            else if (fun == true)
            {
                Gevonden = Contactperson.SearchContact("ContactpersonType", SelectedFunctionSearch); 
            }
            else
            {
                Gevonden = Contactperson.SearchContact("Email", SearchMail);
            }

        }

        
        
        //private void btnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    lstGevonden.Items.Clear();
        //    ZoekTabel();
        //    foreach (Contactperson cp in Contactperson.SearchContact(search, value))
        //    {
        //        lstGevonden.Items.Add(cp);
        //    }


        //}

        //private void ZoekTabel()
        //{
        //    if (rdbName.IsChecked == true)
        //    {
        //        btnSearch.IsEnabled = true;
        //        search = "Name";
        //        value = txtNaamZoeken.Text;

        //    }
        //    else if (rdbFunctie.IsChecked == true)
        //    {
        //        btnSearch.IsEnabled = true;
        //        search = "ContactpersonType";
        //        value = cboFunctiesZoeken.SelectedValue.ToString();
        //    }
        //    else if (rdbBedrijf.IsChecked == true)
        //    {
        //        btnSearch.IsEnabled = true;
        //        search = "Company";
        //        value = txtBedrijfZoeken.Text;
        //    }
        //    else if (rdbPhone.IsChecked == true)
        //    {
        //        btnSearch.IsEnabled = true;
        //        search = "Phone";
        //        value = txtTelefoonZoeken.Text;

        //    }
        //    else if (rdbEmail.IsChecked == true)
        //    {
        //        btnSearch.IsEnabled = true;
        //        search = "Email";
        //        value = txtEmailZoeken.Text;
        //    }
        //    else
        //    {
        //        btnSearch.IsEnabled = false;
        //    }
        //}

        //private void rdbName_Checked(object sender, RoutedEventArgs e)
        //{
        //    btnSearch.IsEnabled = true;
        //}
    }
}
