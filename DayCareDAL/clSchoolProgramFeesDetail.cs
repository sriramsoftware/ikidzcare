using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clSchoolProgramFeesDetail
    {
        #region "Get SchoolProgram Title And Id,Dt:9-Sep-2011,Db:V"
        public static List<DayCarePL.SchoolProgramFeesDetailProperties> LoadProgram(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from sp in db.SchoolPrograms
                            where sp.Active.Equals(true) && sp.SchoolYearId.Equals(SchoolYearId)
                            select new DayCarePL.SchoolProgramFeesDetailProperties()
                            {
                                SchoolProgram = sp.IsPrimary.Equals(true) ? sp.Title + "-" + "Primary" : sp.Title + "-" + "Secondary",
                                SchoolProgramId = sp.Id
                            }).ToList();

                return data;// lstChildProgEnrollment;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "FeesPeriod And Id, Dt:9-Sep-2011,Db:V"
        public static DayCarePL.FeesPeriodProperties[] LoadFeesPeriod()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "LoadAbsentReason", "Execute LoadAbsentReason Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from f in db.FeesPeriods
                            where f.Active.Equals(1)
                            orderby f.Name ascending
                            select new DayCarePL.FeesPeriodProperties()
                            {
                                Id = f.Id,
                                Name = f.Name,
                            }).ToArray();

                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "LoadAbsentReason", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save SchoolProgramFeesDetail,Dt:9-Sep-2011,Db:V"
        public static bool Save(DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd;
                if (objSchoolProgramFeesDetail.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddSchoolProgramFeesDetail", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objSchoolProgramFeesDetail.LastmodifiedById));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateSchoolProgramFeesDetail", conn);
                }
                if (!objSchoolProgramFeesDetail.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objSchoolProgramFeesDetail.Id));
                }
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", objSchoolProgramFeesDetail.SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Fees", objSchoolProgramFeesDetail.Fees));
                cmd.Parameters.Add(clConnection.GetInputParameter("@FeesPeriodId", objSchoolProgramFeesDetail.FeesPeriodId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EffectiveYearDate", objSchoolProgramFeesDetail.EffectiveYearDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EffectiveMonthDay", objSchoolProgramFeesDetail.EffectiveMonthDay));
                cmd.Parameters.Add(clConnection.GetInputParameter("@EffectiveWeekDay", objSchoolProgramFeesDetail.EffectiveWeekDay));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objSchoolProgramFeesDetail.LastmodifiedById));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@LateFeesCharge", objSchoolProgramFeesDetail.LateFeesCharge));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@Result", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@Result"].Value))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolProgram, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
        }
        #endregion

        #region "Load SchoolProgram Fees Detail,Dt:9-Sep-2011,Db:V"
        public static List<DayCarePL.SchoolProgramFeesDetailProperties> LoadSchoolProgramFeesDetail(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.SchoolProgramFeesDetailProperties> lstSchoolProgramFeesDetail = new List<DayCarePL.SchoolProgramFeesDetailProperties>();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollment, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.SchoolProgramFeesDetailProperties objSchoolProgramFeesDetail = null;
                var data = db.spGetSchoolProgramFeesDetail(SchoolProgramId);
                foreach (var d in data)
                {
                    objSchoolProgramFeesDetail = new DayCarePL.SchoolProgramFeesDetailProperties();
                    objSchoolProgramFeesDetail.SchoolProgram = d.ProgramTitle;
                    objSchoolProgramFeesDetail.FeesPeriod = d.Name;
                    objSchoolProgramFeesDetail.SchoolProgramId = d.SchoolProgramId;
                    objSchoolProgramFeesDetail.FeesPeriodId = d.FeesPeriodId;
                    objSchoolProgramFeesDetail.Id = d.Id;
                    objSchoolProgramFeesDetail.Fees = d.Fees;
                   // objSchoolProgramFeesDetail.LateFeesCharge = d.LateFeesCharge;
                    objSchoolProgramFeesDetail.EffectiveWeekDay = Convert.ToInt32(d.EffectiveWeekDay == null ? null : d.EffectiveWeekDay);
                    objSchoolProgramFeesDetail.EffectiveMonthDay = Convert.ToInt32(d.EffectiveMonthDay == null ? null : d.EffectiveMonthDay);
                    if (d.EffectiveYearDate != null)
                    {
                        objSchoolProgramFeesDetail.EffectiveYearDate = d.EffectiveYearDate.Value;
                    }
                    lstSchoolProgramFeesDetail.Add(objSchoolProgramFeesDetail);
                }
                return lstSchoolProgramFeesDetail;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check SchoolProgramID and FeesPeriodId,Dt:13-9-2011,Db:V"
        public static bool CheckSchoolProgramIdAndFeesPeriodId(Guid SchoolProgramId, Guid FeesPeriodId)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollment, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SortedList sl = new SortedList();
            sl.Add("@SchoolProgramId", SchoolProgramId);
            sl.Add("@FeesPeriodId", FeesPeriodId);
            DataSet ds = new DataSet();
            ds = clConnection.GetDataSet("spGetCheckSchoolProgramIdAndFeesPeriodId", sl);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Result"].ToString()) > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        #endregion

        #region "Delete Fees and FeesPeriod ,Dt:13-Sep-2011,Db:V"
        public static bool Delete(Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);
                SchoolProgramFeesDetail DBSchoolProgram = db.SchoolProgramFeesDetails.FirstOrDefault(s => s.Id.Equals(Id));
                if (DBSchoolProgram != null)
                {
                    db.SchoolProgramFeesDetails.DeleteOnSubmit(DBSchoolProgram);
                    db.SubmitChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyPayment, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Check Duplicate EffectiveWeekDay ,dt:16-Sep-2011,Db:V"
        public static bool CheckDuplicateEffectiveWeekDay(Guid SchoolProgramFeesDetailId, Guid FeesPeriodId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolProgram, "CheckDuplicateEffectiveWeekDay", "Execute CheckDuplicateEffectiveWeekDay Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolProgram, "CheckDuplicateEffectiveWeekDay", "Debug CheckDuplicateEffectiveWeekDay Method", DayCarePL.Common.GUID_DEFAULT);
                int count = 0;
                if (SchoolProgramFeesDetailId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.SchoolProgramFeesDetails
                             where s.FeesPeriodId.Equals(FeesPeriodId) && s.SchoolProgramId.Equals(SchoolProgramId) && !s.Id.Equals(SchoolProgramFeesDetailId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.SchoolProgramFeesDetails
                             where s.FeesPeriodId.Equals(FeesPeriodId) && s.SchoolProgramId.Equals(SchoolProgramId) && !s.Id.Equals(SchoolProgramFeesDetailId)
                             select s).Count();
                }
                if (count > 0)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRelationship, "CheckDuplicateRelationshipName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

       
    }
}
