using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clAbsentReason
    {
        #region" Save AbsentReason,Dt: 4-Aug-2011,Db:V"
        public static bool Save(DayCarePL.AbsentResonProperties objAbsentReason)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            AbsentReason DBAbsentReason = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objAbsentReason.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBAbsentReason = new AbsentReason();
                    DBAbsentReason.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBAbsentReason = db.AbsentReasons.SingleOrDefault(A => A.Id.Equals(objAbsentReason.Id));
                }
                DBAbsentReason.LastModifiedById = objAbsentReason.LastModifiedById;
                DBAbsentReason.LastModifiedDatetime = DateTime.Now;
                // DBAbsentReason.Id=objAbsentReason.Id;
                DBAbsentReason.Reason = objAbsentReason.Reason;
                DBAbsentReason.SchoolId = objAbsentReason.SchoolId;
                DBAbsentReason.Active = objAbsentReason.Active;
                DBAbsentReason.BillingAffected = objAbsentReason.BillingAffected;
                DBAbsentReason.Comments = objAbsentReason.Comments;
                DBAbsentReason.LastModifiedById = objAbsentReason.LastModifiedById;
                DBAbsentReason.LastModifiedDatetime = DateTime.Now;
                if (objAbsentReason.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.AbsentReasons.InsertOnSubmit(DBAbsentReason);

                }
                db.SubmitChanges();
                result = true;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Load AbsentReason,Dt:-Aug-2011,Db:V"
        public static DayCarePL.AbsentResonProperties[] LoadAbsentReason(Guid SchoolId)
        {

            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "LoadAbsentReason", "Execute LoadAbsentReason Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "LoadAbsentReason", "Debug LoadAbsentReason Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from A in db.AbsentReasons
                            where A.SchoolId.Equals(SchoolId)
                            orderby A.Reason ascending
                            select new DayCarePL.AbsentResonProperties()
                            {
                                Id = A.Id,
                                Reason = A.Reason,
                                Active = A.Active,
                                Comments = A.Comments,
                                BillingAffected = A.BillingAffected,
                                SchoolId = A.SchoolId,
                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "LoadAbsentReason", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check Duplicate Reason,Dt:5-Aug-2011, Db:V"
        public static bool CheckDuplicateAbsentReason(string AbsentReason, Guid AbsentReasonId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clAbsentReason, "CheckDuplicateAbsentReason", "Execute CheckDuplicateAbsentReason Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clAbsentReason, "CheckDuplicateAbsentReason", "Debug CheckDuplicateAbsentReason Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (AbsentReasonId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.AbsentReasons
                             where s.Reason.Equals(AbsentReason) //&& ug.Id.Equals(UserGroupId)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.AbsentReasons
                             where s.Reason.Equals(AbsentReason) //&& ug.Id.Equals(UserGroupId) 
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(AbsentReasonId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clAbsentReason, "CheckDuplicateAbsentReason", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
