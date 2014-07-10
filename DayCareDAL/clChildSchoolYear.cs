using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DayCareDAL
{
    public class clChildSchoolYear
    {
        #region "Import All Active Child of school, Dt: 2-Aug-2011, DB: A"
        public static bool ImportAllActiveChild(Guid SchoolYearId, Guid SchoolId, Guid OldCurrentSchoolYearId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Execute ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = dbold;
            db.Transaction = tran;
            ChildSchoolYear DBChildSchoolYear = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Debug ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);

                //Guid currentschoolyearid = (from sy in db.SchoolYears
                //                            where sy.CurrentId.Equals(true)
                //                            select sy.Id).SingleOrDefault();

                List<Guid> lstChild = (from c in db.ChildDatas
                                       join csy in db.ChildSchoolYears on c.Id equals csy.ChildDataId
                                       join cf in db.ChildFamilies on c.ChildFamilyId equals cf.Id
                                       where cf.SchoolId.Equals(SchoolId) && csy.active.Equals(true) && csy.SchoolYearId.Equals(OldCurrentSchoolYearId)
                                       && !(from cy in db.ChildSchoolYears
                                            where cy.SchoolYearId.Equals(SchoolYearId)
                                            select cy.ChildDataId).Contains(c.Id)
                                       select c.Id).ToList();

                //var Data = db.spGetChildIdNotInChildSchoolYear(SchoolId, SchoolYearId);

                foreach (Guid ChildDataId in lstChild)
                {
                    try
                    {
                        DBChildSchoolYear = new ChildSchoolYear();
                        DBChildSchoolYear.Id = Guid.NewGuid();
                        DBChildSchoolYear.ChildDataId = ChildDataId;
                        DBChildSchoolYear.SchoolYearId = SchoolYearId;
                        DBChildSchoolYear.active = true;
                        db.ChildSchoolYears.InsertOnSubmit(DBChildSchoolYear);
                        db.SubmitChanges();
                    }
                    catch
                    { }
                }

                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }

        public static List<DayCarePL.ChildDataProperties> GetAllChildListForImport(Guid CurrentSchoolYearId, Guid PreSchoolYearId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllChildListForImport", "Execute GetAllChildListForImport Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllChildListForImport", "Debug GetAllChildListForImport Method", DayCarePL.Common.GUID_DEFAULT);
                var Data = db.spGetAllChildListForImport(CurrentSchoolYearId, PreSchoolYearId, SchoolId);
                DayCarePL.ChildDataProperties objChildData;
                List<DayCarePL.ChildDataProperties> lstChildData = new List<DayCarePL.ChildDataProperties>();

                foreach (var d in Data)
                {
                    objChildData = new DayCarePL.ChildDataProperties();
                    objChildData.ChildDataId = d.ChildDataId;
                    objChildData.FullName = d.FullName;
                    objChildData.FamilyName = d.FamilyName;
                    objChildData.ChildFamilyId = d.ChildFamilyId;
                    objChildData.Active = d.Active == null ? false : d.Active.Value;
                    objChildData.ImportedCount = d.ImportedCount == null ? 0 : d.ImportedCount.Value;
                    lstChildData.Add(objChildData);
                }
                return lstChildData; ;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllChildListForImport", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new List<DayCarePL.ChildDataProperties>();
            }

        }

        //Get Previous Year Of Current Year, Dt: 12-April-2013, DB: A
        public static DayCarePL.SchoolYearProperties GetPreviousYearOfCurrentYear(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Execute ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.DoConnection();

                DayCareDataContext db = new DayCareDataContext();


                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Debug ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);

                //Guid currentschoolyearid = (from sy in db.SchoolYears
                //                            where sy.CurrentId.Equals(true)
                //                            select sy.Id).SingleOrDefault();

                DayCarePL.SchoolYearProperties objSchoolyear = (from sy in db.SchoolYears
                                                                where sy.SchoolId == SchoolId && sy.CurrentId == true
                                                                select new DayCarePL.SchoolYearProperties()
                                                                {
                                                                    Id = sy.Id,
                                                                    Year = sy.Year,
                                                                    OldCurrentSchoolYearId = sy.OldCurrentSchoolYearId == null ? new Guid() : sy.OldCurrentSchoolYearId.Value,
                                                                    OldCurrentSchoolYear = (from s in db.SchoolYears
                                                                                            where s.Id == sy.OldCurrentSchoolYearId
                                                                                            select s.Year).FirstOrDefault()
                                                                }).FirstOrDefault();

                return objSchoolyear;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }

        public static List<DayCarePL.ChildDataProperties> GetAllActiveChildListForImport(Guid CurrentSchoolYearId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllActiveChildListForImport", "Execute GetAllActiveChildListForImport Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllActiveChildListForImport", "Debug GetAllActiveChildListForImport Method", DayCarePL.Common.GUID_DEFAULT);
                var Data = db.spGetAllActiveChildListForImport(CurrentSchoolYearId, SchoolId);
                DayCarePL.ChildDataProperties objChildData;
                List<DayCarePL.ChildDataProperties> lstChildData = new List<DayCarePL.ChildDataProperties>();

                foreach (var d in Data)
                {
                    objChildData = new DayCarePL.ChildDataProperties();
                    objChildData.ChildDataId = d.ChildDataId;
                    objChildData.FullName = d.FullName;
                    objChildData.FamilyName = d.FamilyName;
                    objChildData.ChildFamilyId = d.ChildFamilyId;
                    objChildData.ImportedCount = d.ImportedCount == null ? 0 : d.ImportedCount.Value;
                    lstChildData.Add(objChildData);
                }
                return lstChildData; ;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "GetAllActiveChildListForImport", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new List<DayCarePL.ChildDataProperties>();
            }

        }

        //Import All Selected Child of school, Dt: 11-April-2013, DB: A
        public static bool ImportAllSelectedChild(Guid ChildDataId, Guid ChildFamilyId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllSelectedChild", "Execute ImportAllSelectedChild Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction tran = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllSelectedChild", "Debug ImportAllSelectedChild Method", DayCarePL.Common.GUID_DEFAULT);
                //ChildSchoolYear
                if (db.Connection.State == System.Data.ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                tran = db.Connection.BeginTransaction();
                db.Transaction = tran;

                //ChildFamilySchoolYear
                bool IsActive = true;
                ChildFamilySchoolYear DBChildFamilySchoolYear = db.ChildFamilySchoolYears.FirstOrDefault(i => i.ChildFamilyId.Equals(ChildFamilyId) && i.SchoolYearId.Equals(SchoolYearId));
                if (DBChildFamilySchoolYear == null)
                {
                    DBChildFamilySchoolYear = new ChildFamilySchoolYear();
                    DBChildFamilySchoolYear.Id = Guid.NewGuid();
                    DBChildFamilySchoolYear.ChildFamilyId = ChildFamilyId;
                    DBChildFamilySchoolYear.SchoolYearId = SchoolYearId;
                    DBChildFamilySchoolYear.active = true;
                    db.ChildFamilySchoolYears.InsertOnSubmit(DBChildFamilySchoolYear);
                    db.SubmitChanges();
                }
                else
                {
                    IsActive = DBChildFamilySchoolYear.active == null ? false : DBChildFamilySchoolYear.active.Value;
                }

                ChildSchoolYear DBChildSchoolYear = db.ChildSchoolYears.FirstOrDefault(i => i.ChildDataId.Equals(ChildDataId) && i.SchoolYearId.Equals(SchoolYearId));
                if (DBChildSchoolYear == null)
                {
                    DBChildSchoolYear = new ChildSchoolYear();
                    DBChildSchoolYear.Id = Guid.NewGuid();
                    DBChildSchoolYear.ChildDataId = ChildDataId;
                    DBChildSchoolYear.SchoolYearId = SchoolYearId;
                    DBChildSchoolYear.active = IsActive;
                    db.ChildSchoolYears.InsertOnSubmit(DBChildSchoolYear);
                    db.SubmitChanges();
                }

                
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllSelectedChild", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                if (tran != null)
                {
                    tran.Rollback();
                }
                return false;
            }
            finally
            {
                if (db.Connection.State == System.Data.ConnectionState.Open)
                {
                    db.Connection.Close();
                }
            }
        }

        //Get Previous Year by passing current year id
        public static DayCarePL.SchoolYearProperties GetPreviousYearOfCurrentYearForMessage(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Execute ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.DoConnection();

                DayCareDataContext db = new DayCareDataContext();


                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", "Debug ImportAllActiveChild Method", DayCarePL.Common.GUID_DEFAULT);

                //Guid currentschoolyearid = (from sy in db.SchoolYears
                //                            where sy.CurrentId.Equals(true)
                //                            select sy.Id).SingleOrDefault();

                DayCarePL.SchoolYearProperties objSchoolyear = (from sy in db.SchoolYears
                                                                where sy.SchoolId == SchoolId && sy.Id.Equals(CurrentSchoolYearId)
                                                                select new DayCarePL.SchoolYearProperties()
                                                                {
                                                                    Id = sy.Id,
                                                                    Year = sy.Year,
                                                                    OldCurrentSchoolYearId = sy.OldCurrentSchoolYearId == null ? new Guid() : sy.OldCurrentSchoolYearId.Value,
                                                                    OldCurrentSchoolYear = (from s in db.SchoolYears
                                                                                            where s.Id == sy.OldCurrentSchoolYearId
                                                                                            select s.Year).FirstOrDefault()
                                                                }).FirstOrDefault();

                return objSchoolyear;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildSchoolYear, "ImportAllActiveChild", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion
    }


}
