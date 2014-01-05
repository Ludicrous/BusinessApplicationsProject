using GalaSoft.MvvmLight.Command;
using ProjectBussinessApplications.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessApplicationsProject.ViewModel
{
    class TicketsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }
        public TicketsVM()
        {
            Fill();

        }

        private void Fill()
        {
            TicketTs = TicketType.GetTickets();
            Besteld = Ticket.GetOrders();
            Enabled();
        }

        private void Enabled()
        {
            AddNewTTEnabled = true;
            AddTTEnabled = false;
            ListEnabled = true;
            UpdateEnabled = true;
            AddEnabled = false;
            AddNewEnabled = true;
            ListTEnabled = true;
            UpdateTEnabled = true;
            
        }
        private ObservableCollection<Ticket> _besteld;

        public ObservableCollection<Ticket> Besteld
        {
            get { return _besteld; }
            set { _besteld = value; OnPropertyChanged("Besteld"); }
        }

        private ObservableCollection<TicketType> _ticketTs;

        public ObservableCollection<TicketType> TicketTs
        {
            get { return _ticketTs; }
            set { _ticketTs = value; OnPropertyChanged("TicketTs"); }
        }
        private TicketType _selectedTicketType;

        public TicketType SelectedTicketType
        {
            get { return _selectedTicketType; }
            set { _selectedTicketType = value; OnPropertyChanged("SelectedTicketType"); }
        }
        private bool _addNewTTEnabled;

        public bool AddNewTTEnabled
        {
            get { return _addNewTTEnabled; }
            set { _addNewTTEnabled = value; OnPropertyChanged("AddNewTTEnabled"); }
        }
        private bool _addTTEnabled;

        public bool AddTTEnabled
        {
            get { return _addTTEnabled; }
            set { _addTTEnabled = value; OnPropertyChanged("AddTTEnabled"); }
        }
        private bool _listEnabled;

        public bool ListEnabled
        {
            get { return _listEnabled; }
            set { _listEnabled = value; OnPropertyChanged("ListEnabled"); }
        }
        private bool _updateEnabled;

        public bool UpdateEnabled
        {
            get { return _updateEnabled; }
            set { _updateEnabled = value; OnPropertyChanged("UpdateEnabled"); }
        }
        private Ticket _selectedBesteld;

        public Ticket SelectedBesteld
        {
            get { return _selectedBesteld; }
            set { _selectedBesteld = value; OnPropertyChanged("SelectedBesteld"); }
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
        private bool _updateTEnabled;

        public bool UpdateTEnabled
        {
            get { return _updateTEnabled; }
            set { _updateTEnabled = value; OnPropertyChanged("UpdateTEnabled"); }
        }
        private bool _listTEnabled;

        public bool ListTEnabled
        {
            get { return _listTEnabled; }
            set { _listTEnabled = value; OnPropertyChanged("ListTEnabled"); }
        }
        
        
        




        #region Commands
        public ICommand AddNewTTCommand
        {
            get { return new RelayCommand(AddNewTT); }
        }

        private void AddNewTT()
        {
            TicketType nieuw = new TicketType();

            nieuw.Name = "(leeg)";
            nieuw.Id = TicketType.ZoekId();
            nieuw.Aangekocht = 0;
            nieuw.Price = 0;
            nieuw.Totaal = 0;
            nieuw.AvailableTickets = nieuw.Totaal;

            SelectedTicketType = nieuw;
            TicketTs.Add(SelectedTicketType);
            //BandButton = true;
            AddNewTTEnabled = false;
            AddTTEnabled = true;
            ListEnabled = false;
            UpdateEnabled = false;
        }
        public ICommand AddTTCommand
        {

            get { return new RelayCommand(AddTT, () => Regex()); }
        }

        private void AddTT()
        {

            AddTTEnabled = true;
            if (Check())
            {

                TicketType.AddTicketType(SelectedTicketType);
                Fill();
            }



        }

        private bool Check()
        {
            if ((SelectedTicketType.Totaal - SelectedTicketType.Aangekocht) < 0)
            {
                MessageBox.Show("Er zijn al te veel tickets aangekocht om het totaal aantal tickets te kunnen verlagen.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool Regex()
        {
            double nr;
            int txt;
            if (SelectedTicketType != null)
            {
                if ((double.TryParse(SelectedTicketType.Price.ToString(), out nr)) && (int.TryParse(SelectedTicketType.Totaal.ToString(), out txt)))
                {
                    return true;
                }
                else
                {

                    return false;

                }
            }
            else
            {
                return false;

            }

        }
        public ICommand UpdateTTCommand
        {
            get { return new RelayCommand(UpdateTT, () => (SelectedTicketType != null)); }
        }

        private void UpdateTT()
        {
            if (Check())
            {
                TicketType.UpdateTT(SelectedTicketType);
                Fill();
            }

        }
        public ICommand AddNewOrderCommand
        {
            get { return new RelayCommand(AddNewOrder); }
        }

        private void AddNewOrder()
        {
            Ticket nieuw = new Ticket();
            nieuw.Amount = 0;
            nieuw.Id = Ticket.ZoekId();
            nieuw.TicketType = Ticket.GetTicketType("1");
            nieuw.Ticketholder = "(leeg)";
            nieuw.TicketholderEmail = "";
            nieuw.Text = nieuw.Ticketholder + " | " + nieuw.TicketType.Name + " | €" + nieuw.Price.ToString();
            SelectedBesteld = nieuw;
            Besteld.Add(SelectedBesteld);
            AddEnabled = true;
            AddNewEnabled = false;
            ListTEnabled = false;
            UpdateTEnabled = false;
        }
        public ICommand CancelOrderCommand
        { get { return new RelayCommand(CancelOrder); } }

        private void CancelOrder()
        {
            Besteld.Remove(SelectedBesteld);
            AddEnabled = false;
            AddNewEnabled = true;
            ListTEnabled = true;
            UpdateTEnabled = true;
        }
        public ICommand SendOrderCommand
        {
            get { return new RelayCommand(SendOrder); }
        }
        private void SendOrder()
        {
            if (SelectedBesteld.Amount > SelectedBesteld.TicketType.AvailableTickets)
            {
                MessageBox.Show("Er zijn te weinig tickets beschikbaar voor de bestelling.");
            }
            else
            {
                Ticket.AddTicket(SelectedBesteld);
                TicketType.AdjustTicketType(SelectedBesteld);
                Fill();
            }
            
            
        }
        public ICommand UpdateOrderCommand
        {
            get { return new RelayCommand(UpdateOrder,() => SelectedBesteld != null); }
        }

        private void UpdateOrder()
        {
            if (SelectedBesteld.Amount > SelectedBesteld.TicketType.AvailableTickets)
            { MessageBox.Show("Er zijn te weinig tickets beschikbaar voor de bestelling"); }
            else
            {
                TicketType.UpdateTicketType(SelectedBesteld);
                Ticket.UpdateTicketOrder(SelectedBesteld);
                Fill();
            }
            
        }
        public ICommand DeleteOrderCommand
        {
            get { return new RelayCommand(DeleteOrder, () => SelectedBesteld != null); }
        }

        private void DeleteOrder()
        {
            TicketType.UpdateBestelling(SelectedBesteld);
            Ticket.RemoveTicket(SelectedBesteld);
            Fill();
        }
        public ICommand PrintTicketCommand
        {
            get { return new RelayCommand(PrintTicket, () => Besteld != null); }
        }

        private void PrintTicket()
        {
            Ticket.Print(Besteld);
        }
        #endregion

        

        
    }
}
