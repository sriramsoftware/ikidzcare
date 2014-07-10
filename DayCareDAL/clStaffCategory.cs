using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clStaffCategory
    {
        #region "Save Record into staffCategory, Dt: 2-Aug-2011, Db:V"
        public static bool Save(DayCarePL.StaffCategoryProperties objStaffCat)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffCategory, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            StaffCategory DBstaffCategory = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffCategory, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objStaffCat.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBstaffCategory = new StaffCategory();
                    DBstaffCategory.Id = System.Guid.NewGuid();

                }
                else
                {
                    DBstaffCategory = db.StaffCategories.SingleOrDefault(S => S.Id.Equals(objStaffCat.Id));                  
                }
                DBstaffCategory.LastModifiedDatetime = DateTime.Now;
                DBstaffCategory.LastModifiedById = objStaffCat.Id;
                DBstaffCategory.Name = objStaffCat.Name;
                DBstaffCategory.Comments = objStaffCat.Comments;
                DBstaffCategory.SchoolId = objStaffCat.SchoolId;
                DBstaffCategory.Active = objStaffCat.Active;
                DBstaffCategory.LastModifiedById = objStaffCat.LastModifiedById;
                DBstaffCategory.LastModifiedDatetime = DateTime.Now;
                if (objStaffCat.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.StaffCategories.InsertOnSubmit(DBstaffCategory);
                }
                db.SubmitChanges();
                result = true;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffCategory, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

            }
            return result;

        }
        #endregion

        #region "Load staffCategory into, Dt:2-Aug-2011, Db:V"
        public static List<DayCarePL.StaffCategoryProperties> loadStaffCategory(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffCategory, "loadStaffCategory", "Execute loadStaffCategory Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffCategory, "loadStaffCategory", "Debug loadStaffCategory Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from S in db.StaffCategories
                            where S.SchoolId.Equals(SchoolId) && !S.Name.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR)
                            orderby S.Name ascending
                            select new DayCarePL.StaffCategoryProperties()
                            {
                                Id = S.Id,
                                Name = S.Name,
                                Comments = S.Comments,
                                Active = S.Active,
                                SchoolId = S.SchoolId

                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffCategory, "LoadFeesPeriod", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Chech Duplicate Staff Category Name, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateStaffCategoryName(string StaffCategoryName, Guid StaffCategoryId, Guid? SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaffCategory, "CheckDuplicateStaffCategoryTitle", "Execute CheckDuplicateStaffCategoryTitle Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaffCategory, "CheckDuplicateStaffCategoryTitle", "Debug CheckDuplicateStaffCategoryTitle Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (StaffCategoryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.StaffCategories
                             where s.Name.Equals(StaffCategoryName) //&& ug.Id.Equals(UserGroupId)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.StaffCategories
                             where s.Name.Equals(StaffCategoryName) //&& ug.Id.Equals(UserGroupId) 
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(StaffCategoryId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaffCategory, "CheckDuplicateStaffCategoryTitle", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
