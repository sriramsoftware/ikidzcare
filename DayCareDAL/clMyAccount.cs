using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clMyAccount
    {
        #region "Load Staff Details in FirstName LastName PassWord Code, Dt:26-Aug-2011, Db:V"
        public static DayCarePL.StaffProperties LoadMyAccountDetails(Guid StaffId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clMyAccount, "LoadMyAccountDetails", "Execute LoadMyAccountDetails Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clMyAccount, "LoadMyAccountDetails", "Debug LoadMyAccountDetails Method", DayCarePL.Common.GUID_DEFAULT);
                var MyAccount = (from ss in db.Staffs
                                 where ss.Id.Equals(StaffId)
                                 select new DayCarePL.StaffProperties()
                                 {
                                     Id = ss.Id,
                                     FirstName = ss.FirstName,
                                     LastName = ss.LastName,
                                     UserName = ss.UserName,
                                     Password = ss.Password,
                                     Code = ss.code
                                 }).SingleOrDefault();
                return MyAccount;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clMyAccount, "LoadMyAccountDetails", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Update Staff Details, Dt:26-Aug-2011, Db:V"
        public static bool Save(DayCarePL.StaffProperties objStaff)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clMyAccount, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            Staff DBStaff = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clMyAccount, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (!objStaff.Id.Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    
                    DBStaff=db.Staffs.SingleOrDefault(s => s.Id.Equals(objStaff.Id));

                }
                DBStaff.LastModifiedById=objStaff.LastModifiedById;
                DBStaff.LastModifiedDatetime=DateTime.Now;
                DBStaff.FirstName=objStaff.FirstName;
                DBStaff.LastName=objStaff.LastName;
                DBStaff.UserName=objStaff.UserName;
                DBStaff.Password=objStaff.Password;
                DBStaff.code=objStaff.Code;
                if (objStaff.Id.Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Staffs.InsertOnSubmit(DBStaff);
                }
                db.SubmitChanges();
                result= true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clMyAccount, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
