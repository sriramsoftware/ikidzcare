using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clSyncLog
    {
        #region "JSON : Save"
        public static DayCarePL.ResultStatus Save(string UserId, string DateTime)
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.ResultStatus objResultStatus = new DayCarePL.ResultStatus();
            SyncLog DBSyncLog = null;
            try
            {
                DBSyncLog = new SyncLog();
                DBSyncLog.Id = Guid.NewGuid();
                DBSyncLog.UserId = new Guid(UserId);
                DBSyncLog.Datetime = Convert.ToDateTime(DateTime);
                db.SyncLogs.InsertOnSubmit(DBSyncLog);
                db.SubmitChanges();
                objResultStatus.Status = "true";
            }
            catch 
            {
                objResultStatus.Status = "false";
            }
            return objResultStatus;
        }
        #endregion

        #region "Load SyncLog"
        public static List<DayCarePL.SyncLogProperties> LoadSyncLoad()
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                var data = (from sl in db.SyncLogs
                            join s in db.Staffs on sl.UserId equals s.Id into stf
                            from staff in stf.DefaultIfEmpty()
                            orderby sl.Datetime descending 
                            select new DayCarePL.SyncLogProperties()
                            {
                                Id = sl.Id,
                                UserId = sl.UserId,
                                FullName = staff.LastName + ", " + staff.FirstName,
                                Datetime = sl.Datetime
                            }).ToList();
                return data;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}