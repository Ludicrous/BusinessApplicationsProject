using BusinessApplicationsProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBussinessApplications.Models
{
    class TicketType
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        private int _availableTickets;

        public int AvailableTickets
        {
            get { return _availableTickets; }
            set { _availableTickets = value; }
        }
        private int _totaal;

        public int Totaal
        {
            get { return _totaal; }
            set { _totaal = value; }
        }
        private int _aangekocht;

        public int Aangekocht
        {
            get { return _aangekocht; }
            set { _aangekocht = value; }
        }
        
        
        
        


        internal static ObservableCollection<TicketType> GetTickets()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();
            string sql = "Select * From TicketType";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {

                lijst.Add(VerwerkRij(reader));

            }
            return lijst;
        }
        public static TicketType VerwerkRij(IDataRecord reader)
        {

            TicketType nieuw = new TicketType();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Name = reader["Name"].ToString();
            nieuw.Price = double.Parse(reader["Price"].ToString());
            nieuw.Totaal = (int)reader["Total"];
            nieuw.Aangekocht = (int)reader["Bought"];
            nieuw.AvailableTickets = nieuw.Totaal - nieuw.Aangekocht;



            return nieuw;
        }

        internal static string ZoekId()
        {
            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('TicketType') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;
        }

        internal static void AddTicketType(TicketType s)
        {
            

            DbParameter Name = Database.AddParameter("Name", s.Name);
            DbParameter Totaal = Database.AddParameter("Total", s.Totaal);
            DbParameter Price = Database.AddParameter("Price", s.Price);
            DbParameter Bought = Database.AddParameter("Bought", s.Aangekocht);
            string sql = "Insert into TicketType (Name,Total,Price,Bought) values (@Name,@Total,@Price,@Bought)";
            Database.ModifyData(sql, Name, Totaal,Price,Bought);
        }

        internal static void UpdateTT(TicketType s)
        {
            DbParameter Id = Database.AddParameter("Id", s.Id);
            DbParameter Name = Database.AddParameter("Name", s.Name);
            DbParameter Totaal = Database.AddParameter("Total", s.Totaal);
            DbParameter Price = Database.AddParameter("Price", s.Price);
            DbParameter Bought = Database.AddParameter("Bought", s.Aangekocht);
            string sql = "Update TicketType Set Name=@Name, Total=@Total, Price=@Price, Bought=@Bought where ID=@Id";
            Database.ModifyData(sql, Name, Totaal, Price, Bought, Id);
        }

        internal static void UpdateTicketType(Ticket SelectedBesteld)
        {
            Ticket t = new Ticket();
            DbParameter Id = Database.AddParameter("Value",SelectedBesteld.Id);
            string sql = "Select * from Tickets where ID like @Value";
            t = Ticket.SearchTicket(sql, Id);
            UpdateBestelling(t);
        }

        private static void UpdateBestelling(Ticket t)
        {
            DbParameter Id = Database.AddParameter("Id", t.TicketType.Id);
            DbParameter NewAmount = Database.AddParameter("Amount", t.TicketType.Aangekocht - t.Amount);
            string sql ="Update TicketType Set Bought=@Amount where ID=@Id";
            Database.ModifyData(sql,NewAmount,Id);
            
            
        }


        internal static void AdjustTicketType(Ticket t)
        {
            DbParameter Id = Database.AddParameter("Id", t.TicketType.Id);
            DbParameter NewAmount = Database.AddParameter("Amount", t.TicketType.Aangekocht + t.Amount);
            string sql = "Update TicketType Set Bought=@Amount where ID=@Id";
            Database.ModifyData(sql, NewAmount, Id);
        }
    }
}
