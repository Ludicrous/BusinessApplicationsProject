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
        }
        public ICommand CancelOrderCommand
        { get { return new RelayCommand(CancelOrder); } }

        private void CancelOrder()
        {
            Besteld.Remove(SelectedBesteld);
        }
        public ICommand SendOrderCommand
        {
            get { return new RelayCommand(SendOrder); }
        }
        private void SendOrder()
        {
            Ticket.AddTicket(SelectedBesteld);
            TicketType.AdjustTicketType(SelectedBesteld);
            Fill();
            
        }
        public ICommand UpdateOrderCommand
        {
            get { return new RelayCommand(UpdateOrder,() => SelectedBesteld != null); }
        }

        private void UpdateOrder()
        {
            TicketType.UpdateTicketType(SelectedBesteld);
            Ticket.UpdateTicketOrder(SelectedBesteld);
            Fill();
        }
        #endregion

        

        
    }
}
