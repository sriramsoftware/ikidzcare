using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

namespace DayCareDAL
{
    public class clChilData
    {
        #region "Save ChildData,Dt:19-Aug-2011,Db:V"
        public static Guid Save(DayCarePL.ChildDataProperties objChildData)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildData, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            Guid result = new Guid();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildData, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildData", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildData.CreatedById));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", objChildData.ChildSchoolYearId));
                    //cmd.Parameters.Add(clConnection.GetInputParameter("@ChildDataId", objChildData.ChildDataId));

                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildData", conn);
                }
                if (!objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildData.Id));
                }

                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", objChildData.ChildFamilyId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@FirstName", objChildData.FirstName));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastName", objChildData.LastName));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Gender", objChildData.Gender));
                cmd.Parameters.Add(clConnection.GetInputParameter("@DOB", objChildData.DOB));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SocSec", objChildData.SocSec));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Photo", objChildData.Photo));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objChildData.Comments));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objChildData.Active));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildData.LastModifiedById));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDateTime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetOutputParameter("@Id", SqlDbType.UniqueIdentifier));
                }
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        if (!cmd.Parameters["@Id"].Value.Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            result = new Guid(cmd.Parameters["@Id"].Value.ToString());
                        }
                        else
                        {
                            result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                        }
                    }
                    else
                    {
                        result = objChildData.Id;
                    }
                }
                else
                {
                    result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                }


            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildData, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = new Guid(DayCarePL.Common.GUID_DEFAULT);
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region "Load ChildDataList Dt:23-Aug-2011,Db:V"
        public static List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildData, "LoadChildData", "Execute LoadChildData Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildData, "LoadChildData", "Debug LoadChildData Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildDataProperties objChlidata = null;
                List<DayCarePL.ChildDataProperties> lstChildData = new List<DayCarePL.ChildDataProperties>();
                SortedList sl = new SortedList();
                sl.Add("@SchoolID", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@ChildFamilyId", ChildFamilyId);
                DataSet ds = clConnection.GetDataSet("spGetChildDataList", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                    {
                        objChlidata = new DayCarePL.ChildDataProperties();
                        objChlidata.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                        objChlidata.ChildFamilyId = new Guid(ds.Tables[0].Rows[iRow]["ChildFamilyId"].ToString());
                        objChlidata.FirstName = ds.Tables[0].Rows[iRow]["FirstName"].ToString();
                        objChlidata.LastName = ds.Tables[0].Rows[iRow]["LastName"].ToString();
                        objChlidata.FullName = objChlidata.FirstName + " " + objChlidata.LastName;
                        objChlidata.Gender = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["Gender"].ToString());
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["DOB"].ToString()))
                        {
                            objChlidata.DOB = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["DOB"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SocSec"].ToString()))
                        {
                            objChlidata.SocSec = ds.Tables[0].Rows[iRow]["SocSec"].ToString();
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Photo"].ToString()))
                        {
                            objChlidata.Photo = ds.Tables[0].Rows[iRow]["Photo"].ToString();
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Comments"].ToString()))
                        {
                            objChlidata.Comments = ds.Tables[0].Rows[iRow]["Comments"].ToString();
                        }
                        objChlidata.Active = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["ChildSchoolYearActive"].ToString());
                        //objChlidata.FamilyName = ds.Tables[0].Rows[iRow]["FamilyName"].ToString();

                        lstChildData.Add(objChlidata);
                        objChlidata = null;
                    }

                }
                return lstChildData;


            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildData, "LoadChildData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion

        #region "Load ChildDataListByID, dt:23-Aug-2011,Db:V"
        public static DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildData, "LoadChildDataId", "Execute LoadChildDataId Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildData, "LoadChildDataId", "Debug LoadChildDataId Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildDataProperties objChildDataId = new DayCarePL.ChildDataProperties();
                SortedList sl = new SortedList();
                sl.Add("@Id", ChildDataId);
                sl.Add("@SchoolId", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                DataSet ds = clConnection.GetDataSet("spGetChildDataListById", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objChildDataId.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                        objChildDataId.ChildFamilyId = new Guid(ds.Tables[0].Rows[0]["ChildFamilyId"].ToString());
                        objChildDataId.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        objChildDataId.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        // objChildDataId.FullName = objChildDataId.FirstName + " " + objChildDataId.LastName;
                        objChildDataId.Gender = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gender"].ToString());
                        objChildDataId.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SocSec"].ToString()))
                        {
                            objChildDataId.SocSec = ds.Tables[0].Rows[0]["SocSec"].ToString();
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Photo"].ToString()))
                        {
                            objChildDataId.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Comments"].ToString()))
                        {
                            objChildDataId.Comments = ds.Tables[0].Rows[0]["Comments"].ToString();
                        }
                        objChildDataId.Active = Convert.ToBoolean(ds.Tables[0].Rows[0]["ChildSchoolYearActive"].ToString());
                    }
                }
                return objChildDataId;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildData, "LoadChildDataId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion
    }
}
