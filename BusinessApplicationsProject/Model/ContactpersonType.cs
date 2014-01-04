using BusinessApplicationsProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBussinessApplications.Models
{
    class ContactpersonType:IDataErrorInfo
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

        public static ObservableCollection<ContactpersonType> GetFunction()
        {
            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();
            string sql = "Select * From ContactpersonType";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                lijst.Add(VerwerkRij(reader));


            }
            return lijst;
        }
        private static ContactpersonType VerwerkRij(IDataRecord reader)
        {

            ContactpersonType nieuwContact = new ContactpersonType();
            nieuwContact.Id = reader["ID"].ToString();
            nieuwContact.Name = reader["Name"].ToString();
            return nieuwContact;
        }

        internal static void AddFunction(ContactpersonType SelectedFunctie)
        {
            DbParameter Name = Database.AddParameter("Name", SelectedFunctie.Name);
            string sql = "Insert into ContactpersonType (Name) Values (@Name)";
            Database.ModifyData(sql, Name);
        }

        internal static string ZoekId()
        {

            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('ContactpersonType') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;
        }
        public string Error
        {
            get { return "Model not valid"; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
