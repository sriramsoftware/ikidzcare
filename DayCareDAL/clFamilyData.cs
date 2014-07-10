using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clFamilyData
    {
        #region "Save, Dt: 19-Aug-2011, DB: A"
        //public static Guid Save(DayCarePL.ChildFamilyProperties objChildFamily)
        //{
        //    DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
        //    clConnection.DoConnection();
        //    SqlConnection conn = clConnection.CreateConnection();
        //    SqlTransaction tran=null;
        //    Guid result = new Guid();
        //    bool FamilyResult = false;
        //    try
        //    {
                //DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                //clConnection.OpenConnection(conn);
                //tran = conn.BeginTransaction();
                //SqlCommand cmd;
                //if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                //{
                //    cmd = clConnection.CreateCommand("spAddChildFamily", conn);
                //    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                //    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildFamily.CreatedById));
                //}
                //else
                //{
                //    cmd = clConnection.CreateCommand("spUpdateChildFamily", conn);
                //    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildFamily.Id));
                //    //cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", objFamilyData.ChildFamilyId));
                //}
                ////ChildFamily
                //cmd.Transaction = tran;
                //cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", objChildFamily.SchoolId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@FamilyTitle", objChildFamily.FamilyTitle));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@UserName", objChildFamily.UserName));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Password", objChildFamily.Password));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Code", objChildFamily.Code));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Address1", objChildFamily.Address1));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Address2", objChildFamily.Address2));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@City", objChildFamily.City));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@StateId", objChildFamily.StateId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Zip", objChildFamily.Zip));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@HomePhone", objChildFamily.HomePhone));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objChildFamily.Comments));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@MsgDisplayed", objChildFamily.MsgDisplayed));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@MsgStartDate", objChildFamily.MsgStartDate));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@MsgEndDate", objChildFamily.MsgEndDate));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@MsgActive", objChildFamily.MsgActive));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildFamily.LastModifiedById));
                //cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                //if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                //{
                //    cmd.Parameters.Add(clConnection.GetOutputParameter("@Id", SqlDbType.UniqueIdentifier));
                //}
                //cmd.ExecuteNonQuery();

                //if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                //{
                //    if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                //    {
                //        if (!cmd.Parameters["@Id"].Value.Equals(DayCarePL.Common.GUID_DEFAULT))
                //        {
                //            result = new Guid(cmd.Parameters["@Id"].Value.ToString());
                //        }
                //        else
                //        {
                //            result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                //        }
                //    }
                //    else
                //    {
                //        result = objChildFamily.Id;
                //    }
                //}
                //else
                //{
                //    result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                //}
                ////FamilyData
                //if (!result.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                //{
                //    foreach (DayCarePL.FamilyDataProperties objFamilyData in objChildFamily.lstFamily)
                //    {
                //        if (objFamilyData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                //        {
                //            cmd = clConnection.CreateCommand("spAddFamilyData", conn);
                //            cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                //            cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildFamily.CreatedById));
                //        }
                //        else
                //        {
                //            cmd = clConnection.CreateCommand("spUpdateFamilyData", conn);
                //            cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objFamilyData.Id));
                //        }
                //        cmd.Transaction = tran;
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@RelationShipId", objFamilyData.RelationShipId));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", result));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@FirstName", objFamilyData.FirstName));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@LastName", objFamilyData.LastName));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone1", objFamilyData.Phone1));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone1Type", objFamilyData.Phone1Type));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone2", objFamilyData.Phone2));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone2Type", objFamilyData.Phone2Type));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Email", objFamilyData.Email));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@Photo", objFamilyData.Photo));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                //        cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objFamilyData.LastModifiedById));
                //        cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                //        cmd.ExecuteNonQuery();
                //        if (!Convert.ToBoolean(cmd.Parameters["@status"].Value))
                //        {
                //            tran.Rollback();
                //            break;
                //        }
                //        else
                //        {
                //            FamilyResult = true;
                //        }
                //    }
                //    if (FamilyResult)
                //    {
                //        tran.Commit();
                //    }
                //    else
                //    {
                //        tran.Rollback();
                //    }
                //}
                //else
                //{
                //    tran.Rollback();
                //}
        //    }
        //    catch (Exception ex)
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //        result = new Guid(DayCarePL.Common.GUID_DEFAULT);
        //        //if (tran != null)
        //        //{
        //        //    tran.Rollback();
        //        //}
        //    }
        //    finally
        //    {
        //        clConnection.CloseConnection(conn);
        //    }
        //    return result;
        //}
        #endregion

        #region Load Family Data By Id, Dt: 19-Aug-2011, DB: A"
        public static DayCarePL.FamilyDataProperties LoadFamilyDataById(Guid FamilyDataId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyDataById", "Execute LoadFamilyDataById Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyDataById", "Debug LoadFamilyDataById Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.FamilyDataProperties objFamilyData = new DayCarePL.FamilyDataProperties();
                //SortedList sl = new SortedList();
                //sl.Add("@Id", FamilyDataId);
                //sl.Add("@SchoolId", SchoolId);
                //DataSet ds = clConnection.GetDataSet("spGetFamilyDatabyId", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        //Family Data
                //        objFamilyData.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                //        objFamilyData.RelationShipId = new Guid(ds.Tables[0].Rows[0]["RelationShipId"].ToString());
                //        objFamilyData.ChildFamilyId = new Guid(ds.Tables[0].Rows[0]["ChildFamilyId"].ToString());
                //        objFamilyData.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                //        objFamilyData.LastName = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                //        objFamilyData.Gender = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gender"]);
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IdInfo"].ToString()))
                //        {
                //            objFamilyData.IdInfo = Convert.ToString(ds.Tables[0].Rows[0]["IdInfo"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Address1"].ToString()))
                //        {
                //            objFamilyData.Address1 = Convert.ToString(ds.Tables[0].Rows[0]["Address1"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Address2"].ToString()))
                //        {
                //            objFamilyData.Address2 = Convert.ToString(ds.Tables[0].Rows[0]["Address2"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["City"].ToString()))
                //        {
                //            objFamilyData.City = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Zip"].ToString()))
                //        {
                //            objFamilyData.Zip = Convert.ToString(ds.Tables[0].Rows[0]["Zip"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["StateId"].ToString()))
                //        {
                //            objFamilyData.StateId = new Guid(ds.Tables[0].Rows[0]["StateId"].ToString());
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CountryId"].ToString()))
                //        {
                //            objFamilyData.CountryId = new Guid(ds.Tables[0].Rows[0]["CountryId"].ToString());
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MainPhone"].ToString()))
                //        {
                //            objFamilyData.MainPhone = Convert.ToString(ds.Tables[0].Rows[0]["MainPhone"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SecondaryPhone"].ToString()))
                //        {
                //            objFamilyData.SecondaryPhone = Convert.ToString(ds.Tables[0].Rows[0]["SecondaryPhone"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Fax"].ToString()))
                //        {
                //            objFamilyData.Fax = Convert.ToString(ds.Tables[0].Rows[0]["Fax"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Email"].ToString()))
                //        {
                //            objFamilyData.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UserName"].ToString()))
                //        {
                //            objFamilyData.UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Password"].ToString()))
                //        {
                //            objFamilyData.Password = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Photo"].ToString()))
                //        {
                //            objFamilyData.Photo = Convert.ToString(ds.Tables[0].Rows[0]["Photo"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SecurityQuestion"].ToString()))
                //        {
                //            objFamilyData.SecurityQuestion = Convert.ToString(ds.Tables[0].Rows[0]["SecurityQuestion"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SecurityAnswer"].ToString()))
                //        {
                //            objFamilyData.SecurityAnswer = Convert.ToString(ds.Tables[0].Rows[0]["SecurityAnswer"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Code"].ToString()))
                //        {
                //            objFamilyData.Code = Convert.ToString(ds.Tables[0].Rows[0]["Code"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Active"].ToString()))
                //        {
                //            objFamilyData.Active = Convert.ToBoolean(ds.Tables[0].Rows[0]["Active"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Comments"].ToString()))
                //        {
                //            objFamilyData.FamilyDataComments = Convert.ToString(ds.Tables[0].Rows[0]["Comments"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CreatedDateTime"].ToString()))
                //        {
                //            objFamilyData.CreatedDateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedDateTime"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CreatedById"].ToString()))
                //        {
                //            objFamilyData.CreatedById = new Guid(ds.Tables[0].Rows[0]["CreatedById"].ToString());
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastModifiedDatetime"].ToString()))
                //        {
                //            objFamilyData.LastModifiedDatetime = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastModifiedDatetime"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LastModifiedById"].ToString()))
                //        {
                //            objFamilyData.LastModifiedById = new Guid(ds.Tables[0].Rows[0]["LastModifiedById"].ToString());
                //        }

                //        //Child Family
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SchoolId"].ToString()))
                //        //{
                //        //    objFamilyData.SchoolId = new Guid(ds.Tables[0].Rows[0]["SchoolId"].ToString());
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Comments1"].ToString()))
                //        //{
                //        //    objFamilyData.ChildFamilyComments = Convert.ToString(ds.Tables[0].Rows[0]["Comments1"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MsgDisplayed"].ToString()))
                //        //{
                //        //    objFamilyData.MsgDisplayed = Convert.ToString(ds.Tables[0].Rows[0]["MsgDisplayed"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MsgStartDate"].ToString()))
                //        //{
                //        //    objFamilyData.MsgStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["MsgStartDate"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MsgEndDate"].ToString()))
                //        //{
                //        //    objFamilyData.MsgEndDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["MsgEndDate"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MsgActive"].ToString()))
                //        //{
                //        //    objFamilyData.MsgActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["MsgActive"]);
                //        //}
                //    }
                //}

                return objFamilyData;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyDataById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Load Family Data , Dt: 20-Aug-2011, DB: A"
        public static List<DayCarePL.FamilyDataProperties> LoadFamilyData(Guid ChildFamilyId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyData", "Execute LoadFamilyData Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyData", "Debug LoadFamilyData Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.FamilyDataProperties objFamilyData;
                List<DayCarePL.FamilyDataProperties> lstFamilyData = new List<DayCarePL.FamilyDataProperties>();
                //SortedList sl = new SortedList();
                //sl.Add("@ChildFamilyId", ChildFamilyId);
                //sl.Add("@SchoolId", SchoolId);
                //DataSet ds = clConnection.GetDataSet("spGetFamilyDataList", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                //    {
                //        objFamilyData = new DayCarePL.FamilyDataProperties();
                //        //Family Data
                //        objFamilyData.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                //        objFamilyData.RelationShipId = new Guid(ds.Tables[0].Rows[iRow]["RelationShipId"].ToString());
                //        objFamilyData.RelationShipName = Convert.ToString(ds.Tables[0].Rows[iRow]["RelationShipName"].ToString());
                //        objFamilyData.ChildFamilyId = new Guid(ds.Tables[0].Rows[iRow]["ChildFamilyId"].ToString());
                //        objFamilyData.FirstName = Convert.ToString(ds.Tables[0].Rows[iRow]["FirstName"]);
                //        objFamilyData.LastName = Convert.ToString(ds.Tables[0].Rows[iRow]["LastName"]);
                //        objFamilyData.FullName = objFamilyData.FirstName + " " + objFamilyData.LastName;
                //        objFamilyData.Gender = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["Gender"]);
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["IdInfo"].ToString()))
                //        {
                //            objFamilyData.IdInfo = Convert.ToString(ds.Tables[0].Rows[iRow]["IdInfo"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Address1"].ToString()))
                //        {
                //            objFamilyData.Address1 = Convert.ToString(ds.Tables[0].Rows[iRow]["Address1"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Address2"].ToString()))
                //        {
                //            objFamilyData.Address2 = Convert.ToString(ds.Tables[0].Rows[iRow]["Address2"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["City"].ToString()))
                //        {
                //            objFamilyData.City = Convert.ToString(ds.Tables[0].Rows[iRow]["City"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Zip"].ToString()))
                //        {
                //            objFamilyData.Zip = Convert.ToString(ds.Tables[0].Rows[iRow]["Zip"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["StateId"].ToString()))
                //        {
                //            objFamilyData.StateId = new Guid(ds.Tables[0].Rows[iRow]["StateId"].ToString());
                //            objFamilyData.StateName = Convert.ToString(ds.Tables[0].Rows[iRow]["StateName"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["CountryId"].ToString()))
                //        {
                //            objFamilyData.CountryId = new Guid(ds.Tables[0].Rows[iRow]["CountryId"].ToString());
                //            objFamilyData.CountryName = Convert.ToString(ds.Tables[0].Rows[iRow]["CountryName"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MainPhone"].ToString()))
                //        {
                //            objFamilyData.MainPhone = Convert.ToString(ds.Tables[0].Rows[iRow]["MainPhone"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SecondaryPhone"].ToString()))
                //        {
                //            objFamilyData.SecondaryPhone = Convert.ToString(ds.Tables[0].Rows[iRow]["SecondaryPhone"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Fax"].ToString()))
                //        {
                //            objFamilyData.Fax = Convert.ToString(ds.Tables[0].Rows[iRow]["Fax"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Email"].ToString()))
                //        {
                //            objFamilyData.Email = Convert.ToString(ds.Tables[0].Rows[iRow]["Email"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["UserName"].ToString()))
                //        {
                //            objFamilyData.UserName = Convert.ToString(ds.Tables[0].Rows[iRow]["UserName"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Password"].ToString()))
                //        {
                //            objFamilyData.Password = Convert.ToString(ds.Tables[0].Rows[iRow]["Password"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Photo"].ToString()))
                //        {
                //            objFamilyData.Photo = Convert.ToString(ds.Tables[0].Rows[iRow]["Photo"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SecurityQuestion"].ToString()))
                //        {
                //            objFamilyData.SecurityQuestion = Convert.ToString(ds.Tables[0].Rows[iRow]["SecurityQuestion"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SecurityAnswer"].ToString()))
                //        {
                //            objFamilyData.SecurityAnswer = Convert.ToString(ds.Tables[0].Rows[iRow]["SecurityAnswer"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Code"].ToString()))
                //        {
                //            objFamilyData.Code = Convert.ToString(ds.Tables[0].Rows[iRow]["Code"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Active"].ToString()))
                //        {
                //            objFamilyData.Active = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["Active"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Comments"].ToString()))
                //        {
                //            objFamilyData.FamilyDataComments = Convert.ToString(ds.Tables[0].Rows[iRow]["Comments"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["CreatedDateTime"].ToString()))
                //        {
                //            objFamilyData.CreatedDateTime = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["CreatedDateTime"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["CreatedById"].ToString()))
                //        {
                //            objFamilyData.CreatedById = new Guid(ds.Tables[0].Rows[iRow]["CreatedById"].ToString());
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["LastModifiedDatetime"].ToString()))
                //        {
                //            objFamilyData.LastModifiedDatetime = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["LastModifiedDatetime"]);
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["LastModifiedById"].ToString()))
                //        {
                //            objFamilyData.LastModifiedById = new Guid(ds.Tables[0].Rows[iRow]["LastModifiedById"].ToString());
                //        }

                //        //Child Family
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SchoolId"].ToString()))
                //        //{
                //        //    objFamilyData.SchoolId = new Guid(ds.Tables[0].Rows[iRow]["SchoolId"].ToString());
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Comments1"].ToString()))
                //        //{
                //        //    objFamilyData.ChildFamilyComments = Convert.ToString(ds.Tables[0].Rows[iRow]["Comments1"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgDisplayed"].ToString()))
                //        //{
                //        //    objFamilyData.MsgDisplayed = Convert.ToString(ds.Tables[0].Rows[iRow]["MsgDisplayed"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgStartDate"].ToString()))
                //        //{
                //        //    objFamilyData.MsgStartDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["MsgStartDate"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgEndDate"].ToString()))
                //        //{
                //        //    objFamilyData.MsgEndDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["MsgEndDate"]);
                //        //}
                //        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgActive"].ToString()))
                //        //{
                //        //    objFamilyData.MsgActive = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["MsgActive"]);
                //        //}
                //        lstFamilyData.Add(objFamilyData);
                //    }
                //}

                return lstFamilyData;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "LoadFamilyData", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check Code Require, Dt:2-Aug-2011, DB: A"
        public static bool CheckCodeRequire(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "CheckCodeRequire", "Execute CheckCodeRequire Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "CheckCodeRequire", "Debug CheckCodeRequire Method", DayCarePL.Common.GUID_DEFAULT);

                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spIsCodeRequire", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "CheckCodeRequire", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region Chech Duplicate User Name, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateUserName(string UserName, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateUserName", "Execute CheckDuplicateUserName Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateUserName", "Debug CheckDuplicateUserName Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckDuplicateFamilyDataUserName", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@UserName", UserName));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateUserName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region Chech Duplicate Code, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateCode(string Code, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateCode", "Execute CheckDuplicateCode Method", DayCarePL.Common.GUID_DEFAULT);
            SqlConnection conn = clConnection.CreateConnection();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateCode", "Debug CheckDuplicateCode Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                SqlCommand cmd = clConnection.CreateCommand("spCheckDuplicateFamilyDataCode", conn);
                cmd.Parameters.Add(clConnection.GetInputParameter("@Code", Code));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFamilyData, "CheckDuplicateCode", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
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
