using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clEnrollmentStatus
    {
        #region "Save Record into EnrollmentStatus, Dt:5-Aug-2011, Db:V"
        public static bool Save(DayCarePL.EnrollmentStatusProperties objEnrollment)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEnrollmentStatus, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            EnrollmentStatus DBEnrollmentStatus = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEnrollmentStatus, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBEnrollmentStatus = new EnrollmentStatus();
                    DBEnrollmentStatus.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBEnrollmentStatus = db.EnrollmentStatus.SingleOrDefault(E => E.Id.Equals(objEnrollment.Id));
                }
                DBEnrollmentStatus.LastModifiedById = objEnrollment.LastModifiedById;
                DBEnrollmentStatus.LastModifiedDatetime = DateTime.Now;
                DBEnrollmentStatus.SchoolId = objEnrollment.SchoolId;
                DBEnrollmentStatus.Status = objEnrollment.Status;
                DBEnrollmentStatus.Active = objEnrollment.Active;
                DBEnrollmentStatus.Comments = objEnrollment.Comments;
                DBEnrollmentStatus.LastModifiedById = objEnrollment.LastModifiedById;
                DBEnrollmentStatus.LastModifiedDatetime = DateTime.Now;
                if (objEnrollment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.EnrollmentStatus.InsertOnSubmit(DBEnrollmentStatus);
                }
                db.SubmitChanges();
                result = true;

              
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEnrollmentStatus, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

            }
            return result;
        }
        #endregion

        #region Load EnrollmentStatus, Dt:5-Aug-2011, Db:V"
        public static DayCarePL.EnrollmentStatusProperties[] LoadEnrollmentStatus(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEnrollmentStatus, "LoadEnrollmentStatus", "Execute LoadEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEnrollmentStatus, "LoadEnrollmentStatus", "Debug LoadEnrollmentStatus Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from E in db.EnrollmentStatus where E.SchoolId.Equals(SchoolId)
                            orderby E.Status ascending
                            select new DayCarePL.EnrollmentStatusProperties()
                            {
                                Id =E.Id,
                                Status=E.Status,
                                Active=E.Active,
                                Comments=E.Comments,

                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEnrollmentStatus, "LoadEnrollmentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            
            }
        }
        #endregion

        #region "Check Duplicate EnrollmentStatus, Dt:5-Aug-2011, Db:V"
        public static bool CheckDuplicateEnrollmentStatusName(string EnrollmentStatusName, Guid EnrollmentStatusId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEnrollmentStatus, "CheckDuplicateEnrollmentStatusName", "Execute CheckDuplicateEnrollmentStatusName Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEnrollmentStatus, "CheckDuplicateEnrollmentStatusName", "Debug CheckDuplicateEnrollmentStatusName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (EnrollmentStatusId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.EnrollmentStatus
                             where s.Status.Equals(EnrollmentStatusName) //&& ug.Id.Equals(UserGroupId)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.EnrollmentStatus
                             where s.Status.Equals(EnrollmentStatusName) //&& ug.Id.Equals(UserGroupId) 
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(EnrollmentStatusId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEnrollmentStatus, "CheckDuplicateEnrollmentStatusName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
