using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "StaffService" here, you must also update the reference to "StaffService" in App.config.
    public class StaffService : IStaffService
    {
        public List<DayCarePL.StaffProperties> LoadStaff(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clStaff.LoadStaff(SchoolId, SchoolYearId);
        }

        public Guid Save(DayCarePL.StaffProperties objStaff)
        {
            return DayCareDAL.clStaff.Save(objStaff);
        }

        public bool CheckDuplicateCode(string Code, Guid StaffId, Guid SchoolId)
        {
            return DayCareDAL.clStaff.CheckDuplicateCode(Code, StaffId, SchoolId);
        }

        public bool CheckDuplicateUserName(string UserName, Guid StaffId, Guid UserGroupId)
        {
            return DayCareDAL.clStaff.CheckDuplicateUserName(UserName, StaffId, UserGroupId);
        }

        public bool CheckCodeRequire(Guid SchoolId)
        {
            return DayCareDAL.clStaff.CheckCodeRequire(SchoolId);
        }

        public DayCarePL.StaffProperties LoadStaffBystaffId(Guid StaffId, Guid CurrentSchoolYearId)
        {
            return DayCareDAL.clStaff.LoadStaffBystaffId(StaffId, CurrentSchoolYearId);
        }

        public DayCarePL.StaffProperties LoadStaffDetailsByUserNameAndPassword(string UserName, string Password, Guid SchoolId)
        {
            return DayCareDAL.clStaff.LoadStaffDetailsByUserNameAndPassword(UserName, Password, SchoolId);
        }

        public List<DayCarePL.AttendanceHistoryProperties> LoadAttendanceHistory(string ReportFor, string SearchText)
        {
            return DayCareDAL.clStaffAttendenceHistory.LoadAttendanceHistory(ReportFor, SearchText);
        }
        public DataSet LoadAttendanceHistory1(string SearchText, string SearchStr, Guid SchoolYear)
        {
            return DayCareDAL.clStaffAttendenceHistory.LoadAttendanceHistory1(SearchText,SearchStr, SchoolYear);
        }
        public DataSet LoadChildAttendanceHistory(string SearchText, string SearchStr, Guid SchoolYearId)
        {
            return DayCareDAL.clStaffAttendenceHistory.LoadChildAttendanceHistory(SearchText, SearchStr, SchoolYearId);
        }
    }
}
