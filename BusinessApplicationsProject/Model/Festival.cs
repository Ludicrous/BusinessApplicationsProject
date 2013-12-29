using BusinessApplicationsProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectBussinessApplications.Models
{
    class Festival
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public static Festival VerwerkRij(IDataRecord rij)
        {
            Festival nieuwF = new Festival();
            nieuwF.Id = rij["Id"].ToString();
            string s = rij["Date"].ToString();
            nieuwF.Date = Convert.ToDateTime(s);
            return nieuwF;

        }

        internal static ObservableCollection<Festival> GetDates()
        {
            ObservableCollection<Festival> lijst = new ObservableCollection<Festival>();
            string sql = "Select * From Dates";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                //lijst.Add(VerwerkRij(reader));
                lijst.Add(VerwerkRij(reader));

            }
            return lijst;
        }

        internal static void AddDate(DateTime SelectedDate)
        {
            DbParameter Date = Database.AddParameter("Date", SelectedDate.Date.ToShortDateString());
            string sql = "Insert into Dates (Date) Values (@Date)";
            Database.ModifyData(sql, Date);
        }

       

        internal static string ZoekId()
        {
            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('Genres') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;

        }

        internal static void DeleteDate(Festival SD)
        {
            DbParameter Id = Database.AddParameter("Id", SD.Id);
            string sql = "Delete from Dates where Id = @Id";
            Database.ModifyData(sql, Id);
        }
    }
}
