using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clStaffSchoolYear
    {
        #region "Import All Active Staff of school, Dt: 2-Aug-2011, DB: A"
        public static bool ImportAllActiveStaff(Guid SchoolYearId, Guid SchoolId, Guid OldCurrentSchoolYearId, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffSchoolYear, "ImportAllActiveStaff", "Execute ImportAllActiveStaff Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = dbold;
            db.Transaction = tran;
            StaffSchoolYear DBStaffSchoolYear = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffSchoolYear, "ImportAllActiveStaff", "Debug ImportAllActiveStaff Method", DayCarePL.Common.GUID_DEFAULT);

               /* Guid currentschoolyearid = (from sy in db.SchoolYears
                                            where sy.CurrentId.Equals(true) && sy.SchoolId.Equals(SchoolId)
                                            select sy.Id).SingleOrDefault(); */
                Guid currentschoolyearid = SchoolYearId;

                List<Guid> lstStaff = (from s in db.Staffs
                                       join ug in db.UserGroups on s.UserGroupId equals ug.Id
                                       join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                                       where ug.SchoolId.Equals(SchoolId) && ssy.active.Equals(true) && ssy.SchoolYearId.Equals(OldCurrentSchoolYearId)
                                       && !(from sy in db.StaffSchoolYears
                                            where sy.SchoolYearId.Equals(currentschoolyearid)
                                            select sy.StaffId).Contains(s.Id)
                                       select s.Id).ToList();

                foreach (Guid staffid in lstStaff)
                {
                    try
                    {
                        DBStaffSchoolYear = new StaffSchoolYear();
                        DBStaffSchoolYear.Id = Guid.NewGuid();
                        DBStaffSchoolYear.StaffId = staffid;
                        DBStaffSchoolYear.SchoolYearId = SchoolYearId;
                        DBStaffSchoolYear.active = true;
                        db.StaffSchoolYears.InsertOnSubmit(DBStaffSchoolYear);
                        db.SubmitChanges();
                    }
                    catch
                    { }
                }

                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffSchoolYear, "ImportAllActiveStaff", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Export Staff to Staff School Year Table, Dt: 2-Aug-2011, DB: A"
        public static bool ExportStafftoStaffSchoolYear(Guid StaffId, Guid SchoolId, Guid SchoolYearId, bool Active, System.Data.Common.DbTransaction tran, DayCareDataContext dbold)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffSchoolYear, "ExportStafftoStaffSchoolYear", "Execute ExportStafftoStaffSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = dbold;
            db.Transaction = tran;
            StaffSchoolYear DBStaffSchoolYear = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffSchoolYear, "ExportStafftoStaffSchoolYear", "Debug ExportStafftoStaffSchoolYear Method", DayCarePL.Common.GUID_DEFAULT);
                //Guid SchoolYearId = (from sy in db.SchoolYears
                //                     where sy.CurrentId.Equals(true) && sy.SchoolId.Equals(SchoolId)
                //                     select sy.Id).SingleOrDefault();

                int staffcount = (from ssy in db.StaffSchoolYears
                                  where ssy.SchoolYearId.Equals(SchoolYearId) && ssy.StaffId.Equals(StaffId)
                                  select ssy.Id).Count();

                if (staffcount == 0)
                {
                    DBStaffSchoolYear = new StaffSchoolYear();
                    DBStaffSchoolYear.Id = Guid.NewGuid();
                    DBStaffSchoolYear.StaffId = StaffId;
                    DBStaffSchoolYear.SchoolYearId = SchoolYearId;
                    DBStaffSchoolYear.active = Active;
                    db.StaffSchoolYears.InsertOnSubmit(DBStaffSchoolYear);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffSchoolYear, "ExportStafftoStaffSchoolYear", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion


    }
}
