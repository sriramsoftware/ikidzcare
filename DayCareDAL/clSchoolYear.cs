using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DayCareDAL
{
    public class clSchoolYear
    {
        #region "Load School Year, Dt: 4-Aug-2011, DB: A"
        public static List<DayCarePL.SchoolYearProperties> LoadSchoolYear(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", "Execute LoadSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", "Debug LoadSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);

                var SchoolYearData = (from sy in db.SchoolYears
                                      join s in db.Schools on sy.SchoolId equals s.Id into schoolname
                                      from scnm in schoolname.DefaultIfEmpty()
                                      where sy.SchoolId.Equals(SchoolId)
                                      orderby sy.Year descending
                                      select new DayCarePL.SchoolYearProperties()
                                      {
                                          Id = sy.Id,
                                          SchoolId = sy.SchoolId,
                                          SchoolName = scnm.Name,
                                          Year = sy.Year,
                                          StartDate = sy.StartDate,
                                          EndDate = sy.EndDate,
                                          CurrentId = sy.CurrentId,
                                          Comments = sy.Comments,
                                          CreatedById = sy.CreatedById,
                                          CreatedDateTime = sy.CreatedDateTime,
                                          LastModifiedById = sy.LastModifiedById,
                                          LastModifiedDatetime = sy.LastModifiedDatetime
                                      }).ToList();



                return SchoolYearData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 4-Aug-2011, DB: A"
        public static Guid Save(DayCarePL.SchoolYearProperties objSchoolYear, Guid OldCurrentSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction tran = null;
            SchoolYear DBSchoolYear = null;
            bool result = true;
            //Guid LastCurrentId = new Guid();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (db.Connection.State == System.Data.ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                tran = db.Connection.BeginTransaction();
                db.Transaction = tran;
                //if (objSchoolYear.CurrentId == true)
                if (objSchoolYear.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT) && objSchoolYear.CurrentId == false) // remove condition if you wish to import staff and programs on update event.
                {
                    DBSchoolYear = new SchoolYear();
                    DBSchoolYear = db.SchoolYears.SingleOrDefault(Currentid => Currentid.CurrentId.Equals(true) && Currentid.SchoolId == objSchoolYear.SchoolId);
                    if (DBSchoolYear != null)
                    {
                        OldCurrentSchoolYearId = DBSchoolYear.Id;
                        // DBSchoolYear.CurrentId = false;
                        // db.SubmitChanges();
                    }
                }

                if (objSchoolYear.CurrentId == true)
                {
                    DBSchoolYear = new SchoolYear();
                    DBSchoolYear = db.SchoolYears.SingleOrDefault(Currentid => Currentid.CurrentId.Equals(true) && Currentid.SchoolId == objSchoolYear.SchoolId);
                    if (DBSchoolYear != null)
                    {
                        OldCurrentSchoolYearId = DBSchoolYear.Id;
                        DBSchoolYear.CurrentId = false;
                        db.SubmitChanges();
                    }
                }

                if (objSchoolYear.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBSchoolYear = new SchoolYear();
                    DBSchoolYear.Id = System.Guid.NewGuid();
                    DBSchoolYear.CreatedById = objSchoolYear.CreatedById;
                    DBSchoolYear.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBSchoolYear = db.SchoolYears.SingleOrDefault(u => u.Id.Equals(objSchoolYear.Id));

                }
                DBSchoolYear.Year = objSchoolYear.Year;
                DBSchoolYear.SchoolId = objSchoolYear.SchoolId;
                DBSchoolYear.StartDate = objSchoolYear.StartDate;
                DBSchoolYear.EndDate = objSchoolYear.EndDate;
                DBSchoolYear.CurrentId = objSchoolYear.CurrentId;
                DBSchoolYear.Comments = objSchoolYear.Comments;
                DBSchoolYear.LastModifiedById = objSchoolYear.LastModifiedById;
                DBSchoolYear.LastModifiedDatetime = DateTime.Now;
                //if (objSchoolYear.CurrentId == true)
                //{
                //if (!OldCurrentSchoolYearId.Equals(DBSchoolYear.Id))
                //{
                DBSchoolYear.OldCurrentSchoolYearId = OldCurrentSchoolYearId;
                //}
                //}

                if (objSchoolYear.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.SchoolYears.InsertOnSubmit(DBSchoolYear);
                }
                db.SubmitChanges();




                if (objSchoolYear.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT)) // add ! (not equal to if you wish to import staff and programs on update event.
                //if (objSchoolYear.CurrentId == true)
                {
                    if (DayCareDAL.clStaffSchoolYear.ImportAllActiveStaff(DBSchoolYear.Id, objSchoolYear.SchoolId, OldCurrentSchoolYearId, tran, db))
                    {
                        //tran.Commit();
                        result = true;
                    }
                    else
                    {
                        //tran.Rollback();
                        result = false;
                    }

                    //clChildFamily objChildFamily = new clChildFamily();
                    //if (objChildFamily.ImportAllActiveChildFamily(DBSchoolYear.Id, objSchoolYear.SchoolId, OldCurrentSchoolYearId, tran, db))
                    //{
                    //    //tran.Commit();
                    //    result = true;
                    //    if (objChildFamily.ImportActiveChildFamilyCount > 0)
                    //    {
                    //        if (clChildSchoolYear.ImportAllActiveChild(objSchoolYear.Id, objSchoolYear.SchoolId, OldCurrentSchoolYearId, tran, db))
                    //        {
                    //            result = true;
                    //        }
                    //        else
                    //        {
                    //            result = false;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    //tran.Rollback();
                    //    result = false;
                    //}
                    //importschoolProgram Import
                    if (result == true)
                    {
                        if (!OldCurrentSchoolYearId.Equals(DBSchoolYear.Id))
                        {
                            if (SaveProgramWithOtherSchoolYear(OldCurrentSchoolYearId, DBSchoolYear.Id, objSchoolYear.SchoolId, tran, db))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                    //end
                    //if (result)
                    //{
                    //    tran.Commit();

                    //}
                    //else
                    //{
                    //    tran.Rollback();
                    //}
                }
                else
                {
                    //tran.Commit();
                    result = true;
                }
                #region "import classroom when changes current year"
                //start---import classroom when changes current year
                if (result == true)
                {
                    if (objSchoolYear.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        if (DayCareDAL.clClassRoom.ImportAllActiveClassRoom(DBSchoolYear.Id, objSchoolYear.SchoolId, OldCurrentSchoolYearId, tran, db))
                        {
                            //tran.Commit();
                            result = true;

                            //update ClassRoomSchoolYearId into ProgClassRoom table. 
                            if (DayCareDAL.clClassRoom.UpdateClassRoomSchoolYearID(DBSchoolYear.Id, OldCurrentSchoolYearId, tran, db))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }

                            //update classroomschoolyearid into last year, when first time added
                            if (DayCareDAL.clClassRoom.UpdateLastAddedYearClassRoomSchoolYearID(DBSchoolYear.Id, OldCurrentSchoolYearId, tran, db))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            //tran.Rollback();
                            result = false;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
                //end---import classroom when changes current year
                #endregion
                if (result == true)
                {
                    if (objSchoolYear.CurrentId == true)
                    {
                        if (!OldCurrentSchoolYearId.Equals(DBSchoolYear.Id))
                        {
                            if (UpdateClosingBalance(objSchoolYear.SchoolId, OldCurrentSchoolYearId, null, tran, db))
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                }

                if (result)
                {
                    tran.Commit();
                    return DBSchoolYear.Id;
                }
                else
                {
                    tran.Rollback();
                    return new Guid();
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                if (tran != null)
                {
                    tran.Rollback();
                }
                return new Guid();
            }
            finally
            {
                if (db.Connection.State == System.Data.ConnectionState.Open)
                {
                    db.Connection.Close();
                }
            }
        }
        #endregion

        #region Check Duplicate User Name, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateSchoolYear(string SchoolYear, Guid SchoolYearId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "CheckDuplicateSchoolYear", "Execute CheckDuplicateSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "CheckDuplicateSchoolYear", "Debug CheckDuplicateSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
                int count;

                if (SchoolYearId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.SchoolYears
                             where s.Year.Equals(SchoolYear)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.SchoolYears
                             where s.Year.Equals(SchoolYear)
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(SchoolYearId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "CheckDuplicateSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Get All School Year, Dt: 4-Aug-2011, DB: A"
        public static List<DayCarePL.SchoolYearProperties> LoadAllSchoolYear(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "LoadAllSchoolYear", "Execute LoadAllSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "LoadAllSchoolYear", "Debug LoadAllSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);

                var SchoolYearData = (from sy in db.SchoolYears
                                      where sy.SchoolId.Equals(SchoolId)
                                      orderby sy.Year descending
                                      select new DayCarePL.SchoolYearProperties()
                                      {
                                          Id = sy.Id,
                                          SchoolId = sy.SchoolId,
                                          Year = sy.Year,
                                      }).ToList();
                return SchoolYearData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "LoadAllSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Get Current School Year, Dt: 4-Aug-2011, DB: A"
        public static Guid GetCurrentSchoolYear(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "GetCurrentSchoolYear", "Execute GetCurrentSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "GetCurrentSchoolYear", "Debug GetCurrentSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);

                var SchoolYearData = (from sy in db.SchoolYears
                                      where sy.SchoolId.Equals(SchoolId) && sy.CurrentId.Equals(true)
                                      select new
                                      {
                                          Id = sy.Id
                                      }).Single();
                return SchoolYearData.Id;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "GetCurrentSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new Guid();
            }
        }
        #endregion

        #region "Automatic add to School Program and Fessperiod
        public static bool SaveProgramWithOtherSchoolYear(Guid OldCurrentSchoolYearId, Guid CurrentSchoolYearId, Guid SchoolId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCareDataContext db = dbold;
            db.Transaction = tran;
            SchoolProgram DBSchoolProgram = null;
            SchoolProgramFeesDetail DBSchoolProgramFeesDetail = null;
            try
            {
                /* Guid currentschoolyearid = (from sy in db.SchoolYears
                                             where sy.CurrentId.Equals(true) && sy.SchoolId.Equals(SchoolId)
                                             select sy.Id).SingleOrDefault(); */

                Guid currentschoolyearid = CurrentSchoolYearId;

                List<Guid> lstProgram = (from sp in db.SchoolPrograms
                                         where sp.SchoolYearId.Equals(OldCurrentSchoolYearId) && sp.Active.Equals(true)
                                         && !(from sp1 in db.SchoolPrograms
                                              where sp1.SchoolYearId.Equals(currentschoolyearid)
                                              select sp1.ProgUniqueId).Contains(sp.ProgUniqueId)
                                         select sp.Id).ToList();


                ProgClassRoom DBProgClassRoom = null;
                foreach (Guid ProgramId in lstProgram)
                {
                    //try
                    //{
                    SchoolProgram DBSchoolProgramOld = db.SchoolPrograms.FirstOrDefault(i => i.Id.Equals(ProgramId));
                    if (DBSchoolProgramOld != null)
                    {
                        DBSchoolProgram = new SchoolProgram();
                        //DBSchoolProgram = DBSchoolProgramOld;
                        DBSchoolProgram.Id = Guid.NewGuid();
                        DBSchoolProgram.Title = DBSchoolProgramOld.Title;
                        DBSchoolProgram.Comments = DBSchoolProgramOld.Comments;
                        DBSchoolProgram.Active = DBSchoolProgramOld.Active;
                        DBSchoolProgram.IsPrimary = DBSchoolProgramOld.IsPrimary;
                        DBSchoolProgram.CreatedById = DBSchoolProgramOld.CreatedById;
                        DBSchoolProgram.LastModifiedById = DBSchoolProgramOld.LastModifiedById;
                        DBSchoolProgram.ProgUniqueId = DBSchoolProgramOld.ProgUniqueId;
                        DBSchoolProgram.SchoolYearId = CurrentSchoolYearId;
                        DBSchoolProgram.CreatedDateTime = DateTime.Now;
                        DBSchoolProgram.LastModifiedDatetime = DateTime.Now;
                        db.SchoolPrograms.InsertOnSubmit(DBSchoolProgram);
                        db.SubmitChanges();


                        //Automatic add to progclassroom change on 28-Aug-2012 by Akash
                        List<Guid> lstProgClass = (from pcr in db.ProgClassRooms
                                                   where pcr.Active.Equals(true) && pcr.SchoolProgramId.Equals(ProgramId)
                                                   select pcr.ClassRoomId).ToList();

                        foreach (Guid ClassroomId in lstProgClass)
                        {
                            DBProgClassRoom = new ProgClassRoom();
                            DBProgClassRoom.Id = Guid.NewGuid();
                            DBProgClassRoom.SchoolProgramId = DBSchoolProgram.Id;
                            DBProgClassRoom.ClassRoomId = ClassroomId;
                            DBProgClassRoom.Active = true;
                            DBProgClassRoom.CreatedDateTime = DateTime.Now;
                            DBProgClassRoom.LastModifiedDatetime = DateTime.Now;
                            DBProgClassRoom.CreatedById = DBSchoolProgramOld.CreatedById;
                            DBProgClassRoom.LastModifiedById = DBSchoolProgramOld.LastModifiedById;
                            db.ProgClassRooms.InsertOnSubmit(DBProgClassRoom);
                            db.SubmitChanges();
                        }
                    }

                    var lstDBSchoolProgramFeesDetailOld = db.SchoolProgramFeesDetails.Where(i => i.SchoolProgramId.Equals(ProgramId));
                    if (lstDBSchoolProgramFeesDetailOld != null)
                    {
                        foreach (SchoolProgramFeesDetail objSchoolProgramFeesDetails in lstDBSchoolProgramFeesDetailOld)
                        {
                            DBSchoolProgramFeesDetail = new SchoolProgramFeesDetail();
                            DBSchoolProgramFeesDetail.Id = Guid.NewGuid();
                            //DBSchoolProgramFeesDetail = objSchoolProgramFeesDetails;
                            DBSchoolProgramFeesDetail.SchoolProgramId = DBSchoolProgram.Id;
                            DBSchoolProgramFeesDetail.Fees = objSchoolProgramFeesDetails.Fees;
                            DBSchoolProgramFeesDetail.FeesPeriodId = objSchoolProgramFeesDetails.FeesPeriodId;
                            DBSchoolProgramFeesDetail.EffectiveYearDate = objSchoolProgramFeesDetails.EffectiveYearDate;
                            DBSchoolProgramFeesDetail.EffectiveMonthDay = objSchoolProgramFeesDetails.EffectiveMonthDay;
                            DBSchoolProgramFeesDetail.EffectiveWeekDay = objSchoolProgramFeesDetails.EffectiveWeekDay;
                            DBSchoolProgramFeesDetail.CreatedById = objSchoolProgramFeesDetails.CreatedById;
                            DBSchoolProgramFeesDetail.LastModifiedById = objSchoolProgramFeesDetails.LastModifiedById;
                            DBSchoolProgramFeesDetail.CreatedDateTime = DateTime.Now;
                            DBSchoolProgramFeesDetail.LastModifiedDatetime = DateTime.Now;
                            db.SchoolProgramFeesDetails.InsertOnSubmit(DBSchoolProgramFeesDetail);
                            db.SubmitChanges();
                        }
                    }
                    //}
                    //catch
                    //{ }
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "SaveProgramWithOtherSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }

        //public static bool SaveFeesPeriodWithOtherSchoolYeartoSchoolProgramFeesDetail(Guid OldCurrentSchoolYearId, Guid CurrentSchoolYearId, Guid SchoolId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        //{
        //    DayCareDataContext db = dbold;
        //    db.Transaction = tran;
        //    SchoolProgram DBSchoolProgram = null;
        //    SchoolProgramFeesDetail DBSchoolProgramFeesDetail = null;
        //    try
        //    {
        //        Guid currentschoolyearid = (from sy in db.SchoolYears
        //                                    where sy.CurrentId.Equals(true) && sy.SchoolId.Equals(SchoolId)
        //                                    select sy.Id).SingleOrDefault();

        //        List<Guid> lstProgram = (from sp in db.SchoolPrograms
        //                                 where sp.SchoolYearId.Equals(OldCurrentSchoolYearId) && sp.Active.Equals(true)
        //                                 //&& !(from sp1 in db.SchoolPrograms
        //                                 //     where sp1.SchoolYearId.Equals(currentschoolyearid)
        //                                 //     select sp1.Id).Contains(sp.Id)
        //                                 select sp.Id).ToList();

        //        foreach (Guid ProgramId in lstProgram)
        //        {
        //            //try
        //            //{
        //            SchoolProgram DBSchoolProgramOld = db.SchoolPrograms.FirstOrDefault(i => i.Id.Equals(ProgramId));
        //            if (DBSchoolProgramOld != null)
        //            {
        //                DBSchoolProgram = new SchoolProgram();
        //                DBSchoolProgram = DBSchoolProgramOld;
        //                DBSchoolProgram.Id = Guid.NewGuid();
        //                DBSchoolProgram.SchoolYearId = CurrentSchoolYearId;
        //                DBSchoolProgram.CreatedDateTime = DateTime.Now;
        //                DBSchoolProgram.LastModifiedDatetime = DateTime.Now;
        //                db.SchoolPrograms.InsertOnSubmit(DBSchoolProgram);
        //                db.SubmitChanges();

        //                var lstDBSchoolProgramFeesDetailOld = db.SchoolProgramFeesDetails.Where(i => i.SchoolProgramId.Equals(ProgramId));
        //                if (lstDBSchoolProgramFeesDetailOld != null)
        //                {
        //                    foreach (SchoolProgramFeesDetail objSchoolProgramFeesDetails in lstDBSchoolProgramFeesDetailOld)
        //                    {
        //                        DBSchoolProgramFeesDetail = new SchoolProgramFeesDetail();
        //                        DBSchoolProgramFeesDetail = objSchoolProgramFeesDetails;
        //                        DBSchoolProgramFeesDetail.SchoolProgramId = DBSchoolProgram.Id;
        //                        DBSchoolProgramFeesDetail.CreatedDateTime = DateTime.Now;
        //                        DBSchoolProgramFeesDetail.LastModifiedDatetime = DateTime.Now;
        //                        db.SchoolProgramFeesDetails.InsertOnSubmit(DBSchoolProgramFeesDetail);
        //                        db.SubmitChanges();
        //                    }
        //                }
        //            }
        //            //}
        //            //catch
        //            //{ }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region "Get Schoolyear Detail via Id, Dt: 14-Sept-2012, DB: A"
        public static DayCarePL.SchoolYearProperties LoadSchoolYearDtail(Guid SchoolId, Guid SchoolyearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", "Execute LoadSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", "Debug LoadSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);

                var SchoolYearData = (from sy in db.SchoolYears
                                      where sy.Id.Equals(SchoolyearId) && sy.SchoolId.Equals(SchoolId)
                                      select new DayCarePL.SchoolYearProperties()
                                      {
                                          Id = sy.Id,
                                          SchoolId = sy.SchoolId,
                                          Year = sy.Year,
                                          StartDate = sy.StartDate,
                                          EndDate = sy.EndDate,
                                          CurrentId = sy.CurrentId,
                                          Comments = sy.Comments,
                                          CreatedById = sy.CreatedById,
                                          CreatedDateTime = sy.CreatedDateTime,
                                          LastModifiedById = sy.LastModifiedById,
                                          LastModifiedDatetime = sy.LastModifiedDatetime
                                      }).FirstOrDefault();



                return SchoolYearData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "LoadSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Get Previous Year Of Selected Current Year, Dt: 24-April-2013, DB: A"
        public static List<DayCarePL.SchoolYearListProperties> GetPreviousYearOfSelectedCurrentYear(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "GetPreviousYearOfSelectedCurrentYear", "Execute GetPreviousYearOfSelectedCurrentYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.SchoolYearListProperties> lstSchoolYear = new List<DayCarePL.SchoolYearListProperties>();
            DayCarePL.SchoolYearListProperties objSchoolYear;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "GetPreviousYearOfSelectedCurrentYear", "Debug GetPreviousYearOfSelectedCurrentYear Method", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetPreviousYearOfSelectedCurrentYear(CurrentSchoolYearId, SchoolId);
                foreach (var d in data)
                {
                    objSchoolYear = new DayCarePL.SchoolYearListProperties();
                    objSchoolYear.Year = d.SchoolYear;
                    objSchoolYear.SchoolYearId = d.Id;
                    lstSchoolYear.Add(objSchoolYear);
                }
                return lstSchoolYear;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "GetPreviousYearOfSelectedCurrentYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new List<DayCarePL.SchoolYearListProperties>();
            }

        }
        #endregion

        #region "Check if selected year for edit is current year or next year or not. if not than don not allow to edit, Dt: 9-May-2013, DB: A"
        public static bool IsSelectedYear_Current_NextYearORPrevYear(Guid SchoolId, string SelectedSchoolYear)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYear_Current_NextYearORPrevYear", "Execute IsSelectedYear_Current_NextYearORPrevYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYear_Current_NextYearORPrevYear", "Debug IsSelectedYear_Current_NextYearORPrevYear Method", DayCarePL.Common.GUID_DEFAULT);
                SortedList sl = new SortedList();
                sl.Add("@SchoolID", SchoolId);
                sl.Add("@SchoolYear", SelectedSchoolYear);
                DataSet ds = clConnection.GetDataSet("spIsSelectedYear_Current_NextYearORPrevYear", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYear_Current_NextYearORPrevYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Update Closing Balance, Dt: 9-May-2013, DB: A"
        public static bool UpdateClosingBalance(Guid SchoolId, Guid SchoolYearId, Guid? ChildFamilyId, System.Data.Common.DbTransaction tran, DayCareDataContext db)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", "Execute UpdateClosingBalance Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            //DayCareDataContext db1 = db;
            try
            {
                //db1.Transaction = tran;
                //SqlConnection con = clConnection.CreateConnection();
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", "Debug UpdateClosingBalance Method", DayCarePL.Common.GUID_DEFAULT);
                //clConnection.OpenConnection(con);

                //SqlCommand cmd = clConnection.CreateCommand("spUpdateClosingBalance", con);
                //cmd.Transaction = tran;
                //cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", SchoolYearId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@childfamilyid", ChildFamilyId));
                //object id = cmd.ExecuteScalar();
                var data = db.spUpdateClosingBalance(SchoolId, SchoolYearId, ChildFamilyId);
                foreach (var d in data)
                {

                }
                // if any closing balance of given schoolyearid is null than thare is something wrong with updating.
                int Count = (from cfsy in db.ChildFamilySchoolYears
                             where cfsy.SchoolYearId == SchoolYearId && cfsy.ClosingBalance == null
                             select cfsy).Count();
                if (Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //if (Convert.ToBoolean(id))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}



                //SortedList sl = new SortedList();
                //sl.Add("@SchoolId", SchoolId);
                //sl.Add("@SchoolYearId", SchoolYearId);
                //sl.Add("@childfamilyid", ChildFamilyId);
                //DataSet ds = clConnection.GetDataSet("spUpdateClosingBalance", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }

        public static bool UpdateClosingBalance(Guid SchoolId, Guid SchoolYearId, Guid? ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", "Execute UpdateClosingBalance Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                //db1.Transaction = tran;
                //SqlConnection con = clConnection.CreateConnection();
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", "Debug UpdateClosingBalance Method", DayCarePL.Common.GUID_DEFAULT);
                //clConnection.OpenConnection(con);

                //SqlCommand cmd = clConnection.CreateCommand("spUpdateClosingBalance", con);
                //cmd.Transaction = tran;
                //cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolId", SchoolId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolYearId", SchoolYearId));
                //cmd.Parameters.Add(clConnection.GetInputParameter("@childfamilyid", ChildFamilyId));
                //object id = cmd.ExecuteScalar();
                var data = db.spUpdateClosingBalance(SchoolId, SchoolYearId, ChildFamilyId);
                foreach (var d in data)
                {

                }

                // if any closing balance of given schoolyearid is null than thare is something wrong with updating.
                int Count = (from cfsy in db.ChildFamilySchoolYears
                             where cfsy.SchoolYearId == SchoolYearId && cfsy.ClosingBalance == null
                             select cfsy).Count();
                if (Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                //if (Convert.ToBoolean(id))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}



                //SortedList sl = new SortedList();
                //sl.Add("@SchoolId", SchoolId);
                //sl.Add("@SchoolYearId", SchoolYearId);
                //sl.Add("@childfamilyid", ChildFamilyId);
                //DataSet ds = clConnection.GetDataSet("spUpdateClosingBalance", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "UpdateClosingBalance", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Check if Selected Year is Prev Year OR Not, Dt: 9-May-2013, DB: A"
        public static bool IsSelectedYearPrevYearORNot(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYearPrevYearORNot", "Execute IsSelectedYearPrevYearORNot Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYearPrevYearORNot", "Debug IsSelectedYearPrevYearORNot Method", DayCarePL.Common.GUID_DEFAULT);
                SortedList sl = new SortedList();
                sl.Add("@SchoolId", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                DataSet ds = clConnection.GetDataSet("spIsSelectedYearPrevYearORNot", sl);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "IsSelectedYearPrevYearORNot", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Get Previous Current Year id of selected Current school Year"
        public static Guid GetOldCurrentSchoolYearId(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchoolYear, "GetOldCurrentSchoolYearId", "Execute GetOldCurrentSchoolYearId Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchoolYear, "GetOldCurrentSchoolYearId", "Debug GetOldCurrentSchoolYearId Method", DayCarePL.Common.GUID_DEFAULT);
                var SchoolYearData = (from sy in db.SchoolYears
                                      where sy.SchoolId.Equals(SchoolId) && sy.CurrentId.Equals(true)
                                      select new
                                      {
                                          OldCurrentSchoolYearId = sy.OldCurrentSchoolYearId
                                      }).Single();
                return SchoolYearData.OldCurrentSchoolYearId.Value;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchoolYear, "GetCurrentSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new Guid();
            }
        }
        #endregion
    }
}
