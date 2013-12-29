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
    class Genre
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

        internal static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();
            string sql = "Select * From Genres";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                //lijst.Add(VerwerkRij(reader));
                lijst.Add(VerwerkRij(reader));

            }
            return lijst;
        }
        
        private static Genre VerwerkRij(IDataRecord reader)
        {

            Genre nieuw = new Genre();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Name = reader["Name"].ToString();

            return nieuw;
        }



        internal static void AddGenre(Genre SelectedGenre)
        {
            DbParameter Name = Database.AddParameter("Name", SelectedGenre.Name);
            

            string sql = "Insert into Genres (Name) values (@Name)";
            Database.ModifyData(sql, Name);
        }



        internal static void UpdateGenre(Genre SelectedGenre)
        {
            DbParameter Name = Database.AddParameter("Name", SelectedGenre.Name);
            DbParameter ID = Database.AddParameter("ID", SelectedGenre.Id);
            string sql = "Update Genres SET Name = @Name where ID=@ID";
            Database.ModifyData(sql, Name, ID);
        }
    }
}
