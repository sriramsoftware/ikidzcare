using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DayCareDAL
{
    public class clChildAbsentHistory
    {
        #region "Load Child Absent History, Dt:23-Aug-2011, DB: A"
        public static List<DayCarePL.ChildAbsentHistoryProperties> LoadChildAbsentHistory(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadChildAbsentHistory", "Execute LoadChildAbsentHistory Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadChildAbsentHistory", "Debug LoadChildAbsentHistory Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory;
                List<DayCarePL.ChildAbsentHistoryProperties> lstChildAbsentHistory = new List<DayCarePL.ChildAbsentHistoryProperties>();

                #region Old Code
                //SortedList sl = new SortedList();
                //sl.Add("@ChildSchoolYearId", ChildSchoolYearId);
                //DataSet ds = clConnection.GetDataSet("spGetChildAbsentHistory", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                //        {
                //            objChildAbsentHistory = new DayCarePL.ChildAbsentHistoryProperties();
                //            objChildAbsentHistory.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                //            objChildAbsentHistory.ChildSchoolYearId = new Guid(ds.Tables[0].Rows[iRow]["ChildSchoolYearId"].ToString());
                //            objChildAbsentHistory.ChildFullName = Convert.ToString(ds.Tables[0].Rows[iRow]["ChildName"]);
                //            objChildAbsentHistory.AbsentReasonId = new Guid(ds.Tables[0].Rows[iRow]["AbsentReasonId"].ToString());
                //            objChildAbsentHistory.AbsentReason = Convert.ToString(ds.Tables[0].Rows[iRow]["AbsentReason"]);
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["StartDate"].ToString()))
                //            {
                //                objChildAbsentHistory.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["StartDate"].ToString());
                //            }
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["EndDate"].ToString()))
                //            {
                //                objChildAbsentHistory.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["EndDate"].ToString());
                //            }

                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Comments"].ToString()))
                //            {
                //                objChildAbsentHistory.Comments = Convert.ToString(ds.Tables[0].Rows[iRow]["Comments"]);
                //            }
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["CreatedDateTime"].ToString()))
                //            {
                //                objChildAbsentHistory.CreatedDateTime = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["CreatedDateTime"]);
                //            }
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["CreatedById"].ToString()))
                //            {
                //                objChildAbsentHistory.CreatedById = new Guid(ds.Tables[0].Rows[iRow]["CreatedById"].ToString());
                //            }
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["LastModifiedDatetime"].ToString()))
                //            {
                //                objChildAbsentHistory.LastModifiedDatetime = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["LastModifiedDatetime"]);
                //            }
                //            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["LastModifiedById"].ToString()))
                //            {
                //                objChildAbsentHistory.LastModifiedById = new Guid(ds.Tables[0].Rows[iRow]["LastModifiedById"].ToString());
                //            }
                //            lstChildAbsentHistory.Add(objChildAbsentHistory);
                //        }
                //    }

                //}
                #endregion

                var data = db.spGetChildAbsentHistory(ChildSchoolYearId);

                foreach (var d in data)
                {
                    objChildAbsentHistory = new DayCarePL.ChildAbsentHistoryProperties();
                    objChildAbsentHistory.Id = d.Id;
                    objChildAbsentHistory.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildAbsentHistory.ChildFullName = d.ChildName;
                    objChildAbsentHistory.AbsentReasonId = d.AbsentReasonId;
                    objChildAbsentHistory.AbsentReason = d.AbsentReason;
                    if (d.StartDate != null)
                    {
                        objChildAbsentHistory.StartDate = d.StartDate;
                    }
                    if (d.EndDate != null)
                    {
                        objChildAbsentHistory.EndDate = d.EndDate;
                    }
                    objChildAbsentHistory.Comments = d.Comments;
                    if (d.CreatedDateTime != null)
                    {
                        objChildAbsentHistory.CreatedDateTime = d.CreatedDateTime;
                    }
                    if (d.CreatedById != null)
                    {
                        objChildAbsentHistory.CreatedById = d.CreatedById;
                    }
                    if (d.LastModifiedDatetime != null)
                    {
                        objChildAbsentHistory.LastModifiedDatetime = d.LastModifiedDatetime;
                    }
                    if (d.LastModifiedById != null)
                    {
                        objChildAbsentHistory.LastModifiedById = d.LastModifiedById;
                    }
                    lstChildAbsentHistory.Add(objChildAbsentHistory);
                }
                return lstChildAbsentHistory;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildAbsentHistory, "LoadChildAbsentHistory", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 23-Aug-2011, DB: A"
        public static bool Save(DayCarePL.ChildAbsentHistoryProperties objChildAbsentHistory)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildAbsentHistory, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildAbsentHistory, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objChildAbsentHistory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildAbsentHistory", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildAbsentHistory.CreatedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildAbsentHistory", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildAbsentHistory.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", objChildAbsentHistory.ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objChildAbsentHistory.Comments));
                cmd.Parameters.Add(clConnection.GetInputParameter("@AbsentReasonId", objChildAbsentHistory.AbsentReasonId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@StartDate", objChildAbsentHistory.StartDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EndDate", objChildAbsentHistory.EndDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildAbsentHistory.LastModifiedById));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));

                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildAbsentHistory, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion
    }
}
