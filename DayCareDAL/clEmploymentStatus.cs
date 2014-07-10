using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clEmploymentStatus
    {
        #region "Save Record into EmploymentStatus, Dt:2-Sep-2011, Db:V"
        public static bool Save(DayCarePL.EmploymentStatusProperties objEmployment)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEmploymentStatus, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            EmploymentStatus DBEmploymentStatus = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEmploymentStatus, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objEmployment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBEmploymentStatus = new EmploymentStatus();
                    DBEmploymentStatus.Id = System.Guid.NewGuid();
                }
                else
                {
                   
                    DBEmploymentStatus = db.EmploymentStatus.SingleOrDefault(e => e.Id.Equals(objEmployment.Id));
                }
                DBEmploymentStatus.LastModifiedById = objEmployment.LastModifiedById;
                DBEmploymentStatus.LastModifiedDatetime = DateTime.Now;
                DBEmploymentStatus.SchoolId = objEmployment.SchoolId;
                DBEmploymentStatus.Status = objEmployment.Status;
                DBEmploymentStatus.Active = objEmployment.Active;
                DBEmploymentStatus.Comments = objEmployment.Comments;
                DBEmploymentStatus.LastModifiedById = objEmployment.LastModifiedById;
                DBEmploymentStatus.LastModifiedDatetime = DateTime.Now;
                if (objEmployment.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    
                    db.EmploymentStatus.InsertOnSubmit(DBEmploymentStatus);
                }
                db.SubmitChanges();
                result = true;


            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEmploymentStatus, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

            }
            return result;
        }
        #endregion

        #region Load EmploymentStatus, Dt:2-Sep-2011, Db:V"
        public static DayCarePL.EmploymentStatusProperties[] LoadEmploymentStatus(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEmploymentStatus, "LoadEmploymentStatus", "Execute LoadEmploymentStatus Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEmploymentStatus, "LoadEmploymentStatus", "Debug LoadEmploymentStatus Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from E in db.EmploymentStatus
                            where E.SchoolId.Equals(SchoolId)
                            orderby E.Status ascending
                            select new DayCarePL.EmploymentStatusProperties()
                            {
                                Id = E.Id,
                                Status = E.Status,
                                Active = E.Active,
                                Comments = E.Comments,

                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEmploymentStatus, "LoadEmploymentStatus", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;

            }
        }
        #endregion

        #region "Check Duplicate EmploymentStatus, Dt:2-Sep-2011, Db:V"
        public static bool CheckDuplicateEmploymentStatusName(string EmploymentStatusName, Guid EmploymentStatusId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clEmploymentStatus, "CheckDuplicateEmploymentStatusName", "Execute CheckDuplicateEmploymentStatusName Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clEmploymentStatus, "CheckDuplicateEmploymentStatusName", "Debug CheckDuplicateEmploymentStatusName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (EmploymentStatusId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.EmploymentStatus
                             where s.Status.Equals(EmploymentStatusName) //&& ug.Id.Equals(UserGroupId)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.EmploymentStatus
                             where s.Status.Equals(EmploymentStatusName) //&& ug.Id.Equals(UserGroupId) 
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(EmploymentStatusId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clEmploymentStatus, "CheckDuplicateEmploymentStatusName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
