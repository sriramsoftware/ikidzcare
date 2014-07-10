using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clChargeCode
    {
        #region "Load ChargeCode, Dt: 1-Aug-2011, DB: A"
        public static List<DayCarePL.ChargeCodeProperties> LoadChargeCode()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChargeCode , "LoadChargeCode", "Execute LoadChargeCode Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChargeCode, "LoadChargeCode", "Debug LoadChargeCode Method", DayCarePL.Common.GUID_DEFAULT);
                var ChargeCodeData = (from cc in db.ChargeCodes orderby cc.Name ascending
                                      select new DayCarePL.ChargeCodeProperties()
                                      {
                                          Id = cc.Id,
                                          Name = cc.Name,
                                          Category = cc.Category,
                                          DebitCrdit = cc.DebitCrdit,
                                          CreatedById = cc.CreatedById,
                                          CreatedDateTime = cc.CreatedDateTime,
                                          LastModifiedById = cc.LastModifiedById,
                                          LastModifiedDatetime = cc.LastModifiedDatetime
                                      }).ToList();
                return ChargeCodeData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChargeCode, "LoadChargeCode", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save Charges Code, Dt: 1-Aug-2011, DB: A"
        public static bool Save(DayCarePL.ChargeCodeProperties objChargesCode)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChargeCode, "Save", "Save method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            ChargeCode DBChargeCode = null;
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChargeCode, "Save", "Debug Save called", DayCarePL.Common.GUID_DEFAULT);
                if (objChargesCode.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBChargeCode = new ChargeCode();
                    DBChargeCode.Id = Guid.NewGuid();
                    DBChargeCode.CreatedDateTime = DateTime.Now;
                    DBChargeCode.CreatedById = objChargesCode.CreatedById;
                }
                else
                {
                    DBChargeCode = db.ChargeCodes.SingleOrDefault(cc => cc.Id.Equals(objChargesCode.Id));
                }
                DBChargeCode.LastModifiedDatetime = DateTime.Now;
                DBChargeCode.LastModifiedById = objChargesCode.LastModifiedById;
                DBChargeCode.Name = objChargesCode.Name;
                DBChargeCode.Category = objChargesCode.Category;
                DBChargeCode.DebitCrdit = objChargesCode.DebitCrdit;
                if (objChargesCode.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ChargeCodes.InsertOnSubmit(DBChargeCode);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChargeCode, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
