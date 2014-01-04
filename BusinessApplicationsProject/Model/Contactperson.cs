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
    class Contactperson:IDataErrorInfo
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _name;

        [Required(ErrorMessage = "De naam is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten bij de naam")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 50 karakters bevatten ")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _company;
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten bij de stad")]
        [StringLength(50, ErrorMessage = "Het bedrijf moet tussen de 3 en 50 karakters bevatten ")]
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        private ContactpersonType _jobRole;

        public ContactpersonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }
        private string _city;

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten bij de stad")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De stad moet tussen de 3 en 50 karakters bevatten ")]
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _email;

        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _phone;
        [Phone]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        



        public static ObservableCollection<Contactperson> GetContacts()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();
            string sql = "Select * From Contactpersons";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                lijst.Add(VerwerkRij(reader));


            }
            return lijst;

        }
        private static Contactperson VerwerkRij(IDataRecord reader)
        {

            Contactperson nieuwContact = new Contactperson();
            nieuwContact.Id = reader["ID"].ToString();
            nieuwContact.Name = reader["Name"].ToString();
            if ((nieuwContact.Company = reader["Company"].ToString()) == null || (nieuwContact.Company = reader["Company"].ToString())=="")
            {
                nieuwContact.Company = "n.v.t.";
            }
            nieuwContact.JobRole = GetJobRole(reader["ContactpersonType"].ToString());
            nieuwContact.City = reader["City"].ToString();
            nieuwContact.Email = reader["Email"].ToString();
            nieuwContact.Phone = reader["Phone"].ToString();
            
            return nieuwContact;
        }
         
        private static ContactpersonType GetJobRole(string id)
        {
            ContactpersonType jr = new ContactpersonType();
            jr.Id = id;

            DbParameter ID = Database.AddParameter("Value", "%" + id + "%");
            string sql = "Select * From ContactpersonType where ID like @Value";
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

        }
        public static ObservableCollection<Contactperson> SearchNameContact(string waarde)
        {
            
            DbParameter Value = Database.AddParameter("Value", "%"+ waarde+"%");
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();
            string sql = "Select * From Contactpersons where Name like @Value";
            DbDataReader reader = Database.GetData(sql, Value);
            while (reader.Read())
            {
                lijst.Add(VerwerkRij(reader));


            }
            return lijst;

        }

        internal static void BewerkContact(Contactperson SelectedCont)
        {
            DbParameter ID = Database.AddParameter("ID",  Int32.Parse(SelectedCont.Id) );
            DbParameter Name = Database.AddParameter("Name",SelectedCont.Name);
            DbParameter JobRole = Database.AddParameter("ContactpersonType", SelectedCont.JobRole.Id);
            DbParameter Company = Database.AddParameter("Company",  SelectedCont.Company);
            //city email phone cellphone
            DbParameter City = Database.AddParameter("City", SelectedCont.City);
            DbParameter Email = Database.AddParameter("Email",  SelectedCont.Email);
            DbParameter Phone = Database.AddParameter("Phone",  SelectedCont.Phone);
            
            string sql = "Update Contactpersons Set Name=@Name, City=@City,Email=@Email,Phone=@Phone,ContactpersonType=@ContactpersonType,Company=@Company where ID =@ID";
            Database.ModifyData(sql, Name, City, Email, Phone, JobRole, Company, ID);
        }

        internal static void DeleteContact(Contactperson SelectedCont)
        {
            if (SelectedCont.Id != null)
            {
                DbParameter ID = Database.AddParameter("ID", Int32.Parse(SelectedCont.Id));
                string sql = "Delete from Contactpersons Where ID=@ID";
                Database.ModifyData(sql, ID);
            }
            
        }

       

        internal static void VoegContactToe(Contactperson SelectedCont)
        {
           
           
            DbParameter Name = Database.AddParameter("Name",SelectedCont.Name);
            DbParameter JobRole = Database.AddParameter("ContactpersonType", SelectedCont.JobRole.Id);
            
            //city email phone cellphone

            DbParameter City = Database.AddParameter("City", SelectedCont.City);
            DbParameter Company = Database.AddParameter("Company", SelectedCont.Company);
            DbParameter Email = Database.AddParameter("Email",  SelectedCont.Email);
            DbParameter Phone = Database.AddParameter("Phone",  SelectedCont.Phone);
            Controleer(City,Company, Phone);
            if (Email.Value == null)
            {
                string sql = "Insert into Contactpersons  (Name,City,Phone,ContactpersonType,Company) Values (@Name,@City,@Phone,@ContactpersonType,@Company)";
                Database.ModifyData(sql, Name, City, Phone, JobRole, Company);
            }
            else
            {
                string sql = "Insert into Contactpersons  (Name,City,Email,Phone,ContactpersonType,Company) Values (@Name,@City,@Email,@Phone,@ContactpersonType,@Company)";
                Database.ModifyData(sql, Name, City, Email, Phone, JobRole, Company);
            }
        
        }

        private static void Controleer(DbParameter City, DbParameter Company, DbParameter Phone)
        {
            if (City.Value == null)
            {
                City.Value = "n.v.t.";
            }
            if (Company.Value == null)
            {
                Company.Value = "n.v.t.";
            }
            if (Phone.Value == null)
            {
                Phone.Value = "";
            }
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





        public bool IsValid()
        {
            if (this == null)
            {
                return false;
            }
            else
            {
                return Validator.TryValidateObject(this, new ValidationContext(this), null, true);
            }
        }





        internal static ObservableCollection<Contactperson> SearchFunction(string waarde)
        {
            DbParameter Value = Database.AddParameter("Value", "%" + waarde + "%");
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();
            string sql = "Select * From Contactpersons where ContactpersonType like @Value";
            DbDataReader reader = Database.GetData(sql, Value);
            while (reader.Read())
            {
                lijst.Add(VerwerkRij(reader));


            }
            return lijst;
        }

        internal static string ZoekId()
        {

            string id = null;
            //Select IDENT_CURRENT('Festival.dbo.Contactpersons')
            string sql = "Select IDENT_CURRENT ('Contactpersons') AS kolom";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {
                int i = Convert.ToInt32(reader["kolom"]);
                id = (i + 1).ToString();

            }
            return id;
        }

        internal static ObservableCollection<Contactperson> SearchName( string SearchName)
        {
            DbParameter Value = Database.AddParameter("Value", "%" + SearchName + "%");
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();
            string sql = "Select * From Contactpersons where Name like @Value";
            DbDataReader reader = Database.GetData(sql, Value);
            while (reader.Read())
            {
                lijst.Add(VerwerkRij(reader));


            }
            return lijst;
        }
    }
}
