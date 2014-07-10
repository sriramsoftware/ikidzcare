using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clFeesPeriod
    {
        #region "Save Record into FeesPeriod, Dt: 30-Jul-2011, Db: V"
        public static bool Save(DayCarePL.FeesPeriodProperties objFeesPeriod)
        { 
          DayCarePL.Logger.Write(DayCarePL.LogType.INFO ,DayCarePL.ModuleToLog.clFeesPeriod ,"Save","Execute Save Method",DayCarePL .Common.GUID_DEFAULT);
          clConnection.DoConnection();
          bool result = false;
          DayCareDataContext db = new DayCareDataContext();
          FeesPeriod DBFeesPeriod = null;
          try
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFeesPeriod, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
              if (objFeesPeriod.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
              {
                  DBFeesPeriod = new FeesPeriod();
                  DBFeesPeriod.Id = System.Guid.NewGuid();
                  DBFeesPeriod.CreatedDateTime = DateTime.Now;
                  DBFeesPeriod.CreatedById = objFeesPeriod.CreatedById;
              }
              else
              {
                  DBFeesPeriod =db.FeesPeriods .SingleOrDefault(F => F .Id.Equals(objFeesPeriod .Id ));
              }
              DBFeesPeriod.LastModifiedById = objFeesPeriod.LastModifiedById;
              DBFeesPeriod.LastModifiedDatetime = DateTime.Now;
              DBFeesPeriod.Name = objFeesPeriod.Name;
              DBFeesPeriod.Active = objFeesPeriod.Active;
              if (objFeesPeriod.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
              {
                  db.FeesPeriods.InsertOnSubmit(DBFeesPeriod); 
              }
              db.SubmitChanges();
              result = true;              
          }
          catch (Exception ex)
          {
              DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFeesPeriod, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
              result = false;
          }
          return result;
        }   
        #endregion

        #region "LoadFeesPeriod into, Dt: 30-Jul-2011,Db:V"
        public static DayCarePL.FeesPeriodProperties[] LoadFeesPeriod()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clFeesPeriod, "LoadFeesPeriod", "Execute LoadFeesPeriod Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clFeesPeriod, "LoadFeesPeriod", "Debug LoadFeesPeriod Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from F in db.FeesPeriods
                            orderby F.Name ascending
                            select new DayCarePL.FeesPeriodProperties()
                            {
                                Id = F.Id,
                                Name = F.Name,
                                Active = F.Active.Value == null ? false : F.Active.Value,
                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clFeesPeriod, "LoadFeesPeriod", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        
        }
        #endregion
    }
}
