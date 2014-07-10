using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DayCareDAL
{
    public class clRole
    {
        #region "Save record into Role, Dt: 30-Jul-2011, DB: P"
        public static bool Save(DayCarePL.RoleProperties objRole)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clRole, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            Role DBRole = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clRole, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objRole.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBRole = new Role();
                    DBRole.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBRole = db.Roles.SingleOrDefault(u => u.Id.Equals(objRole.Id));
                }
                DBRole.Name = objRole.Name;
                DBRole.Active = objRole.Active;
                if (objRole.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Roles.InsertOnSubmit(DBRole);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRole, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Load Roles, Dt: 30-Jul-2011, DB: P"
        public static DayCarePL.RoleProperties[] LoadRoles()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clRole, "LoadRoles", "Execute LoadRoles Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clRole, "LoadRoles", "Debug LoadRoles Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from r in db.Roles
                            orderby r.Name ascending
                            select new DayCarePL.RoleProperties()
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Active = r.Active
                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clRole, "LoadRoles", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

    
    }
}
