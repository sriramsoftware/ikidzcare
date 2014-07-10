using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clClassRoom
    {
        #region "Save ClassRoom, Dt:4-Aug-2011 , Db:V"
        public static bool Save(DayCarePL.ClassRoomProperties objClassRoom)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;



            DayCareDataContext db = new DayCareDataContext();
            ClassRoom DBClassRoom = null;


            try
            {

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBClassRoom = new ClassRoom();
                    DBClassRoom.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBClassRoom = db.ClassRooms.SingleOrDefault(D => D.Id.Equals(objClassRoom.Id));
                }
                DBClassRoom.LastModifiedById = objClassRoom.LastModifiedById;
                DBClassRoom.LastModifiedDatetime = DateTime.Now;
                DBClassRoom.Name = objClassRoom.Name;
                DBClassRoom.SchoolId = objClassRoom.SchoolId;
                DBClassRoom.MaxSize = objClassRoom.MaxSize;
                DBClassRoom.Active = objClassRoom.Active;
                DBClassRoom.StaffId = objClassRoom.StaffId;

                if (objClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ClassRooms.InsertOnSubmit(DBClassRoom);
                }



                db.SubmitChanges();


                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;


            }
            finally
            {


            }
            return result;
        }
        #endregion

        #region "Save ClassRoom, Dt:4-Aug-2011 , Db:P"
        public static bool SaveClassRoomYearWise(DayCarePL.ClassRoomProperties objClassRoom, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;

            //declare trans variable
            System.Data.Common.DbTransaction trans = null;

            DayCareDataContext db = new DayCareDataContext();
            ClassRoom DBClassRoom = null;
            ClassRoomSchoolYear DBClassRoomSchoolYear = null;

            try
            {
                // Open the connection
                db.Connection.Open();

                // Begin the transaction
                trans = db.Connection.BeginTransaction();
                // Assign transaction to context class
                // All the database operation perform by this object will now use
                //transaction 
                db.Transaction = trans;

                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBClassRoom = new ClassRoom();
                    DBClassRoom.Id = System.Guid.NewGuid();

                    DBClassRoomSchoolYear = new ClassRoomSchoolYear();
                    DBClassRoomSchoolYear.Id = System.Guid.NewGuid();
                    DBClassRoomSchoolYear.ClassRoomName = objClassRoom.Name;
                    DBClassRoomSchoolYear.CreatedById = objClassRoom.LastModifiedById;
                    DBClassRoomSchoolYear.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBClassRoom = db.ClassRooms.SingleOrDefault(D => D.Id.Equals(objClassRoom.Id));
                    DBClassRoomSchoolYear = db.ClassRoomSchoolYears.FirstOrDefault(u => u.ClassRoomId.Equals(objClassRoom.Id) && u.SchoolYearId.Equals(SchoolYearId));
                }
                DBClassRoom.LastModifiedById = objClassRoom.LastModifiedById;
                DBClassRoom.LastModifiedDatetime = DateTime.Now;
                DBClassRoom.Name = objClassRoom.Name;
                DBClassRoom.SchoolId = objClassRoom.SchoolId;
                DBClassRoom.MaxSize = objClassRoom.MaxSize;
                DBClassRoom.Active = objClassRoom.Active;
                DBClassRoom.StaffId = objClassRoom.StaffId;

                //class school year
                DBClassRoomSchoolYear.ClassRoomId = DBClassRoom.Id;
                DBClassRoomSchoolYear.SchoolYearId = SchoolYearId;
                DBClassRoomSchoolYear.Active = objClassRoom.Active;
                DBClassRoomSchoolYear.StaffId = objClassRoom.StaffId;
                DBClassRoomSchoolYear.ClassRoomName = objClassRoom.Name;
                DBClassRoomSchoolYear.LastModifiedById = objClassRoom.LastModifiedById;
                DBClassRoomSchoolYear.LastModifiedDateTime = DateTime.Now;


                if (objClassRoom.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ClassRooms.InsertOnSubmit(DBClassRoom);
                    db.ClassRoomSchoolYears.InsertOnSubmit(DBClassRoomSchoolYear);
                }

                db.SubmitChanges();

                // Commit transaction
                trans.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

                // Rollback transaction
                if (trans != null)
                    trans.Rollback();
            }
            finally
            {

                // Close the connection
                if (db.Connection.State == ConnectionState.Open)
                    db.Connection.Close();
            }
            return result;
        }
        #endregion


        #region "LoadClassRoom, Dt:4-Aug-2011,Db:V"
        public static DayCarePL.ClassRoomProperties[] LoadClassRoom(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Execute LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Debug LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from C in db.ClassRooms
                            where C.SchoolId.Equals(SchoolId) && !C.Name.Equals("N/A")
                            orderby C.Name ascending
                            select new DayCarePL.ClassRoomProperties()
                            {
                                Id = C.Id,
                                Name = C.Name,
                                MaxSize = C.MaxSize,
                                Active = C.Active,
                                StaffId = C.StaffId
                            }).ToArray();

                if (data != null)
                {
                    foreach (DayCarePL.ClassRoomProperties objClassRoom in data)
                    {
                        string staffname = (from s in db.Staffs
                                            where s.Id.Equals(objClassRoom.StaffId)
                                            select s.FirstName + " " + s.LastName).SingleOrDefault();
                        //{
                        //    Name = s.FirstName + " " + s.LastName
                        //});

                        if (staffname != null)
                        {
                            objClassRoom.FullName = staffname;//.Select(s => s.Name).SingleOrDefault();
                        }
                        else
                        {
                            objClassRoom.FullName = "N/A";
                        }



                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Load Classroom Report ,Dt:21-01-2012"
        public static DayCarePL.ClassRoomProperties[] LoadClassRoomReport(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Execute LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Debug LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from C in db.ClassRooms
                            join crsy in db.ClassRoomSchoolYears on C.Id equals crsy.ClassRoomId
                            where C.SchoolId.Equals(SchoolId) && crsy.SchoolYearId.Value.Equals(SchoolYearId)
                            orderby C.Name ascending
                            select new DayCarePL.ClassRoomProperties()
                            {
                                Id = C.Id,
                                Name = crsy.ClassRoomName,
                                MaxSize = C.MaxSize,
                                Active = crsy.Active.Value,
                                StaffId = crsy.StaffId

                            }).ToArray();

                if (data != null)
                {
                    foreach (DayCarePL.ClassRoomProperties objClassRoom in data)
                    {
                        string staffname = (from s in db.Staffs
                                            where s.Id.Equals(objClassRoom.StaffId)
                                            select s.FirstName + " " + s.LastName).SingleOrDefault();
                        //{
                        //    Name = s.FirstName + " " + s.LastName
                        //});

                        if (staffname != null)
                        {
                            objClassRoom.FullName = staffname;//.Select(s => s.Name).SingleOrDefault();
                        }
                        else
                        {
                            objClassRoom.FullName = "N/A";
                        }



                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "CheckDuplicate classRoom, Dt:5-Aug-2011, Db:V"
        public static bool CheckDuplicateClassRoomName(string ClassRoomName, Guid ClassRoomId, Guid SchoolId, Guid StaffId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", "Execute LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            int count1 = 0;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", "Debug CheckDuplicateClassRoomName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                // 
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (ClassRoomId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    //count = (from s in db.ClassRooms
                    //         where s.Name.Equals(ClassRoomName) //&& ug.Id.Equals(UserGroupId)
                    //         && s.SchoolId.Equals(SchoolId)
                    //         select s).Count();

                    //modified by vimal check duplicate classrooms name in classroomschoolyear table 03-06-2013

                    count = (from csy in db.ClassRoomSchoolYears
                             join sy in db.SchoolYears on csy.SchoolYearId equals sy.Id
                             where csy.ClassRoomName.Equals(ClassRoomName) && sy.SchoolId.Equals(SchoolId)
                             && csy.SchoolYearId.Equals(SchoolYearId)
                             select csy).Count();

                }
                else
                {
                    //count = (from s in db.ClassRooms
                    //         where s.Name.Equals(ClassRoomName) //&& ug.Id.Equals(UserGroupId) 
                    //         && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(ClassRoomId)
                    //         select s).Count();

                    //modified by vimal check duplicate classrooms name in classroomschoolyear table 03-06-2013
                    count = (from csy in db.ClassRoomSchoolYears
                             join sy in db.SchoolYears on csy.SchoolYearId equals sy.Id
                             where csy.ClassRoomName.Equals(ClassRoomName) && sy.SchoolId.Equals(SchoolId)
                             && csy.SchoolYearId.Equals(SchoolYearId) && !csy.ClassRoomId.Equals(ClassRoomId)
                             select csy).Count();

                }
                if (!StaffId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count1 = (from s in db.ClassRooms
                              join csy in db.ClassRoomSchoolYears on s.Id equals csy.ClassRoomId
                              where
                              s.SchoolId.Equals(SchoolId) && csy.StaffId.Equals(StaffId)
                              && !s.Id.Equals(ClassRoomId) && csy.SchoolYearId.Equals(SchoolYearId)
                              select s).Count();

                }

                if (count > 0 || count1 > 0)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Get Classroom wise student weekly schedule"
        public static DataSet GetClassroomWiseStudentWeeklySchedule(Guid ClassRoomId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "GetClassroomWiseStudentWeeklySchedule", "Execute GetClassroomWiseStudentWeeklySchedule Method", DayCarePL.Common.GUID_DEFAULT);
            DataSet ds = new DataSet();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "GetClassroomWiseStudentWeeklySchedule", "Debug GetClassroomWiseStudentWeeklySchedule Method", DayCarePL.Common.GUID_DEFAULT);
                SortedList sl = new SortedList();
                sl.Add("@ClassRoomId", ClassRoomId);
                sl.Add("@SchoolYearId", SchoolYearId);
                ds = clConnection.GetDataSet("spRptClassRoomWiseStudentPresent", sl);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "GetClassroomWiseStudentWeeklySchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return ds;
        }
        #endregion

        #region "Delete ClassRoom"
        public static bool Delete(Guid Id, Guid SchoolYearID)
        {
            bool result = false;
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "Delete", "Delete Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "Delete", "Debug Delete Method", DayCarePL.Common.GUID_DEFAULT);

                ClassRoom DBClassRoom = db.ClassRooms.FirstOrDefault(c => c.Id.Equals(Id));

                ProgClassRoom DBProgClassRoom = db.ProgClassRooms.FirstOrDefault(pc => pc.ClassRoomId.Equals(Id) && pc.ClassRoomSchoolYearId.Equals(SchoolYearID));

                ClassRoomSchoolYear DBClassRoomSchoolYear = db.ClassRoomSchoolYears.FirstOrDefault(crsy => crsy.ClassRoomId.Equals(Id) && crsy.SchoolYearId.Equals(SchoolYearID));

                if (DBClassRoomSchoolYear != null)
                {
                    db.ClassRoomSchoolYears.DeleteOnSubmit(DBClassRoomSchoolYear);
                }
                if (DBProgClassRoom != null)
                {
                    db.ProgClassRooms.DeleteOnSubmit(DBProgClassRoom);
                    db.SubmitChanges();
                    result = true;
                }
                if (DBClassRoom != null)
                {
                    db.ClassRooms.DeleteOnSubmit(DBClassRoom);
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "Delete", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Check ClassRoom already assigned SchoolProgram"
        public static bool CheckClassRoomAssignedSchoolProgramm(Guid ClassRoomId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", "Execute LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", "Debug CheckDuplicateClassRoomName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                count = (from pc in db.ProgClassRooms
                         where pc.ClassRoomId.Equals(ClassRoomId) && pc.Active.Equals(true)
                         select pc).Count();
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "CheckDuplicateClassRoomName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Report: Student Schedule"
        public static DataSet GetStudentSchedule(Guid ClassRoomId, Guid SchoolYearId, string LastNameFrom, string LastNameTo)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "spRptStudentSchedule", "Execute spRptStudentSchedule Method", DayCarePL.Common.GUID_DEFAULT);
            DataSet ds = new DataSet();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "spRptStudentSchedule", "Debug spRptStudentSchedule Method", DayCarePL.Common.GUID_DEFAULT);
                SortedList sl = new SortedList();
                sl.Add("@ClassRoomId", ClassRoomId);
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@LastNameFrom", LastNameFrom);
                sl.Add("@LastNameTo", LastNameTo);
                ds = clConnection.GetDataSet("spRptStudentSchedule", sl);
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "spRptStudentSchedule", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return ds;
        }
        #endregion


        #region "LoadClassRoom School Year Wise, Dt:21-May-2013,Db:P"
        public static DayCarePL.ClassRoomProperties[] LoadClassRoom(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Execute LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", "Debug LoadClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from C in db.ClassRooms
                            join csy in db.ClassRoomSchoolYears on C.Id equals csy.ClassRoomId
                            where C.SchoolId.Equals(SchoolId) && !C.Name.Equals("N/A")
                            && csy.SchoolYearId.Equals(SchoolYearId)
                            orderby C.Name ascending
                            select new DayCarePL.ClassRoomProperties()
                            {
                                Id = C.Id,
                                Name = csy.ClassRoomName,
                                MaxSize = C.MaxSize,
                                Active = csy.Active.Value,
                                StaffId = csy.StaffId
                            }).ToArray();

                if (data != null)
                {
                    foreach (DayCarePL.ClassRoomProperties objClassRoom in data)
                    {
                        string staffname = (from s in db.Staffs
                                            where s.Id.Equals(objClassRoom.StaffId)
                                            select s.FirstName + " " + s.LastName).SingleOrDefault();

                        if (staffname != null)
                        {
                            objClassRoom.FullName = staffname;//.Select(s => s.Name).SingleOrDefault();
                        }
                        else
                        {
                            objClassRoom.FullName = "N/A";
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassRoom, "LoadClassRoom", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Import AllClassRoom and Staff of Previous year to next current year"
        public static bool ImportAllActiveClassRoom(Guid SchoolYearId, Guid SchoolId, Guid OldCurrentSchoolYearId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.ClassRoom, "ImportAllActiveClassRoom", "Execute ImportAllActiveClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = dbold;
            db.Transaction = tran;
            ClassRoomSchoolYear DBClassRoomSchoolYear = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.ClassRoom, "ImportAllActiveClassRoom", "Debug ImportAllActiveClassRoom Method", DayCarePL.Common.GUID_DEFAULT);
                Guid currentschoolyearid = SchoolYearId;
                List<DayCarePL.ClassRoomProperties> lstClassroom = (from cr in db.ClassRooms
                                                                    join sy in db.SchoolYears on cr.SchoolId equals sy.SchoolId
                                                                    where cr.SchoolId.Equals(SchoolId)
                                                                    && sy.Id.Equals(OldCurrentSchoolYearId)
                                                                    && !(from crsy in db.ClassRoomSchoolYears
                                                                         where crsy.SchoolYearId.Equals(currentschoolyearid)
                                                                         select crsy.ClassRoomId).Contains(cr.Id)
                                                                    select new DayCarePL.ClassRoomProperties()
                                                                   {
                                                                       Id = cr.Id,
                                                                       SchoolYearId = sy.Id,
                                                                       Active = cr.Active,
                                                                       StaffId = cr.StaffId,
                                                                       LastModifiedById = cr.LastModifiedById,
                                                                       Name = cr.Name
                                                                   }).ToList();

                foreach (DayCarePL.ClassRoomProperties objClassroom in lstClassroom)
                {
                    DBClassRoomSchoolYear = new ClassRoomSchoolYear();
                    DBClassRoomSchoolYear.Id = Guid.NewGuid();
                    DBClassRoomSchoolYear.ClassRoomId = objClassroom.Id;
                    DBClassRoomSchoolYear.SchoolYearId = SchoolYearId;
                    DBClassRoomSchoolYear.Active = objClassroom.Active;
                    DBClassRoomSchoolYear.StaffId = objClassroom.StaffId;
                    DBClassRoomSchoolYear.CreatedById = objClassroom.LastModifiedById;
                    DBClassRoomSchoolYear.CreatedDateTime = DateTime.Now.Date;
                    DBClassRoomSchoolYear.LastModifiedById = objClassroom.LastModifiedById;
                    DBClassRoomSchoolYear.LastModifiedDateTime = DateTime.Now.Date;
                    DBClassRoomSchoolYear.ClassRoomName = objClassroom.Name;
                    db.ClassRoomSchoolYears.InsertOnSubmit(DBClassRoomSchoolYear);
                    db.SubmitChanges();
                }

            }
            catch
            {

            }
            return true;
        }
        #endregion

        #region"update ClassRoomSchoolYearId into ProgClassRoom table."
        public static bool UpdateClassRoomSchoolYearID(Guid SchoolYearId, Guid OldCurrentSchoolYearID, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {

            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;

            //declare trans variable
            // System.Data.Common.DbTransaction trans = null;
            DayCareDataContext db = dbold;
            db.Transaction = tran;
            ProgClassRoom DBProgClassRoom = null;
            try
            {


                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "Update", "Debug Update Method", DayCarePL.Common.GUID_DEFAULT);

                var ClassRoomSchoolYear = (from pcr in db.ProgClassRooms
                                           join sp in db.SchoolPrograms on pcr.SchoolProgramId equals sp.Id
                                           join csy in db.ClassRoomSchoolYears on pcr.ClassRoomSchoolYearId equals csy.Id
                                           where sp.SchoolYearId.Equals(OldCurrentSchoolYearID) && csy.ClassRoomId == pcr.ClassRoomId
                                           select new
                                           {
                                               pcrId = pcr.Id,
                                               csyId = csy.Id
                                           }).ToList();

                var cl = ClassRoomSchoolYear;
                for (int cnt = 0; cnt < cl.Count(); cnt++)
                {
                    DBProgClassRoom = db.ProgClassRooms.FirstOrDefault(pc => pc.Id.Equals(cl[cnt].pcrId));
                    if (DBProgClassRoom != null)
                    {
                        DBProgClassRoom.ClassRoomSchoolYearId = cl[cnt].csyId;
                        db.SubmitChanges();
                        DBProgClassRoom = null;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region"update ClassRoomSchoolYearId into ProgClassRoom table of Last Added Year."
        public static bool UpdateLastAddedYearClassRoomSchoolYearID(Guid SchoolYearId, Guid OldCurrentSchoolYearID, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {

            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassRoom, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;

            //declare trans variable
            // System.Data.Common.DbTransaction trans = null;
            DayCareDataContext db = dbold;
            db.Transaction = tran;
            ProgClassRoom DBProgClassRoom = null;
            try
            {


                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassRoom, "Update", "Debug Update Method", DayCarePL.Common.GUID_DEFAULT);

                var ClassRoomSchoolYear = (from pcr in db.ProgClassRooms
                                           join sp in db.SchoolPrograms on pcr.SchoolProgramId equals sp.Id
                                           where sp.SchoolYearId.Equals(SchoolYearId) 
                                           select new
                                           {
                                               pcrId = pcr.Id                                                
                                           }).ToList();

                var cl = ClassRoomSchoolYear;
                for (int cnt = 0; cnt < cl.Count(); cnt++)
                {
                    DBProgClassRoom = db.ProgClassRooms.FirstOrDefault(pc => pc.Id.Equals(cl[cnt].pcrId));
                    if (DBProgClassRoom != null)
                    {
                        DBProgClassRoom.ClassRoomSchoolYearId = db.ClassRoomSchoolYears.FirstOrDefault(u => u.ClassRoomId.Value.Equals(DBProgClassRoom.ClassRoomId) && u.SchoolYearId.Value.Equals(SchoolYearId)).Id;
                        db.SubmitChanges();
                        DBProgClassRoom = null;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
