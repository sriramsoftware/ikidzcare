using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clHoursOfOperation
    {
        #region "Load Hours Of Operation, Dt: 8-Aug-2011, DB: A"
        public static List<DayCarePL.HoursOfOperationProperties> LoadHoursOfOperation(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clHoursOfOperation, "LoadHoursOfOperation", "Execute LoadHoursOfOperation Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clHoursOfOperation, "LoadHoursOfOperation", "Debug LoadHoursOfOperation Method", DayCarePL.Common.GUID_DEFAULT);

                var data = (from h in db.HoursOfOperations
                            where h.SchoolId.Equals(SchoolId)
                            orderby h.DayIndex ascending
                            select new DayCarePL.HoursOfOperationProperties()
                            {
                                Id = h.Id,
                                SchoolId = h.SchoolId,
                                Day = h.Day,
                                DayIndex = h.DayIndex,
                                OpenTime = h.OpenTime,
                                CloseTime = h.CloseTime,
                                Comments = h.Comments,
                                LastModifiedById = h.LastModifiedById,
                                LastModifiedDatetime = h.LastModifiedDatetime
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clHoursOfOperation, "LoadHoursOfOperation", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save Hours Of Operation, Dt: 8-Aug-2011, DB: A"
        public static bool Save(DayCarePL.HoursOfOperationProperties objHoursOfOperation)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clHoursOfOperation, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            HoursOfOperation DBHourOfOeration = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clHoursOfOperation, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objHoursOfOperation.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBHourOfOeration = new HoursOfOperation();
                    DBHourOfOeration.Id = Guid.NewGuid();
                }
                else
                {
                    DBHourOfOeration = db.HoursOfOperations.SingleOrDefault(id => id.Id.Equals(objHoursOfOperation.Id));
                }
                DBHourOfOeration.LastModifiedById = objHoursOfOperation.LastModifiedById;
                DBHourOfOeration.LastModifiedDatetime = DateTime.Now;
                DBHourOfOeration.Day = objHoursOfOperation.Day;
                DBHourOfOeration.DayIndex = objHoursOfOperation.DayIndex;
                DBHourOfOeration.OpenTime = objHoursOfOperation.OpenTime;
                DBHourOfOeration.CloseTime = objHoursOfOperation.CloseTime;
                DBHourOfOeration.Comments = objHoursOfOperation.Comments;
                DBHourOfOeration.SchoolId = objHoursOfOperation.SchoolId;

                if (objHoursOfOperation.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.HoursOfOperations.InsertOnSubmit(DBHourOfOeration);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clHoursOfOperation, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }

        }
        #endregion
    }
}
