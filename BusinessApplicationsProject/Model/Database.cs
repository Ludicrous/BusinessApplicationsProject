using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplicationsProject.Model
{
    class Database
    {

        //* 0. info ophalen uit app.config


        private static ConnectionStringSettings ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MijnConnectionString"]; }

        }

        //* 1. connectie leggen met database
        private static DbConnection GetConnection()
        {
            DbConnection con = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionString.ConnectionString;
            con.Open();
            return con;
        }
        //* 2. connectie sluiten met database
        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }
        //* 3. command opstellen <--- SQL string + parameters
        //keyword params ----> als er geen parameters zijn hoeft er niets doorgegeven te worden (enkel een sql
        private static DbCommand Buildcommand(String sql, params DbParameter[] parameters)
        {
            DbCommand command = GetConnection().CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;

        }
        //* 3A. aanmaken van een parameter
        public static DbParameter AddParameter(String naam, Object value)
        {
            DbParameter parameter = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateParameter();
            parameter.ParameterName = naam;
            parameter.Value = value;
            return parameter;
        }
        //* 4A Select-statements: info ophalen
        public static DbDataReader GetData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = Buildcommand(sql, parameters);
            DbDataReader reader = null;
            try
            {
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }
        }
        //* 4B Insert/Delete/Update: database manipuleren
        public static int ModifyData(String sql, params DbParameter[] parameters)
        {
            DbCommand command = Buildcommand(sql, parameters);
            int gewijzigdeRijen = command.ExecuteNonQuery();
            try
            {
                return gewijzigdeRijen;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }
        }
        //* Uitbreiding: transactie (combo van meerdere commands die kan worden geroll-backed bij een fout)

        //* 5. Transactie aanmaken
        public static DbTransaction BeginTransaction()
        {
            DbConnection con = null;
            try
            {
                con = GetConnection();
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        //* 6: Command binnen transactie aanmaken

        private static DbCommand Buildcommand(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            DbCommand command = BeginTransaction().Connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            command.Transaction = trans;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;

        }
        //* 7A Select binnen transactie
        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = Buildcommand(trans, sql, parameters);
            DbDataReader reader = null;
            try
            {
                command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }
        }
        //* 7B Insert/delete/update binnen transactie
        public static int ModifyData(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            DbCommand command = Buildcommand(trans, sql, parameters);
            int gewijzigdeRijen = command.ExecuteNonQuery();
            try
            {
                return gewijzigdeRijen;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }
        }    
    }
}
