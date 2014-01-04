using BusinessApplicationsProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBussinessApplications.Models
{
    class Ticket
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _ticketholder;

        public string Ticketholder
        {
            get { return _ticketholder; }
            set { _ticketholder = value; }
        }
        private string _ticketholderEmail;

        public string TicketholderEmail
        {
            get { return _ticketholderEmail; }
            set { _ticketholderEmail = value; }
        }
        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value; }
        }
        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        internal static ObservableCollection<Ticket> GetOrders()
        {
            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();
            string sql = "Select * From Tickets";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {

                lijst.Add(VerwerkRij(reader));

            }
            return lijst;
        }

        private static Ticket VerwerkRij(DbDataReader reader)
        {
            Ticket nieuw = new Ticket();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Ticketholder = reader["Ticketholder"].ToString();
            nieuw.TicketType = GetTicketType(reader["TicketType"].ToString());
            nieuw.Price = double.Parse(reader["TotalPrice"].ToString());
            nieuw.TicketholderEmail = reader["Email"].ToString();
            nieuw.Amount = Int32.Parse(reader["Amount"].ToString());
            nieuw.Text = nieuw.Ticketholder + " | " + nieuw.TicketType.Name + " | €" + nieuw.Price.ToString(); 


            return nieuw;
        }

        public static TicketType GetTicketType(string p)
        {
            TicketType tt = new TicketType();
            DbParameter ID = Database.AddParameter("Value", "%" + p + "%");
            string sql = "Select * From TicketType where ID like @Value";
            tt = GetTicket(sql, ID);
            return tt;
        }
        public static Ticket SearchTicket(string sql, DbParameter Id)
        {
            Ticket t = new Ticket();
            DbDataReader reader = Database.GetData(sql, Id);
            while (reader.Read())
            {
                t.Id = reader["ID"].ToString();
                t.Price = (double)reader["TotalPrice"];
                t.Ticketholder = reader["Ticketholder"].ToString();
                t.TicketholderEmail = reader["Email"].ToString();
                t.TicketType = Ticket.GetTicketType(reader["TicketType"].ToString());
                t.Amount = (int)reader["Amount"];
            }
            return t;
        }

        

        private static TicketType GetTicket(string sql, DbParameter ID)
        {
            TicketType tt = new TicketType();
            DbDataReader reader = Database.GetData(sql, ID);
            while (reader.Read())
            {
                tt.Name = reader["Name"].ToString();
                tt.Id = reader["ID"].ToString();
                tt.Price = (double)reader["Price"];
                tt.Totaal = (int)reader["Total"];
                tt.Aangekocht = (int)reader["Bought"];
            }
            return tt;

        }


        internal static string ZoekId()
        {
            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('Tickets') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;
        }

        internal static void AddTicket(Ticket SelectedBesteld)
        {
            DbParameter TicketHolder = Database.AddParameter("Ticketholder", SelectedBesteld.Ticketholder);
            DbParameter TicketHolderEmail = Database.AddParameter("TicketholderEmail", SelectedBesteld.TicketholderEmail);
            DbParameter TicketType = Database.AddParameter("TicketType", SelectedBesteld.TicketType.Id);
            DbParameter Amount = Database.AddParameter("Amount", SelectedBesteld.Amount);
            DbParameter TotalPrice = Database.AddParameter("TotalPrice", SelectedBesteld.Amount * SelectedBesteld.TicketType.Price);
            string sql = "insert into Tickets(Ticketholder,Email,TicketType,Amount,TotalPrice) Values (@Ticketholder,@TicketholderEmail,@TicketType,@Amount,@TotalPrice)";
            Database.ModifyData(sql, TicketHolder, TicketHolderEmail, TicketType, Amount, TotalPrice);
        }

        internal static void UpdateTicketOrder(Ticket SelectedBesteld)
        {
            UpdateTblTickets(SelectedBesteld);
            UpdateTblTicketType(SelectedBesteld);
            
            
            
        }

        private static void UpdateTblTicketType(Ticket SelectedBesteld)
        {
            DbParameter TTId = Database.AddParameter("Id", SelectedBesteld.TicketType.Id);
            DbParameter Bought = Database.AddParameter("Bought",SelectedBesteld.Amount);
            string sql = "Update TicketType Set Bought=@Bought where ID=@Id";

            Database.ModifyData(sql, Bought, TTId);
        }

        private static void UpdateTblTickets(Ticket SelectedBesteld)
        {
            DbParameter Name = Database.AddParameter("Ticketholder", SelectedBesteld.Ticketholder);
            DbParameter Email = Database.AddParameter("Email", SelectedBesteld.TicketholderEmail);
            DbParameter Type = Database.AddParameter("TicketType", SelectedBesteld.TicketType.Id);
            DbParameter Bought = Database.AddParameter("Bought",  SelectedBesteld.Amount);
            DbParameter TId = Database.AddParameter("TId", SelectedBesteld.Id);
            DbParameter Price = Database.AddParameter("TotalPrice", (int)Bought.Value * SelectedBesteld.TicketType.Price);
            string sql2 = "Update Tickets Set Ticketholder=@Ticketholder, Email=@Email, TicketType=@TicketType, Amount=@Bought, TotalPrice=@TotalPrice where ID=@TId";
            Database.ModifyData(sql2,Name,Email,Type, Bought,Price, TId);
        }
    }
    }

