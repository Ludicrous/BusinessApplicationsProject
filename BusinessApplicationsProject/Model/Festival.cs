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
    class Festival
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }


        //public static ObservableCollection<Festival> GetDate()
        //{
            
        //    //Festival data = new Festival();
        //    //string sql = "Select * From Dates where ID=";
        //    //DbDataReader reader = Database.GetData(sql);
        //    //while (reader.Read())
        //    //{
        //    //    lijst.Add(VerwerkRij(reader));
        //    //}
        //    //return lijst;
        //}
        public static Festival VerwerkRij(IDataRecord rij)
        {
            Festival nieuwF = new Festival();
            nieuwF.StartDate = DateTime.Parse(rij["StartDate"].ToString());
            nieuwF.EndDate = DateTime.Parse(rij["EndDate"].ToString());
            return nieuwF;

        }
    }
}
