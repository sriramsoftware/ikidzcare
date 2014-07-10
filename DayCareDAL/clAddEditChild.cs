using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace DayCareDAL
{
    public class clAddEditChild
    {
        #region Child
        #region "Save ChildData,Dt:7-Sept-2011,Db: A"
        public static DayCarePL.AddEdditChildProperties ChildSave(DayCarePL.AddEdditChildProperties objChildData)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "ChildSave", "Execute ChildSave Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction tran = null;
            ChildData DBChildData = null;
            bool Flag = true;
            Guid result = new Guid();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "ChildSave", "Debug ChildSave Method", DayCarePL.Common.GUID_DEFAULT);
                if (db.Connection.State == ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                tran = db.Connection.BeginTransaction();
                db.Transaction = tran;
                #region Child Data

                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBChildData = new ChildData();
                    DBChildData.Id = Guid.NewGuid();
                    DBChildData.CreatedById = objChildData.CreatedById;
                    DBChildData.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBChildData = db.ChildDatas.FirstOrDefault(i => i.Id.Equals(objChildData.Id));
                }
                DBChildData.ChildFamilyId = objChildData.ChildFamilyId;
                DBChildData.FirstName = objChildData.FirstName;
                DBChildData.LastName = objChildData.LastName;
                DBChildData.Gender = objChildData.Gender.Value;
                DBChildData.DOB = objChildData.DOB;
                DBChildData.SocSec = objChildData.SocSec;
                if (!string.IsNullOrEmpty(objChildData.Photo))
                {
                    DBChildData.Photo = DBChildData.Id + Path.GetExtension(objChildData.Photo);
                }
                else
                {
                    DBChildData.Photo = string.Empty;
                }
                //DBChildData.Photo = objChildData.Photo;
                DBChildData.Comments = objChildData.ChildComments;
                DBChildData.Active = objChildData.Active;
                DBChildData.LastModifiedById = objChildData.LastModifiedById;
                DBChildData.LastModifiedDatetime = DateTime.Now;
                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ChildDatas.InsertOnSubmit(DBChildData);
                }
                db.SubmitChanges();


                ChildSchoolYear DBChildSchoolYear = new ChildSchoolYear();
                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBChildSchoolYear.Id = Guid.NewGuid();
                    DBChildSchoolYear.ChildDataId = DBChildData.Id;
                    DBChildSchoolYear.SchoolYearId = objChildData.SchoolYearId;
                    DBChildSchoolYear.active = objChildData.Active; 
                    db.ChildSchoolYears.InsertOnSubmit(DBChildSchoolYear);
                    db.SubmitChanges();
                }
                else
                {
                    DBChildSchoolYear = db.ChildSchoolYears.FirstOrDefault(i => i.ChildDataId.Equals(objChildData.Id) && i.SchoolYearId.Equals(objChildData.SchoolYearId));
                    DBChildSchoolYear.active = objChildData.Active;
                    db.SubmitChanges();
                }
                //else
                //{
                //    DBChildSchoolYear = db.ChildSchoolYears.FirstOrDefault(i => i.Id.Equals(objChildData.ChildSchoolYearId));
                //}
                //result = DBChildData.Id;
                if (objChildData.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    objChildData.ChildDataId = DBChildData.Id;
                }
                else
                {
                    objChildData.ChildDataId = objChildData.Id;
                }
                //objChildData.ChildSchoolYearId = DBChildSchoolYear.Id;
                objChildData.ChildSchoolYearId = (from csy in db.ChildSchoolYears
                                                  where csy.ChildDataId.Equals(objChildData.ChildDataId) &&
                                                  csy.SchoolYearId.Equals(objChildData.SchoolYearId)
                                                  select csy.Id).FirstOrDefault();

                #endregion

                #region ChildProgEnrollment
                objChildData = ChildProgEnrollmentSave(objChildData, tran, db);
                if (objChildData != null)
                {
                    foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment in objChildData.lstChildProgEnrolment)
                    {
                        if (objChildProgEnrollment.Id.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                        {
                            Flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    Flag = false;
                }

                #endregion

                #region ChildEnrollment
                if (Flag)
                {
                    if (ChildEnrollmentStatusSave(objChildData, tran, db))
                    {
                        tran.Commit();
                    }
                    else
                    {
                        objChildData.EnrollmentStatusId = null;
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Rollback();
                }
                #endregion


                

                return objChildData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "ChildSave", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                //result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                if (tran != null)
                {
                    tran.Rollback();
                }
                return null;
            }
        }
        #endregion

        #region "Load ChildDataListByID, dt: 7-Sept-2011,Db: A"
        public static DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", "Execute LoadChildDataList Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", "Debug LoadChildDataList Method", DayCarePL.Common.GUID_DEFAULT);
                #region
                //DayCarePL.ChildDataProperties objChildDataId = new DayCarePL.ChildDataProperties();
                //SortedList sl = new SortedList();
                //sl.Add("@Id", ChildDataId);
                //sl.Add("@SchoolId", SchoolId);
                //sl.Add("@SchoolYearId", SchoolYearId);
                //DataSet ds = clConnection.GetDataSet("spGetChildDataListById", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        objChildDataId.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                //        objChildDataId.ChildFamilyId = new Guid(ds.Tables[0].Rows[0]["ChildFamilyId"].ToString());
                //        objChildDataId.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                //        objChildDataId.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                //        // objChildDataId.FullName = objChildDataId.FirstName + " " + objChildDataId.LastName;
                //        objChildDataId.Gender = Convert.ToBoolean(ds.Tables[0].Rows[0]["Gender"].ToString());
                //        objChildDataId.DOB = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SocSec"].ToString()))
                //        {
                //            objChildDataId.SocSec = ds.Tables[0].Rows[0]["SocSec"].ToString();
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Photo"].ToString()))
                //        {
                //            objChildDataId.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Comments"].ToString()))
                //        {
                //            objChildDataId.Comments = ds.Tables[0].Rows[0]["Comments"].ToString();
                //        }
                //        objChildDataId.Active = Convert.ToBoolean(ds.Tables[0].Rows[0]["Active"].ToString());
                //    }
                //}
                #endregion

                var data = (from c in db.ChildDatas
                            join cf in db.ChildFamilies on c.ChildFamilyId equals cf.Id
                            where c.Id.Equals(ChildDataId) && cf.SchoolId.Equals(SchoolId) &&
                            (from csy1 in db.ChildSchoolYears
                             where csy1.SchoolYearId.Equals(SchoolYearId)
                             select csy1.ChildDataId).Contains(c.Id)
                            select new DayCarePL.ChildDataProperties()
                            {
                                Id = c.Id,
                                ChildFamilyId = c.ChildFamilyId,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                FullName = c.FirstName + " " + c.LastName,
                                Gender = c.Gender,
                                DOB = c.DOB,
                                SocSec = c.SocSec,
                                Photo = c.Photo,
                                Comments = c.Comments,
                                Active = (from csy in db.ChildSchoolYears where csy.ChildDataId.Equals(c.Id) && csy.SchoolYearId.Equals(SchoolYearId) select csy.active.Value).FirstOrDefault(),
                                CreatedDateTime = c.CreatedDateTime,
                                CreatedById = c.CreatedById,
                                LastModifiedById = c.LastModifiedById,
                                LastModifiedDatetime = c.LastModifiedDatetime
                            }).FirstOrDefault();

                if (data != null)
                {

                    Guid ChildSchoolYearId = (from csy in db.ChildSchoolYears
                                              where csy.ChildDataId.Equals(data.Id) &&
                                              csy.SchoolYearId.Equals(SchoolYearId)
                                              select csy.Id).FirstOrDefault();
                    if (ChildSchoolYearId != null)
                    {
                        var enroll = (from ces in db.ChildEnrollmentStatus
                                      join es in db.EnrollmentStatus on ces.EnrollmentStatusId equals es.Id
                                      where ces.ChildSchoolYearId.Equals(ChildSchoolYearId)
                                      orderby ces.LastModifiedDatetime descending
                                      select new
                                      {
                                          EnrollmentStatusId = es.Id,
                                          EnrollmentStatus = es.Status,
                                          EnrollmentDate = ces.EnrollmentDate
                                      }).FirstOrDefault();
                        if (enroll != null)
                        {
                            data.EnrollmentStatusId = enroll.EnrollmentStatusId;// Select(en => en.EnrollmentStatusId).FirstOrDefault();
                            data.EnrollmentStatus = enroll.EnrollmentStatus;// Select(en => en.EnrollmentStatus).FirstOrDefault();
                            data.EnrollmentDate = enroll.EnrollmentDate;// Select(en => en.EnrollmentDate).FirstOrDefault();
                        }
                        else
                        {
                            data.EnrollmentDate = null;
                        }
                        var Program = (from cpe in db.ChildProgEnrollments
                                       join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                                       where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId) && sp.IsPrimary.Equals(true)
                                       select new
                                       {
                                           ProgramName = sp.Title,
                                           SchoolProgramId = sp.Id
                                       });
                        if (Program != null)
                        {
                            data.SchoolProgramId = Program.Select(p => p.SchoolProgramId).FirstOrDefault();
                            data.ProgramName = Program.Select(p => p.ProgramName).FirstOrDefault();
                        }


                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load ChildDataList, Dt:7-Sept-2011, Db:A"
        public static List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", "Execute LoadChildDataList Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", "Debug LoadChildDataList Method", DayCarePL.Common.GUID_DEFAULT);
                #region
                //DayCarePL.ChildDataProperties objChlidata = null;
                //List<DayCarePL.ChildDataProperties> lstChildData = new List<DayCarePL.ChildDataProperties>();
                //SortedList sl = new SortedList();
                //sl.Add("@SchoolID", SchoolId);
                //sl.Add("@SchoolYearId", SchoolYearId);
                //sl.Add("@ChildFamilyId", ChildFamilyId);
                //DataSet ds = clConnection.GetDataSet("spGetChildDataList", sl);
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                //    {
                //        objChlidata = new DayCarePL.ChildDataProperties();
                //        objChlidata.Id = new Guid(ds.Tables[0].Rows[iRow]["Id"].ToString());
                //        objChlidata.ChildFamilyId = new Guid(ds.Tables[0].Rows[iRow]["ChildFamilyId"].ToString());
                //        objChlidata.FirstName = ds.Tables[0].Rows[iRow]["FirstName"].ToString();
                //        objChlidata.LastName = ds.Tables[0].Rows[iRow]["LastName"].ToString();
                //        objChlidata.FullName = objChlidata.FirstName + " " + objChlidata.LastName;
                //        objChlidata.Gender = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["Gender"].ToString());
                //        objChlidata.DOB = Convert.ToDateTime(ds.Tables[0].Rows[iRow]["DOB"].ToString());
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["SocSec"].ToString()))
                //        {
                //            objChlidata.SocSec = ds.Tables[0].Rows[iRow]["SocSec"].ToString();
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Photo"].ToString()))
                //        {
                //            objChlidata.Photo = ds.Tables[0].Rows[iRow]["Photo"].ToString();
                //        }
                //        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[iRow]["Comments"].ToString()))
                //        {
                //            objChlidata.Comments = ds.Tables[0].Rows[iRow]["Comments"].ToString();
                //        }
                //        objChlidata.Active = Convert.ToBoolean(ds.Tables[0].Rows[iRow]["Active"].ToString());
                //        //objChlidata.FamilyName = ds.Tables[0].Rows[iRow]["FamilyName"].ToString();

                //        lstChildData.Add(objChlidata);
                //        objChlidata = null;
                //    }

                //}
                #endregion

                var data = (from c in db.ChildDatas
                            join cf in db.ChildFamilies on c.ChildFamilyId equals cf.Id
                            where cf.SchoolId.Equals(SchoolId) && c.ChildFamilyId.Equals(ChildFamilyId) &&
                            (from csy1 in db.ChildSchoolYears
                             where csy1.SchoolYearId.Equals(SchoolYearId)
                             select csy1.ChildDataId).Contains(c.Id)
                            select new DayCarePL.ChildDataProperties()
                            {
                                Id = c.Id,
                                ChildFamilyId = c.ChildFamilyId,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                FullName = c.FirstName + " " + c.LastName,
                                Gender = c.Gender,
                                DOB = c.DOB,
                                SocSec = c.SocSec,
                                Photo = c.Photo,
                                Comments = c.Comments,
                                Active = (from csy in db.ChildSchoolYears where csy.ChildDataId.Equals(c.Id) && csy.SchoolYearId.Equals(SchoolYearId) select csy.active.Value).FirstOrDefault(),
                                CreatedDateTime = c.CreatedDateTime,
                                CreatedById = c.CreatedById,
                                LastModifiedById = c.LastModifiedById,
                                LastModifiedDatetime = c.LastModifiedDatetime
                            }).ToList();

                List<DayCarePL.ChildDataProperties> lstChildData = new List<DayCarePL.ChildDataProperties>();
                if (data != null)
                {
                    foreach (DayCarePL.ChildDataProperties objChildData in data)
                    {
                        Guid ChildSchoolYearId = (from csy in db.ChildSchoolYears
                                                  where csy.ChildDataId.Equals(objChildData.Id) &&
                                                  csy.SchoolYearId.Equals(SchoolYearId)
                                                  select csy.Id).FirstOrDefault();
                        if (ChildSchoolYearId != null)
                        {
                            var enroll = (from ces in db.ChildEnrollmentStatus
                                          join es in db.EnrollmentStatus on ces.EnrollmentStatusId equals es.Id
                                          where ces.ChildSchoolYearId.Equals(ChildSchoolYearId)
                                          orderby ces.LastModifiedDatetime descending
                                          select new
                                          {
                                              ChildEnrollmentStatusId = ces.Id,
                                              EnrolmentStatus = es.Status,
                                              EnrollmentDate = ces.EnrollmentDate
                                          }).FirstOrDefault();
                            if (enroll != null)
                            {
                                objChildData.ChildEnrollmentStatusId = enroll.ChildEnrollmentStatusId;//Select(en => en.ChildEnrollmentStatusId).FirstOrDefault();
                                objChildData.EnrollmentStatus = enroll.EnrolmentStatus;// Select(en => en.EnrolmentStatus).FirstOrDefault();
                                objChildData.EnrollmentDate = enroll.EnrollmentDate;// Select(en => en.EnrollmentDate).FirstOrDefault();
                            }
                            else
                            {
                                objChildData.EnrollmentDate = null;
                            }
                            var Program = (from cpe in db.ChildProgEnrollments
                                           join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                                           where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId) && sp.IsPrimary.Equals(true)
                                           select new
                                           {
                                               ProgramName = sp.Title,
                                               SchoolProgramId = sp.Id
                                           });
                            if (Program != null)
                            {
                                objChildData.SchoolProgramId = Program.Select(p => p.SchoolProgramId).FirstOrDefault();
                                objChildData.ProgramName = Program.Select(p => p.ProgramName).FirstOrDefault();
                            }
                            //objChildData.ProgramName = (from cpe in db.ChildProgEnrollments
                            //                            join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                            //                            where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId)
                            //                            select sp.Title).FirstOrDefault();
                            lstChildData.Add(objChildData);
                        }

                    }
                }
                return lstChildData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildDataList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion
        #endregion

        #region Child Program
        #region "Load ProgClassRoom ID and Name Display, Dt:7-Sept-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgClassRoom", "Execute LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgClassRoom", "Debug LoadProgClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                /**/
                var data = (from pcr in db.ProgClassRooms
                            join crsy in db.ClassRoomSchoolYears on pcr.ClassRoomSchoolYearId equals crsy.Id
                            join cr in db.ClassRooms on pcr.ClassRoomId equals cr.Id
                            
                            where pcr.SchoolProgramId.Equals(SchoolProgramId)
                            && pcr.Active.Equals(true)
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                Id = pcr.Id,
                                ProgClassRoomTitle = crsy.ClassRoomName,
                                ClassRoomId = cr.Id
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion

        #region "Load Program SchoolProgramId and ProgramName, Dt:7-Sep-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", "Execute LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", "Debug LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from sp in db.SchoolPrograms
                            where sp.Active.Equals(true) && sp.SchoolYearId.Equals(SchoolYearId) && sp.IsPrimary.Equals(true)
                            orderby sp.Title ascending
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                ProgramTitle = sp.Title,//sp.IsPrimary.Equals(true) ? sp.Title + " - " + "Primary" : sp.Title + " - " + "Secondary",
                                SchoolProgramId = sp.Id
                            }).ToList();

                return data;// lstChildProgEnrollment;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load Secondary Program SchoolProgramId and ProgramName, Dt:7-Sep-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadSecondaryProgram(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", "Execute LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", "Debug LoadProgram Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from sp in db.SchoolPrograms
                            where sp.Active.Equals(true) && sp.SchoolYearId.Equals(SchoolYearId) && sp.IsPrimary.Equals(false)
                            orderby sp.Title ascending
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                ProgramTitle = sp.Title,//sp.IsPrimary.Equals(true) ? sp.Title + " - " + "Primary" : sp.Title + " - " + "Secondary",
                                SchoolProgramId = sp.Id
                            }).ToList();

                return data;// lstChildProgEnrollment;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        //#region "Get Fees From SchoolProgram by Id, Dt:7-Sep-2011,Db: A"
        //public static decimal GetFees(Guid SchoolProgramId)
        //{
        //    DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", "Execute GetFees Method", DayCarePL.Common.GUID_DEFAULT);
        //    clConnection.DoConnection();
        //    DayCareDataContext db = new DayCareDataContext();
        //    try
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", "Debug GetFees Method", DayCarePL.Common.GUID_DEFAULT);
        //        var data = (from c in db.SchoolPrograms
        //                    where c.Id.Equals(SchoolProgramId)
        //                    select c.Fees).SingleOrDefault();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //        return 0;
        //    }
        //}
        //#endregion

        #region "Get Fees From SchoolProgramFeesDetail by SchoolProgramId and FeesPeriodId, Dt:7-Sep-2011,Db: A"
        public static decimal GetFees(Guid SchoolProgramId, Guid FeesPeriodId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", "Execute GetFees Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", "Debug GetFees Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from spfd in db.SchoolProgramFeesDetails
                            where spfd.SchoolProgramId.Equals(SchoolProgramId) && spfd.FeesPeriodId.Equals(FeesPeriodId)
                            select spfd.Fees).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "GetFees", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return 0;
            }
        }
        #endregion

        #region "Save ChildProgEnrollment, Dt:7-Sep-2011, Db:A"
        public static DayCarePL.AddEdditChildProperties ChildProgEnrollmentSave(DayCarePL.AddEdditChildProperties objChildData, System.Data.Common.DbTransaction tran, DayCareDataContext dbMain)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentSave", "Execute ChildProgEnrollmentSave Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = dbMain;
            ChildProgEnrollment DBChildProgEnrollment = null;
            Guid result = new Guid();
            int count = 1;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentSave", "Debug ChildProgEnrollmentSave Method", DayCarePL.Common.GUID_DEFAULT);
                bool IsPrimary = false;
                bool IsAddInChildProgEnrollmentFeeDetail = false;//for Automatic
                Guid PrimarySchoolProgramId;
                db.Transaction = tran;
                foreach (DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment in objChildData.lstChildProgEnrolment)
                {
                    objChildProgEnrollment.ChildSchoolYearId = objChildData.ChildSchoolYearId;
                    objChildProgEnrollment.EnrollmentDate = objChildData.EnrollmentDate;
                    objChildProgEnrollment.EnrollmentStatus = objChildData.EnrollmentStatus;
                    objChildProgEnrollment.EnrollmentStatusId = objChildData.EnrollmentStatusId;

                    if (objChildProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        DBChildProgEnrollment = new ChildProgEnrollment();
                        DBChildProgEnrollment.Id = Guid.NewGuid();
                        DBChildProgEnrollment.CreatedById = objChildProgEnrollment.CreatedById;
                        DBChildProgEnrollment.CreatedDateTime = DateTime.Now;
                    }
                    else
                    {
                        DBChildProgEnrollment = db.ChildProgEnrollments.FirstOrDefault(i => i.Id.Equals(objChildProgEnrollment.Id));
                    }

                    DBChildProgEnrollment.ChildSchoolYearId = objChildData.ChildSchoolYearId;
                    DBChildProgEnrollment.SchoolProgramId = objChildProgEnrollment.SchoolProgramId;
                    DBChildProgEnrollment.ProgClassRoomId = objChildProgEnrollment.ProgClassRoomId;
                    DBChildProgEnrollment.DayIndex = objChildProgEnrollment.DayIndex;
                    DBChildProgEnrollment.DayType = objChildProgEnrollment.DayType;
                    DBChildProgEnrollment.FeesPeriodId = objChildProgEnrollment.FeesPeriodId;
                    DBChildProgEnrollment.Fees = objChildProgEnrollment.Fees;
                    DBChildProgEnrollment.LastModifiedById = objChildProgEnrollment.LastModifiedById;
                    DBChildProgEnrollment.LastModifiedDateTime = DateTime.Now;
                    DBChildProgEnrollment.StartDate = objChildData.StartDate;
                    DBChildProgEnrollment.EndDate = objChildData.EndDate;
                    // if (objChildProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    //{
                    IsPrimary = (from sp in db.SchoolPrograms
                                 where sp.Id.Equals(objChildProgEnrollment.SchoolProgramId)
                                 select sp.IsPrimary).SingleOrDefault();
                    objChildProgEnrollment.IsPrimary = IsPrimary;
                    if (IsPrimary)
                    {
                        PrimarySchoolProgramId = (from cpe in db.ChildProgEnrollments
                                                  join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                                                  where sp.IsPrimary.Equals(true) && cpe.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId)
                                                  select cpe.SchoolProgramId).FirstOrDefault();
                        if (PrimarySchoolProgramId == null)
                        {
                            PrimarySchoolProgramId = new Guid();
                        }
                        if (!PrimarySchoolProgramId.Equals(objChildProgEnrollment.SchoolProgramId))
                        {
                            //ChildProgEnrollment DBChildProgEnrollmentDelete = db.ChildProgEnrollments.FirstOrDefault(i => i.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId) && i.SchoolProgramId.Equals(PrimarySchoolProgramId));
                            if (!PrimarySchoolProgramId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                var ChildProgEnrollmentDelete = (from cpe in db.ChildProgEnrollments
                                                                 where cpe.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId) &&
                                                                 cpe.SchoolProgramId.Equals(PrimarySchoolProgramId)
                                                                 select cpe);
                                ChildProgEnrollmentFeeDetail DBChildProgEnrollmentFeeDetailDelete = db.ChildProgEnrollmentFeeDetails.FirstOrDefault(i => i.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId) && i.SchoolProgramId.Equals(PrimarySchoolProgramId));
                                if (ChildProgEnrollmentDelete != null && DBChildProgEnrollmentFeeDetailDelete != null)
                                {
                                    db.ChildProgEnrollments.DeleteAllOnSubmit(ChildProgEnrollmentDelete);
                                    db.SubmitChanges();

                                    db.ChildProgEnrollmentFeeDetails.DeleteOnSubmit(DBChildProgEnrollmentFeeDetailDelete);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                    else
                    {
                        PrimarySchoolProgramId = (from cpe in db.ChildProgEnrollments
                                                  join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                                                  where sp.IsPrimary.Equals(true) && cpe.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId)
                                                  select cpe.SchoolProgramId).FirstOrDefault();

                        if (PrimarySchoolProgramId == null || PrimarySchoolProgramId.ToString().ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                        {
                            PrimarySchoolProgramId = new Guid("11111111-1111-1111-1111-111111111111");
                        }
                        if (PrimarySchoolProgramId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                        {
                            result = PrimarySchoolProgramId;
                        }
                    }
                    if (!PrimarySchoolProgramId.ToString().Equals("11111111-1111-1111-1111-111111111111"))
                    {

                        count = (from cpe in db.ChildProgEnrollments
                                 where cpe.ChildSchoolYearId.Equals(objChildData.ChildSchoolYearId) &&
                                 cpe.SchoolProgramId.Equals(objChildProgEnrollment.SchoolProgramId) &&
                                     //cpe.ProgClassRoomId.Equals(objChildProgEnrollment.ProgClassRoomId) &&
                                 cpe.DayIndex.Equals(objChildProgEnrollment.DayIndex) //&&
                                 //cpe.DayType.Equals(objChildProgEnrollment.DayType)
                                 select cpe).Count();

                        if (count == 0)
                        {
                            db.ChildProgEnrollments.InsertOnSubmit(DBChildProgEnrollment);
                        }
                    }
                    // }
                    if (!objChildProgEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT) || count == 0)
                    {
                        db.SubmitChanges();
                        result = DBChildProgEnrollment.Id;
                        #region Automatic Insert into ChildProgEnrollmentFeeDetail
                        if (IsAddInChildProgEnrollmentFeeDetail == false)
                        {
                            objChildProgEnrollment.StartDate = objChildData.StartDate;
                            objChildProgEnrollment.EndDate = objChildData.EndDate;
                            if (!clChildProgEnrollmentFeeDetail.Save(objChildProgEnrollment, objChildData.ChildFamilyId, db, tran))
                            {
                                return null;
                            }
                        }
                        IsAddInChildProgEnrollmentFeeDetail = true;
                        #endregion
                    }
                    objChildProgEnrollment.Id = result;
                }
                return objChildData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentSave", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                //result = new Guid(DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Delete Child Program Enrollment", Dt:7-Sep-2011, Db:A"
        public static int ChildProgEnrollmentDelete(Guid Id)
        {
            int result = 0;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", "ChildProgEnrollmentDelete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            ChildProgEnrollment DBChildProgEnrollment = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", "Debug ChildProgEnrollmentDelete Method", DayCarePL.Common.GUID_DEFAULT);
                DBChildProgEnrollment = db.ChildProgEnrollments.FirstOrDefault(i => i.Id.Equals(Id));
                db.ChildProgEnrollments.DeleteOnSubmit(DBChildProgEnrollment);
                db.SubmitChanges();
                result = 1;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = 0;
            }
            return result;
        }
        #endregion

        #region "Delete School Program Child Program Enrollment", Dt:22-Sep-2011, Db:A"
        public static bool DeleteSchoolProgramChildProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", "ChildProgEnrollmentDelete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            SqlConnection con = clConnection.CreateConnection();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", "Debug ChildProgEnrollmentDelete Method", DayCarePL.Common.GUID_DEFAULT);
                clConnection.OpenConnection(con);
                SqlCommand cmd = clConnection.CreateCommand("spDeleteSchoolProgramChildProgEnrollment", con);
                cmd.Parameters.Add(clConnection.GetInputParameter("@ChildSchoolYearId", ChildSchoolYearId));
                cmd.Parameters.Add(clConnection.GetInputParameter("@SchoolProgramId", SchoolProgramId));
                cmd.Parameters.Add(clConnection.GetOutputParameter("@status", SqlDbType.Bit));
                cmd.ExecuteNonQuery();
                if (Convert.ToBoolean(cmd.Parameters["@status"].Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "ChildProgEnrollmentDelete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }

        }
        #endregion

        #region "Get School Program Wise Enrollment", Dt:7-Sep-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ChildProgEnrollment, "LoadProgEnrollment", "LoadProgEnrollment method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ChildProgEnrollment, "LoadProgEnrollment", "Debug LoadProgEnrollment called", DayCarePL.Common.GUID_DEFAULT);
                var data = (from cpe in db.ChildProgEnrollments
                            join fp in db.FeesPeriods on cpe.FeesPeriodId equals fp.Id into fprd
                            from fsprd in fprd.DefaultIfEmpty()
                            where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId) &&
                            cpe.SchoolProgramId.Equals(SchoolProgramId)
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                Id = cpe.Id,
                                ChildSchoolYearId = cpe.ChildSchoolYearId,
                                ProgClassRoomId = cpe.ProgClassRoomId,
                                CreatedById = cpe.CreatedById,
                                DayIndex = cpe.DayIndex,
                                DayType = cpe.DayType,
                                SchoolProgramId = cpe.SchoolProgramId,
                                Fees = cpe.Fees.Value,
                                FeesPeriodId = cpe.FeesPeriodId.Value,
                                FeesPeriodName = fsprd.Name,
                                StartDate = cpe.StartDate,
                                EndDate = cpe.EndDate
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ChildProgEnrollment, "LoadProgEnrollment", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion

        #region "Gell All Program Lists by ChildSchoolYearId", Dt:7-Sep-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            //List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
            //DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = null;
            try
            {
                //var data = db.spGetChildProgEnrollmentAll(ChildSchoolYearId);
                //foreach (var d in data)
                //{
                //    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                //    objChildProgEnrollment.Id = d.Id;
                //    objChildProgEnrollment.LastModifiedById = d.LastModifiedById;
                //    objChildProgEnrollment.ProgClassRoomId = d.ProgClassRoomId;
                //    objChildProgEnrollment.ChildSchoolYearId = d.ChildSchoolYearId;
                //    objChildProgEnrollment.SchoolProgramId = d.SchoolProgramId;
                //    objChildProgEnrollment.DayIndex = d.DayIndex;
                //    objChildProgEnrollment.Day = d.Day;
                //    objChildProgEnrollment.DayType = d.DayType;
                //    objChildProgEnrollment.CreatedById = d.CreatedById;
                //    objChildProgEnrollment.CreateDateTime = d.CreatedDateTime;
                //    objChildProgEnrollment.LastModifiedDateTime = d.LastModifiedDateTime;
                //    objChildProgEnrollment.ProgramTitle = d.Program;
                //    objChildProgEnrollment.ClassRoom = d.ClassRoomTitle;
                //    lstChildProgEnrollment.Add(objChildProgEnrollment);
                //    objChildProgEnrollment = null;
                //}
                var data = (from cpe in db.ChildProgEnrollments
                            join sp in db.SchoolPrograms on cpe.SchoolProgramId equals sp.Id
                            join csy in db.ChildSchoolYears on cpe.ChildSchoolYearId equals csy.Id
                            where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId)
                            orderby sp.Title ascending
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                SchoolProgramId = cpe.SchoolProgramId,
                                ProgramTitle = sp.Title,
                                IsPrimary = sp.IsPrimary,
                                ChildSchoolYearId = cpe.ChildSchoolYearId,
                                ChildDataId = csy.ChildDataId
                            }).Distinct().ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
            //return lstChildProgEnrollment;
        }
        #endregion

        #region "Gell All Distinct Program Lists by ChildSchoolYearId", Dt:7-Sep-2011, Db:A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> LoadAllDistinctProgEnrolled(Guid ChildSchoolYearId)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                var data = (from cpe in db.ChildProgEnrollments
                            where cpe.ChildSchoolYearId.Equals(ChildSchoolYearId)
                            select new DayCarePL.ChildProgEnrollmentProperties()
                            {
                                SchoolProgramId = cpe.SchoolProgramId,
                            }).Distinct().ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region Get Fees Period From SchoolProgramFeesDetails, Dt: 13-Sept-2011, DB: A"
        public static List<DayCarePL.FeesPeriodProperties> GetFessPeriodFromSchoolProgramFeesDetail(Guid SchoolProgramId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "GetFessPeriodFromSchoolProgramFeesDetail", "Execute GetFessPeriodFromSchoolProgramFeesDetail Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "GetFessPeriodFromSchoolProgramFeesDetail", "Debug GetFessPeriodFromSchoolProgramFeesDetail Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from spfd in db.SchoolProgramFeesDetails
                            join fp in db.FeesPeriods on spfd.FeesPeriodId equals fp.Id
                            where spfd.SchoolProgramId.Equals(SchoolProgramId)
                            select new DayCarePL.FeesPeriodProperties()
                            {
                                Id = spfd.FeesPeriodId,
                                Name = fp.Name
                            }).ToList();

                return data;// lstChildProgEnrollment;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "GetFessPeriodFromSchoolProgramFeesDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion
        #endregion

        #region Child Enrolment
        #region "Save ChildEnrollmentStatus, Dt:7-Sept-2011, Db:A"
        public static bool ChildEnrollmentStatusSave(DayCarePL.AddEdditChildProperties objChildEnrollment, System.Data.Common.DbTransaction tran, DayCareDataContext dbMain)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "ChildEnrollmentStatusSave", "Execute ChildEnrollmentStatusSave Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = dbMain;
            ChildEnrollmentStatus DBChildEnrollmentStatus = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "ChildEnrollmentStatusSave", "Debug ChildEnrollmentStatusSave Method", DayCarePL.Common.GUID_DEFAULT);
                db.Transaction = tran;
                int EnrollmentCount = 0;
                if (!objChildEnrollment.EnrollmentStatusId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    EnrollmentCount = (from ces in db.ChildEnrollmentStatus
                                       where ces.EnrollmentStatusId.Equals(objChildEnrollment.EnrollmentStatusId) &&
                                       ces.ChildSchoolYearId.Equals(objChildEnrollment.ChildSchoolYearId)
                                       select ces).Count();

                    if (objChildEnrollment.ChildEnrollmentId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT) || EnrollmentCount == 0)
                    {
                        //cmd = clConnection.CreateCommand("spAddChildEnrollmentStatus", conn);
                        DBChildEnrollmentStatus = new ChildEnrollmentStatus();
                        DBChildEnrollmentStatus.Id = Guid.NewGuid();
                        DBChildEnrollmentStatus.CreatedDateTime = DateTime.Now;
                        DBChildEnrollmentStatus.CreatedById = objChildEnrollment.CreatedById;
                    }
                    else
                    {
                        DBChildEnrollmentStatus = db.ChildEnrollmentStatus.FirstOrDefault(i => i.Id.Equals(objChildEnrollment.ChildEnrollmentId));

                    }
                    DBChildEnrollmentStatus.ChildSchoolYearId = objChildEnrollment.ChildSchoolYearId;
                    DBChildEnrollmentStatus.EnrollmentStatusId = objChildEnrollment.EnrollmentStatusId;
                    DBChildEnrollmentStatus.EnrollmentDate = objChildEnrollment.EnrollmentDate;
                    DBChildEnrollmentStatus.Comments = objChildEnrollment.ChildEnrollmentComments;
                    DBChildEnrollmentStatus.LastModifiedDatetime = DateTime.Now;
                    DBChildEnrollmentStatus.LastModifiedById = objChildEnrollment.LastModifiedById;
                    if (objChildEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT) || EnrollmentCount == 0)
                    {
                        if (EnrollmentCount == 0)
                        {
                            db.ChildEnrollmentStatus.InsertOnSubmit(DBChildEnrollmentStatus);
                        }
                    }
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "ChildEnrollmentStatusSave", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Load ChildEnrollmentStatus Data, Dt:7-Sept-2011, Db:A"
        public static List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildEnrollmentStatus", "LoadChildEnrollmentStatus method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildEnrollmentStatus", "Debug LoadChildEnrollmentStatus called", DayCarePL.Common.GUID_DEFAULT);
                var data = (from ces in db.ChildEnrollmentStatus
                            join es in db.EnrollmentStatus on ces.EnrollmentStatusId equals es.Id
                            where es.SchoolId.Equals(SchoolId) && ces.ChildSchoolYearId.Equals(ChildSchoolYearId) &&
                            es.Active.Equals(true)
                            select new DayCarePL.ChildEnrollmentStatusProperties()
                            {
                                Id = ces.Id,
                                EnrollmentStatusId = ces.EnrollmentStatusId,
                                EnrollmentStatus = es.Status,
                                ChildSchoolYearId = ces.ChildSchoolYearId,
                                EnrollmentDate = ces.EnrollmentDate,
                                Comments = ces.Comments
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "LoadChildEnrollmentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }

        }
        #endregion

        #region "Check DuplicateChildEnrollmentStatus, Dt:7-Sept-2011, Db:A"
        public static bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAddEditChild, "CheckDuplicateChildEnrollmentStatus", "Execute CheckDuplicateChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAddEditChild, "CheckDuplicateChildEnrollmentStatus", "Debug CheckDuplicateChildEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
                int data = 0;
                data = (from ces in db.ChildEnrollmentStatus
                        where ces.ChildSchoolYearId.Equals(ChildSchoolYearId) &&
                        ces.EnrollmentStatusId.Equals(EnrollmentStatusId) &&
                        ces.EnrollmentDate.Equals(EnrollmentDate) &&
                        !ces.Id.Equals(Id)
                        select ces).Count();
                if (data > 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAddEditChild, "CheckDuplicateChildEnrollmentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #endregion

        #region "Get SchoolProgram Is primary,Dt:23-01-2012,Db:V"
        public static List<DayCarePL.ChildProgEnrollmentProperties> GetIsPrimarySchoolProgram(Guid ChildSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildList, "GetIsPrimarySchoolProgram", "GetIsPrimarySchoolProgram method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildProgEnrollmentProperties> lstChildProgEnrollment = new List<DayCarePL.ChildProgEnrollmentProperties>();
            DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildList, "GetIsPrimarySchoolProgram", "Debug GetIsPrimarySchoolProgram called", DayCarePL.Common.GUID_DEFAULT);
                var data = db.spGetIsPrimarySchoolProgram(ChildSchoolYearId);
                foreach (var d in data)
                {
                    objChildProgEnrollment = new DayCarePL.ChildProgEnrollmentProperties();
                    objChildProgEnrollment.ChildFamilyId = d.ChildFamilyId.Value;
                    objChildProgEnrollment.SchoolProgramId = d.SchoolProgramId.Value;
                    lstChildProgEnrollment.Add(objChildProgEnrollment);


                }
                return lstChildProgEnrollment;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildList, "GetIsPrimarySchoolProgram", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion
    }
}
