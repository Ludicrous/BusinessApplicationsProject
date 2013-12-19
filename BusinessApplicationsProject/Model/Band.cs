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
    class Band
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
        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        private string _twitter;

        public string Twitter
        {
            get { return _twitter; }
            set { _twitter = value; }
        }
        private string _facebook;

        public string Facebook
        {
            get { return _facebook; }
            set { _facebook = value; }
        }
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }


        internal static ObservableCollection<Band> GetBands()
        {
             
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();
            string sql = "Select * From Bands";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                //lijst.Add(VerwerkRij(reader));
                lijst.Add(VerwerkRij(reader));

            }
            return lijst;

        }
      /*  private static Band VerwerkRij(IDataRecord reader)
        {

            Band nieuw = new Band();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Name = reader["Name"].ToString();
            nieuw.Picture = (byte[])reader["Picture"];
            nieuw.Genres = BandsToGenre.GetData(nieuw.Id);



            return nieuw;
        }
        */

        internal static void VoegBandToe(Band SelectedBand, byte[] NoIM){
        

            byte[] bytes = NoIM;

            DbParameter Name = Database.AddParameter("Name", SelectedBand.Name);
            DbParameter Image;
            if (SelectedBand.Picture != null)
            {
                Image= Database.AddParameter("Picture", SelectedBand.Picture);
            }
            else
            {
                Image = Database.AddParameter("Picture", bytes);
            }
           
            string sql = "Insert into Bands (Name,Picture) values (@Name,@Picture)";
            Database.ModifyData(sql, Name, Image);
        }

        internal static void UpdateBand(Band SelectedBand)
        {
            DbParameter Id = Database.AddParameter("Id", SelectedBand.Id);
            DbParameter Name = Database.AddParameter("Name", SelectedBand.Name);
            DbParameter Picture = Database.AddParameter("Picture", SelectedBand.Picture);
            string sql = "Update Bands Set Name=@Name, Picture = @Picture where ID=@Id";
            Database.ModifyData(sql,Id, Name, Picture);
        }

        internal static string ZoekId()
        {
            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('Bands') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;

        }

        private static string GeefID(DbDataReader reader)
        {
            throw new NotImplementedException();
        }
        private static Band VerwerkRij(IDataRecord reader)
        {

            Band nieuw = new Band();
            nieuw.Id = reader["ID"].ToString();
            nieuw.Name = reader["Name"].ToString();
            nieuw.Picture = (byte[])reader["Picture"];
            nieuw.Genres = BandsToGenre.GetData(nieuw.Id);



            return nieuw;
        }

        internal static void DeleteBand(Band SelectedBand)
        {
            DbParameter Id = Database.AddParameter("Id", SelectedBand.Id);
            string sql = "Delete from Bands where ID = @Id";
            Database.ModifyData(sql, Id);
        }
    }
    }

