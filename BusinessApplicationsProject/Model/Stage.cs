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
    class Stage
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

        internal static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> lijst = new ObservableCollection<Stage>();
            string sql = "Select * From Stage";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                //lijst.Add(VerwerkRij(reader));
                lijst.Add(VerwerkRij(reader));

            }
            return lijst; 
        }
        
        
        private static Stage VerwerkRij(IDataRecord reader)
        {

            Stage nieuw = new Stage();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Name = reader["Name"].ToString();

            return nieuw;
        }

        internal static string ZoekId()
        {
            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('Stage') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;

        }

        internal static void UpdateGenre(Stage SelectedStage)
        {
            DbParameter Name = Database.AddParameter("Name", SelectedStage.Name);
            DbParameter ID = Database.AddParameter("ID", SelectedStage.Id);
            string sql = "Update Stage SET Name = @Name where ID=@ID";
            Database.ModifyData(sql, Name, ID);
        }

        internal static void AddStage(Stage SelectedStage)
        {
            DbParameter Name = Database.AddParameter("Name", SelectedStage.Name);

            string sql = "Insert into Stage (Name) values (@Name)";
            Database.ModifyData(sql, Name);
        }
    }
}
