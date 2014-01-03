using BusinessApplicationsProject.Model;
using System;
using System.Collections.Generic;
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



        internal static void AddPerformance(Band SelectedBand, Festival SelectedDate, Stage SelectedStage, int Startmin, int Startuur, int Eindmin, int Einduur)
        {
            DbParameter Band = Database.AddParameter("Band", SelectedBand.Id);
            DbParameter Stage = Database.AddParameter("Stage", SelectedStage.Id);
            DateTime From = SelectedDate.Date.AddHours(Startuur).AddMinutes(Startmin);
            DateTime Until = SelectedDate.Date.AddHours(Einduur).AddMinutes(Eindmin);
            DbParameter Start = Database.AddParameter("StartTime", From);
            DbParameter Eind = Database.AddParameter("EndTime", Until);
                    

            string sql = "Insert into Performances (StageID,BandID,StartTime,EndTime) values (@Stage,@Band,@StartTime,@EndTime)";
            Database.ModifyData(sql, Stage,Band,Start,Eind);
        }
    }
}
