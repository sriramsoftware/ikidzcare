using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DayCareDAL
{
   public class clProgClassCategory
   {
       SqlConnection conn;
       
       #region "Save ProgClassCategory, Dt:11-Aug-2011, Db:V"
       public static bool Save(DayCarePL.ProgClassCategoryProperties objProgClassCategory, Guid SchoolProgramId)
       {
           DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassCategory, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
           SqlConnection conn = clConnection.CreateConnection();
          
           try
           {
               DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassCategory, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
               clConnection.OpenConnection(conn);
               SqlCommand cmd;
               if (objProgClassCategory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
               {
                   cmd = clConnection.CreateCommand("spAddProgClassCategory", conn);
                   cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                   cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objProgClassCategory.CreatedById));
               }
               else
               {
                   cmd = clConnection.CreateCommand("spUpdateProgClassCategory", conn);
               }
               if (!objProgClassCategory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
               {
                   cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objProgClassCategory.Id));
               
               }
               cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objProgClassCategory.SchoolProgramId));
               cmd.Parameters.Add(clConnection.GetInputParameter("@ClassCategoryId", objProgClassCategory.ClassCategoryId));
               cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDateTime", DateTime.Now));
               cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objProgClassCategory.LastModifiedById));
               cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objProgClassCategory.Active));
               cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
               cmd.ExecuteNonQuery();
               if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
               {
                   return true;
               }
               return false;
                            
           }
           catch (Exception ex)
           {
               DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassCategory, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
               return false;

           }
           finally
           {
               clConnection.CloseConnection(conn);
           }

       }
       #endregion

       #region "LoadProgClassCategory ClassCategory, Dt:12-Aug-2011, Db:V"
      public static List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategory(Guid SchoolProgramId,Guid SchoolId)
      {
          DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategory", "Execute LoadProgClassCategory Method", DayCarePL.Common.GUID_DEFAULT);
          SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
          try
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategory", "Debug LoadProgClassCategory Method", DayCarePL.Common.GUID_DEFAULT);
              List<DayCarePL.ProgClassCategoryProperties> lstProgClassCategory = new List<DayCarePL.ProgClassCategoryProperties>();
              DayCarePL.ProgClassCategoryProperties objProgClassCategory;
              if (conn.State == System.Data.ConnectionState.Closed)
              {
                  conn.Open();
              }
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "spGetProgClassCategory";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Connection = conn;
              cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SchoolProgramId));
              cmd.Parameters.Add(new SqlParameter("@SchoolId", SchoolId));
              SqlDataReader dr = cmd.ExecuteReader();
              while (dr.Read())
              {
                  objProgClassCategory = new DayCarePL.ProgClassCategoryProperties();
                  //objProgClassCategory.Id = new Guid(dr["pccId"].ToString());
                  objProgClassCategory.Id= new Guid(dr["Id"].ToString());
                  objProgClassCategory.ClassCategoryName = dr["Name"].ToString();
                  objProgClassCategory.Assign = new Guid(dr["Assign"].ToString());
                  objProgClassCategory.Active = Convert.ToBoolean(dr["Active"].ToString());
                  //objProgClassCategory.pActive = Convert.ToBoolean(dr["AssignActive"].ToString());
                  lstProgClassCategory.Add(objProgClassCategory);
              }
              return lstProgClassCategory;
          }
          catch (Exception ex)
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategory", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
              return null;
          }
          finally
          {
              if (conn.State == System.Data.ConnectionState.Open)
              {
                  conn.Close();
              }
          }
      
      }
       #endregion

      #region "Load Prog Class Category For Child Schedule, Dt:24-Aug-2011, Db: A"
      public static List<DayCarePL.ProgClassCategoryProperties> LoadProgClassCategoryForChildSchedule(Guid SchoolProgramId, Guid SchoolId)
      {
          DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategoryForChildSchedule", "Execute LoadProgClassCategoryForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
          SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["daycareConnectionString"].ToString());
          try
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategoryForChildSchedule", "Debug LoadProgClassCategoryForChildSchedule Method", DayCarePL.Common.GUID_DEFAULT);
              List<DayCarePL.ProgClassCategoryProperties> lstProgClassCategory = new List<DayCarePL.ProgClassCategoryProperties>();
              DayCarePL.ProgClassCategoryProperties objProgClassCategory;
              if (conn.State == System.Data.ConnectionState.Closed)
              {
                  conn.Open();
              }
              SqlCommand cmd = new SqlCommand();
              cmd.CommandText = "spGetProgClassCategoryForChildSchedule";
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Connection = conn;
              cmd.Parameters.Add(new SqlParameter("@SchoolProgramId", SchoolProgramId));
              cmd.Parameters.Add(new SqlParameter("@SchoolId", SchoolId));
              SqlDataReader dr = cmd.ExecuteReader();
              while (dr.Read())
              {
                  objProgClassCategory = new DayCarePL.ProgClassCategoryProperties();
                  objProgClassCategory.Id = new Guid(dr["Id"].ToString());
                  objProgClassCategory.ClassCategoryName = dr["ClassCategoryName"].ToString();
                  lstProgClassCategory.Add(objProgClassCategory);
              }
              return lstProgClassCategory;
          }
          catch (Exception ex)
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clProgClassCategory, "LoadProgClassCategoryForChildSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
              return null;
          }
          finally
          {
              if (conn.State == System.Data.ConnectionState.Open)
              {
                  conn.Close();
              }
          }

      }
      #endregion
   }
}
