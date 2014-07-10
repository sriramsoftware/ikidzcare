using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

namespace DayCareDAL
{
    public class clChildFamily
    {
        #region "Save ChildFamily, Dt:3-Sept-2011, Db:A"
        public static Guid Save(DayCarePL.ChildFamilyProperties objChildFamily)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildFamily, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SqlConnection conn = clConnection.CreateConnection();
            SqlTransaction tran = null;
            Guid result = new Guid();
            bool FamilyResult = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildFamily, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(conn);
                tran = conn.BeginTransaction();
                SqlCommand cmd;
                if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd = clConnection.CreateCommand("spAddChildFamily", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildFamily.CreatedById));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", objChildFamily.SchoolYearId));
                }
                else
                {
                    cmd = clConnection.CreateCommand("spUpdateChildFamily", conn);
                    cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objChildFamily.Id));
                    cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", objChildFamily.SchoolYearId));
                    //cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", objFamilyData.ChildFamilyId));
                }
                //ChildFamily
                cmd.Transaction = tran;
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", objChildFamily.SchoolId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@FamilyTitle", objChildFamily.FamilyTitle));
                cmd.Parameters.Add(clConnection.GetInputParameter("@UserName", objChildFamily.UserName));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Password", objChildFamily.Password));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Code", objChildFamily.Code));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Address1", objChildFamily.Address1));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Address2", objChildFamily.Address2));
                cmd.Parameters.Add(clConnection.GetInputParameter("@City", objChildFamily.City));
                cmd.Parameters.Add(clConnection.GetInputParameter("@StateId", objChildFamily.StateId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Zip", objChildFamily.Zip));
                cmd.Parameters.Add(clConnection.GetInputParameter("@HomePhone", objChildFamily.HomePhone));
                cmd.Parameters.Add(clConnection.GetInputParameter("@Comments", objChildFamily.Comments));

                cmd.Parameters.Add(clConnection.GetInputParameter("@Active", objChildFamily.Active));
                cmd.Parameters.Add(clConnection.GetInputParameter("@MsgDisplayed", objChildFamily.MsgDisplayed));
                cmd.Parameters.Add(clConnection.GetInputParameter("@MsgStartDate", objChildFamily.MsgStartDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@MsgEndDate", objChildFamily.MsgEndDate));
                cmd.Parameters.Add(clConnection.GetInputParameter("@MsgActive", objChildFamily.MsgActive));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objChildFamily.LastModifiedById));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    cmd.Parameters.Add(clConnection.GetOutputParameter("@Id", SqlDbType.UniqueIdentifier));
                }
                cmd.ExecuteNonQuery();

                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    if (objChildFamily.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
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
                        result = objChildFamily.Id;
                    }
                }
                else
                {
                    result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                }
                //FamilyData
                if (!result.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    foreach (DayCarePL.FamilyDataProperties objFamilyData in objChildFamily.lstFamily)
                    {
                        if (objFamilyData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            cmd = clConnection.CreateCommand("spAddFamilyData", conn);
                            cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedDateTime", DateTime.Now));
                            cmd.Parameters.Add(clConnection.GetInputParameter("@CreatedById", objChildFamily.CreatedById));
                        }
                        else
                        {
                            cmd = clConnection.CreateCommand("spUpdateFamilyData", conn);
                            cmd.Parameters.Add(clConnection.GetInputParameter("@Id", objFamilyData.Id));
                        }
                        cmd.Transaction = tran;
                        cmd.Parameters.Add(clConnection.GetInputParameter("@RelationShipId", objFamilyData.RelationShipId));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@ChildFamilyId", result));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@FirstName", objFamilyData.FirstName));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@LastName", objFamilyData.LastName));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone1", objFamilyData.Phone1));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone1Type", objFamilyData.Phone1Type));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone2", objFamilyData.Phone2));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Phone2Type", objFamilyData.Phone2Type));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Email", objFamilyData.Email));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Photo", objFamilyData.Photo));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@Guardian", objFamilyData.GuardianIndex));

                        cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedDatetime", DateTime.Now));
                        cmd.Parameters.Add(clConnection.GetInputParameter("@LastModifiedById", objFamilyData.LastModifiedById));
                        cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                        cmd.ExecuteNonQuery();
                        if (!Convert.ToBoolean(cmd.Parameters["@status"].Value))
                        {
                            // tran.Rollback();
                            FamilyResult = false;
                            break;
                        }
                        else
                        {
                            FamilyResult = true;
                        }
                    }
                    if (FamilyResult)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                        tran.Rollback();
                    }
                }
                else
                {
                    result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildFamily, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            finally
            {
                clConnection.CloseConnection(conn);
            }
            return result;
        }
        #endregion

        #region "Load ChildFamily Dt:31-Aug-2011, Db:V"
        public static List<DayCarePL.ChildFamilyProperties> LoadChildFamily(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamily", "Execute LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {
                clConnection.DoConnection();
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamily", "Debug LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildFamilyProperties objChildFamily = null;
                List<DayCarePL.ChildFamilyProperties> lstChildFamily = new List<DayCarePL.ChildFamilyProperties>();
                SortedList sl = new SortedList();
                sl.Add("@SchoolID", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                DataSet ds = clConnection.GetDataSet("spGetChildFamily", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                    {
                        objChildFamily = new DayCarePL.ChildFamilyProperties();
                        objChildFamily.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                        objChildFamily.ChildFamilyId = objChildFamily.Id;
                        objChildFamily.FamilyTitle = ds.Tables[0].Rows[iRow]["FamilyTitle"].ToString();
                        objChildFamily.UserName = Convert.ToString(ds.Tables[0].Rows[iRow]["UserName"]);
                        if (Convert.ToString(ds.Tables[0].Rows[iRow]["ChildName"]).Length > 0)
                        {
                            objChildFamily.ChildName = "[ " + Convert.ToString(ds.Tables[0].Rows[iRow]["ChildName"]).Substring(0, ds.Tables[0].Rows[iRow]["ChildName"].ToString().LastIndexOf(", ")) + " ]";
                        }
                        objChildFamily.Password = Convert.ToString(ds.Tables[0].Rows[iRow]["Password"]);
                        objChildFamily.Code = Convert.ToString(ds.Tables[0].Rows[iRow]["Code"]);
                        objChildFamily.Address1 = Convert.ToString(ds.Tables[0].Rows[iRow]["Address1"]);
                        objChildFamily.Address2 = Convert.ToString(ds.Tables[0].Rows[iRow]["Address2"]);
                        objChildFamily.City = Convert.ToString(ds.Tables[0].Rows[iRow]["City"]);
                        objChildFamily.StateId = Convert.ToString(ds.Tables[0].Rows[iRow]["StateId"]).Trim() == "" ? new Guid(DayCarePL.Common.GUID_DEFAULT) : new Guid(Convert.ToString(ds.Tables[0].Rows[iRow]["StateId"]));
                        objChildFamily.Zip = Convert.ToString(ds.Tables[0].Rows[iRow]["Zip"]);
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["HomePhone"].ToString()))
                        {
                            objChildFamily.HomePhone = Convert.ToString(ds.Tables[0].Rows[iRow]["HomePhone"]);
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Email"].ToString()))
                        {
                            objChildFamily.Email = Convert.ToString(ds.Tables[0].Rows[iRow]["Email"]);
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["ChildFamilyActive"].ToString()))
                        {
                            objChildFamily.Active = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["ChildFamilyActive"]);
                        }
                        objChildFamily.Comments = Convert.ToString(ds.Tables[0].Rows[iRow]["Comments"]);

                        objChildFamily.MsgDisplayed = ds.Tables[0].Rows[iRow]["MsgDisplayed"].ToString();
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgStartDate"].ToString()))
                        {
                            objChildFamily.MsgStartDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["MsgStartDate"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgEndDate"].ToString()))
                        {
                            objChildFamily.MsgEndDate = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["MsgEndDate"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["MsgActive"].ToString()))
                        {
                            objChildFamily.MsgActive = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["MsgActive"].ToString());
                        }
                        lstChildFamily.Add(objChildFamily);
                        objChildFamily = null;
                    }
                }
                return lstChildFamily;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamily", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;

            }
        }
        #endregion

        #region "Load ChildFamilyById, Dt:31-Aug-2011,Db:V"
        public static DayCarePL.ChildFamilyProperties LoadChildFamilyById(Guid Id, Guid CurrentSchoolyearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamilyById", "Execute LoadChildFamilyById Method", DayCarePL.Common.GUID_DEFAULT);
            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamilyById", "Debug LoadChildFamilyById Method", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.ChildFamilyProperties objChildFamilyId = null;
                SortedList sl = new SortedList();
                sl.Add("@Id", Id);
                sl.Add("@CurrentSchoolyearId", CurrentSchoolyearId);
                objChildFamilyId = new DayCarePL.ChildFamilyProperties();
                objChildFamilyId.lstFamily = new List<DayCarePL.FamilyDataProperties>();
                DayCarePL.FamilyDataProperties objFamilyData;
                var data = db.spGetChildFamilyById(Id, CurrentSchoolyearId);

                foreach (var d in data)
                {
                    if (objChildFamilyId.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objChildFamilyId.Id = d.ChildFamilyId;
                        objChildFamilyId.SchoolId = d.SchoolId;
                        objChildFamilyId.FamilyTitle = d.FamilyTitle;
                        objChildFamilyId.UserName = d.UserName;
                        objChildFamilyId.Password = d.Password;
                        objChildFamilyId.Code = d.Code;
                        objChildFamilyId.Address1 = d.Address1;
                        objChildFamilyId.Address2 = d.Address2;
                        objChildFamilyId.City = d.City;
                        objChildFamilyId.StateId = d.StateId;
                        objChildFamilyId.Zip = d.Zip;
                        objChildFamilyId.HomePhone = d.HomePhone;
                        objChildFamilyId.Comments = d.Comments;
                        objChildFamilyId.Active = d.Active;
                        objChildFamilyId.MsgDisplayed = d.MsgDisplayed;
                        objChildFamilyId.MsgStartDate = d.MsgStartDate;
                        objChildFamilyId.MsgEndDate = d.MsgEndDate;
                        objChildFamilyId.MsgActive = d.MsgActive;
                        objChildFamilyId.CreatedById = d.ChildFamilyCreatedById;
                        objChildFamilyId.CreatedDateTime = d.ChildFamilyCreateDateTime;
                        objChildFamilyId.LastModifiedById = d.ChildFamilyLastModifiedById;
                        objChildFamilyId.LastModifiedDatetime = d.ChildFamilyLastModifiedDateTime;
                    }
                    objFamilyData = new DayCarePL.FamilyDataProperties();
                    objFamilyData.Id = d.FamilyDataId;
                    objFamilyData.RelationShipId = d.RelationShipId;
                    objFamilyData.FirstName = d.FamilyFirstName;
                    objFamilyData.LastName = d.FamilyLastName;
                    objFamilyData.Email = d.Email;
                    objFamilyData.Phone1Type = d.FamilyPhone1Type;
                    objFamilyData.Phone1 = d.FamilyPhone1;
                    objFamilyData.Phone2Type = d.FamilyPhone2Type;
                    objFamilyData.Phone2 = d.FamilyPhone2;
                    objFamilyData.Photo = d.Photo;
                    objFamilyData.GuardianIndex = d.Guardian;
                    objFamilyData.CreatedById = d.FamilyCreatedById;
                    objFamilyData.CreatedDateTime = d.FamilyCreateDateTime;
                    objFamilyData.LastModifiedById = d.FamilyLastModifiedById;
                    objFamilyData.LastModifiedDatetime = d.FamilyLastModifiedDateTime;
                    objChildFamilyId.lstFamily.Add(objFamilyData);
                }
                return objChildFamilyId;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamilyById", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }

        #endregion

        #region "Get ChildDataId"
        public static Guid GetChildDataId(Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildFamily, "LoadChildFamily", "Execute LoadChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                Guid ChildDataId = (from cd in db.ChildDatas
                                    where cd.ChildFamilyId.Equals(ChildFamilyId)
                                    select cd.Id).SingleOrDefault();
                return ChildDataId;
            }
            catch (Exception ex)
            {
                return new Guid(DayCarePL.Common.GUID_DEFAULT);
            }
        }
        #endregion

        #region "Get Families With thier child, Dt:17-Jan-2012, DB: A"
        public static List<DayCarePL.ChildFamilyProperties> GetFamiliesWithChild(Guid SchoolId, Guid SchoolYearId)
        {
            DayCareDataContext db = new DayCareDataContext();
            clConnection.DoConnection();
            try
            {
                List<DayCarePL.ChildFamilyProperties> lstFamilies = new List<DayCarePL.ChildFamilyProperties>();
                DayCarePL.ChildFamilyProperties objFamily;
                var data = db.spGetFamiliesWithChild(SchoolId, SchoolYearId);
                foreach (var d in data)
                {
                    objFamily = new DayCarePL.ChildFamilyProperties();
                    objFamily.Id = d.Id;
                    objFamily.FamilyTitle = d.FamilyTitle;
                    string ChildName = d.ChildName;
                    if (!string.IsNullOrEmpty(ChildName))
                    {
                        objFamily.FamilyTitle += " [ " + ChildName.Substring(0, ChildName.LastIndexOf(", ")) + " ]";
                    }
                    lstFamilies.Add(objFamily);
                }
                return lstFamilies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region "Import All Active Child of school, Dt: 7-Jul-2012, DB: A"
        public int ImportActiveChildFamilyCount { get; set; }
        public bool ImportAllActiveChildFamily(Guid SchoolYearId, Guid SchoolId, Guid OldCurrentSchoolYearId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildFamily, "ImportAllActiveChildFamily", "Execute ImportAllActiveChildFamily Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = dbold;
            db.Transaction = tran;
            ChildFamilySchoolYear DBChildFamilySchoolYear = null;
            try
            {
                this.ImportActiveChildFamilyCount = 0;
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildFamily, "ImportAllActiveChildFamily", "Debug ImportAllActiveChildFamily Method", DayCarePL.Common.GUID_DEFAULT);

                //Guid currentschoolyearid = (from sy in db.SchoolYears
                //                            where sy.CurrentId.Equals(true)
                //                            select sy.Id).SingleOrDefault();

                //List<Guid> lstChildFamily = (from c in db.ChildDatas
                //                       join cf in db.ChildFamilies on c.ChildFamilyId equals cf.Id
                //                       where cf.SchoolId.Equals(SchoolId) && c.Active.Equals(true)
                //                       && !(from cy in db.ChildSchoolYears
                //                            where cy.SchoolYearId.Equals(SchoolYearId)
                //                            select cy.ChildDataId).Contains(c.Id)
                //                       select c.Id).ToList();

                List<Guid> lstChildFamily = (from cf in db.ChildFamilies
                                             join cfsy in db.ChildFamilySchoolYears on cf.Id equals cfsy.ChildFamilyId
                                             where cf.SchoolId.Equals(SchoolId) && cfsy.active.Equals(true) && cfsy.SchoolYearId.Equals(OldCurrentSchoolYearId)
                                             && !(from cy in db.ChildFamilySchoolYears
                                                  where cy.SchoolYearId.Equals(SchoolYearId)
                                                  select cy.ChildFamilyId).Contains(cf.Id)
                                             select cf.Id).ToList();

                //var Data = db.spGetChildIdNotInChildSchoolYear(SchoolId, SchoolYearId);

                foreach (Guid ChildFamilyId in lstChildFamily)
                {
                    try
                    {
                        this.ImportActiveChildFamilyCount = lstChildFamily.Count;
                        DBChildFamilySchoolYear = new ChildFamilySchoolYear();
                        DBChildFamilySchoolYear.Id = Guid.NewGuid();
                        DBChildFamilySchoolYear.ChildFamilyId = ChildFamilyId;
                        DBChildFamilySchoolYear.SchoolYearId = SchoolYearId;
                        DBChildFamilySchoolYear.active = true;
                        db.ChildFamilySchoolYears.InsertOnSubmit(DBChildFamilySchoolYear);
                        db.SubmitChanges();
                    }
                    catch
                    { }
                }

                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildFamily, "ImportAllActiveChildFamily", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion
    }
}
