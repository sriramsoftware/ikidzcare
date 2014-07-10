using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class  clConnection
    {
        public static void DoConnection()
        {
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            string strConnectionstring = myConfiguration.ConnectionStrings.ConnectionStrings["daycareConnectionString"].ConnectionString;
            DayCareDAL.Properties.Settings.Default["daycareConnectionString"] = strConnectionstring;
            DayCareDAL.Properties.Settings.Default.Save();
        }

       
        public static SqlConnection CreateConnection()
        {
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            string strConnectionstring = myConfiguration.ConnectionStrings.ConnectionStrings["daycareConnectionString"].ConnectionString;
            return new SqlConnection(strConnectionstring);
        }

        public static void OpenConnection(SqlConnection sqlCon)
        {
            if (sqlCon != null)
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
            }
        }

        public static void CloseConnection(SqlConnection sqlCon)
        {
            if (sqlCon != null)
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        public static SqlParameter GetInputParameter(string ParameterName, object ParameterValue)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = ParameterName;
            sqlParameter.Value = ParameterValue;
            sqlParameter.Direction = ParameterDirection.Input;
            return sqlParameter;
        }

        public static SqlParameter GetOutputParameter(string ParameterName, SqlDbType dbType)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = ParameterName;
            sqlParameter.SqlDbType = dbType;
            sqlParameter.Direction = ParameterDirection.Output;
            return sqlParameter;
        }

        public static SqlCommand CreateCommand(string CommandText,SqlConnection sqlCon)
        {
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = CommandText;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.Connection = sqlCon;
            return sqlComm;
        }

        public static SqlCommand CreateTranCommand(string CommandText, SqlConnection sqlCon,SqlTransaction tran)
        {
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.CommandText = CommandText;
            sqlComm.CommandType = CommandType.StoredProcedure;
            sqlComm.Connection = sqlCon;
            sqlComm.Transaction = tran;
            return sqlComm;
        }

        public static DataSet GetDataSet(string sSQL, SortedList paramList)
        {
            // Create Instance of Connection
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            string strConnectionstring = myConfiguration.ConnectionStrings.ConnectionStrings["daycareConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(strConnectionstring);
            SqlCommand cmd = new SqlCommand(sSQL, myConnection);
            int x = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sSQL;
            cmd.Connection = myConnection;
            cmd.CommandTimeout = 0;
            for (x = 0; x <= paramList.Count - 1; x++)
            {
                //cmd.Parameters.Add(paramList.GetKey(x), paramList.GetByIndex(x));
                cmd.Parameters.AddWithValue((String)paramList.GetKey(x), paramList.GetByIndex(x));
            }
            SqlDataAdapter myAdapter = default(SqlDataAdapter);
            myAdapter = new SqlDataAdapter(cmd);

            DataSet result = new DataSet();
            try
            {
                myAdapter.Fill(result);
            }
            catch (Exception ex)
            {
                //ErrorHandler.WriteError(Convert.ToString(ex));
                return result;
            }
            // Return the datareader result
            return result;
        }

        public static DataSet GetDataSet(string sSQL)
        {
            // Create Instance of Connection

            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            string strConnectionstring = myConfiguration.ConnectionStrings.ConnectionStrings["daycareConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(strConnectionstring);
            SqlDataAdapter myAdapter = new SqlDataAdapter(sSQL, myConnection);
            DataSet result = new DataSet();
            try
            {
                myAdapter.Fill(result);
            }
            catch (Exception ex)
            {
                //ErrorHandler.WriteError(Convert.ToString(ex));
                return result;
            }
            // Return the datareader result
            return result;
        }

        

    }
}