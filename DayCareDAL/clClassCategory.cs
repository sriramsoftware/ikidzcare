using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clClassCategory
    {
        #region "Save ClassCategory, Dt:4-Aug-2011, Db:V"
        public static bool Save(DayCarePL.ClassCategoryProperties objClassCategory)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassCategory, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            ClassCategory DBClassCategory = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassCategory, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objClassCategory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBClassCategory = new ClassCategory();
                    DBClassCategory.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBClassCategory = db.ClassCategories.SingleOrDefault(C => C.Id.Equals(objClassCategory.Id));
                }
                DBClassCategory.LastModifiedById = objClassCategory.LastModifiedById;
                DBClassCategory.LastModifiedDatetime = DateTime.Now;
                DBClassCategory.SchoolId = objClassCategory.SchoolId;
                DBClassCategory.Name = objClassCategory.Name;
                DBClassCategory.Active = objClassCategory.Active;
                DBClassCategory.Comments = objClassCategory.Comments;
                DBClassCategory.LastModifiedById = objClassCategory.LastModifiedById;
                DBClassCategory.LastModifiedDatetime = DateTime.Now;
                if (objClassCategory.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.ClassCategories.InsertOnSubmit(DBClassCategory);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassCategory, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;

            }
            return result;
        }
        #endregion

        #region "Load ClassCategory, Dt:4-Aug-2011, Db:V"
        public static DayCarePL.ClassCategoryProperties[] LoadClassCategory(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassCategory, "LoadClassCategory", "Execute LoadClassCategory Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassCategory, "LoadClassCategory", "Debug LoadClassCategory Method", DayCarePL.Common.GUID_DEFAULT);
                var data = (from C in db.ClassCategories
                            where C.SchoolId.Equals(SchoolId)
                            orderby C.Name ascending
                            select new DayCarePL.ClassCategoryProperties()
                            {
                                Id = C.Id,
                                Name = C.Name,
                                Active = C.Active,
                                Comments = C.Comments,

                            }).ToArray();
                return data;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clClassCategory, "LoadClassCategory", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Check Duplicate ClassCategory, Dt:5-Aug-2011, Db:V"
        public static bool CheckDuplicateClassCategory(string ClassCategoryName, Guid ClassCategoryId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clClassCategory, "CheckDuplicateClassCategoryName", "Execute CheckDuplicateClassCategoryName Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clClassCategory, "CheckDuplicateClassCategoryName", "Debug CheckDuplicateClassCategoryName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (ClassCategoryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.ClassCategories
                             where s.Name.Equals(ClassCategoryName) //&& ug.Id.Equals(UserGroupId)
                             && s.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.ClassCategories
                             where s.Name.Equals(ClassCategoryName) //&& ug.Id.Equals(UserGroupId) 
                             && s.SchoolId.Equals(SchoolId) && !s.Id.Equals(ClassCategoryId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.ClassCategory, "CheckDuplicateClassCategoryName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
