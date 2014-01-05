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
    class LineUp
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        private DateTime _from;

        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }
        private DateTime _until;

        public DateTime Until
        {
            get { return _until; }
            set { _until = value; }
        }
        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }
        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }
        private string _totInfo;

        public string TotInfo
        {
            get { return _totInfo; }
            set { _totInfo = value; }
        }
        



        internal static void AddPerformance(Band SelectedBand, Festival SelectedDate, Stage SelectedStage, int Startmin, int Startuur, int Eindmin, int Einduur)
        {
            DbParameter Band = Database.AddParameter("Band", SelectedBand.Id);
            DbParameter Stage = Database.AddParameter("Stage", SelectedStage.Id);
            DateTime From = SelectedDate.Date.AddHours(Startuur).AddMinutes(Startmin);
            DateTime Until = SelectedDate.Date.AddHours(Einduur).AddMinutes(Eindmin);
            DbParameter Start = Database.AddParameter("StartTime", From);
            DbParameter Eind = Database.AddParameter("EndTime", Until);
            DbParameter Date = Database.AddParameter("Date", SelectedDate.Date);
                    

            string sql = "Insert into Performances (StageID,BandID,StartTime,EndTime,Date) values (@Stage,@Band,@StartTime,@EndTime,@Date)";
            Database.ModifyData(sql, Stage,Band,Start,Eind, Date);
        }

        
        private static LineUp VerwerkRij(IDataRecord reader)
        {

            LineUp nieuw = new LineUp();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Band = GetBand(reader["BandID"].ToString());
            nieuw.Stage = GetStage(reader["StageID"].ToString());
            nieuw.From = (DateTime)reader["StartTime"];
            nieuw.Until = (DateTime)reader["EndTime"];
            nieuw.TotInfo = nieuw.Band.Name + " | " + nieuw.Stage.Name + " | " + nieuw.From.ToShortTimeString() +" - " + nieuw.Until.ToShortTimeString();
            
           



            return nieuw;
           
        }

        private static Stage GetStage(string p)
        {
            Stage s = new Stage();
            s.Id = p;
            DbParameter ID = Database.AddParameter("Value", "%" + p + "%");
            string sql = "Select * from Stage where ID like @Value";
            s.Name = GetName(sql, ID);
            return s;
        }

       

        

        private static Band GetBand(string p)
        {
            Band b = new Band();
            b.Id = p;
            DbParameter ID = Database.AddParameter("Value", "%" + p + "%");
            string sql = "Select * from Bands where ID like @Value";
            b.Name = GetName(sql, ID);
            return b;
        }

        private static string GetName(string sql, DbParameter ID)
        {
            string name = null;
            DbDataReader reader = Database.GetData(sql, ID);
            while (reader.Read())
            {
                name = reader["Name"].ToString();
            }
            return name;
        }
        /*internal static ObservableCollection<Band> GetBands()
        {
             
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();
            string sql = "Select * From Bands";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
               
                lijst.Add(VerwerkRij(reader));

            }
            return lijst;

        }*/

        internal static ObservableCollection<LineUp> GetPerformances(Festival SelectedDate, Models.Stage SelectedStage)
        {
            ObservableCollection<LineUp> lijst = new ObservableCollection<LineUp>();
            DbParameter Date = Database.AddParameter("Date", SelectedDate.Date);
            DbParameter Stage = Database.AddParameter("Stage", SelectedStage.Id);
            string sql = "Select * From Performances where Date=@Date AND StageID=@Stage";
            DbDataReader reader = Database.GetData(sql,Date,Stage);
            while (reader.Read())
            {

                lijst.Add(VerwerkRij(reader));

            }
            return lijst;
        }

        internal static void DeleteDate(LineUp SelectedPerf)
        {
            DbParameter Id = Database.AddParameter("Value", SelectedPerf.Id);
            string sql = "Delete from Performances where ID=@Value";
            Database.ModifyData(sql, Id);
        }
    }
}
