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
using System.Data.Common;
using System.Data;

namespace BusinessApplicationsProject.Model
{
    class BandsToGenre
    {
        private string _bandId;

        public string BandId
        {
            get { return _bandId; }
            set { _bandId = value; }
        }
        private string _genreId;

        public string GenreId
        {
            get { return _genreId; }
            set { _genreId = value; }
        }
        private string _genreName;

        public string GenreName
        {
            get { return _genreName; }
            set { _genreName = value; }
        }
        
        


        internal static ObservableCollection<Genre> GetData(string id)
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();
            DbParameter ID = Database.AddParameter("Value", "%" + id + "%");
            //}
            //else
            //{
            //    ID = Database.AddParameter("Value", selected.Id);
            //}
                string sql = "Select * From BandsToGenre where BandID like @Value";
                DbDataReader reader = Database.GetData(sql,ID);
                while (reader.Read())
                {
                   
                    lijst.Add(VerwerkRij(reader));

                }
            
            return lijst;

        }
        
        private static Genre VerwerkRij(IDataRecord reader)
        {

            Genre nieuw = new Genre();
            
            nieuw.Id = reader["GenreID"].ToString();
            nieuw.Name = GetGenre(reader["GenreID"].ToString());
            return nieuw;
        }

        private static string GetGenre(string p)
        {
            string g;
            DbParameter ID = Database.AddParameter("Value", "%" + p + "%");
            string sql = "Select * From Genres where ID like @Value";
            g = GetGenreName(sql, ID);
            return g;
        }

        private static string GetGenreName(string sql, DbParameter ID)
        {
            string name = null;
            DbDataReader reader = Database.GetData(sql, ID);
            while (reader.Read())
            {
                name = reader["Name"].ToString();
            }
            return name;
        }
        /*nieuwContact.JobRole = GetJobRole(reader["ContactpersonType"].ToString());
        private static ContactpersonType GetJobRole(string id)
        {
            ContactpersonType jr = new ContactpersonType();
            jr.Id = id;

            DbParameter ID = Database.AddParameter("Value", "%" + id + "%");
            string sql = "Select * From ContactpersonType where Name like @Value";
            jr.Name = GetJobName(sql,ID);
            return jr;
        }

        private static string GetJobName(string sql, DbParameter ID)
        {
            string name = null;
            DbDataReader reader = Database.GetData(sql, ID);
            while (reader.Read())
            {
                name = reader["Name"].ToString();
            }
            return name;

        }*/
    }
}
