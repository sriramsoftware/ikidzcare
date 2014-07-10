using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clUserGroup
    {
        #region Load User Group, Dt: 2-Aug-2011, DB: A"
        public static List<DayCarePL.UserGroupProperties> LoadUserGroup(Guid SchoolId)
        {
            //following code should move to UserGroup Class==>DayCareDAL
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clUserGroup, "LoadUserGroup", "Execute LoadUserGroup Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clUserGroup, "LoadUserGroup", "Debug LoadUserGroup Method", DayCarePL.Common.GUID_DEFAULT);

                var UserGroupData = (from ug in db.UserGroups
                                     where ug.SchoolId.Equals(SchoolId) && !ug.GroupTitle.Equals(DayCarePL.Common.SCHOOL_ADMINISTRATOR)
                                     orderby ug.GroupTitle ascending
                                     select new DayCarePL.UserGroupProperties()
                                     {
                                         Id = ug.Id,
                                         GroupTitle = ug.GroupTitle,
                                         SchoolId = ug.SchoolId,
                                         Comments = ug.Comments,
                                         RoleId = ug.RoleId,
                                         LastModifiedById = ug.LastModifiedById,
                                         LastModifiedDatetime = ug.LastModifiedDatetime
                                     }).ToList();

                List<DayCarePL.UserGroupProperties> lstUserGroup = new List<DayCarePL.UserGroupProperties>();

                foreach (DayCarePL.UserGroupProperties objUserGroup in UserGroupData)
                {
                    var RolName = from r in db.Roles
                                  where r.Id.Equals(objUserGroup.RoleId)
                                  select new
                                  {
                                      Role = r.Name
                                  };
                    objUserGroup.RoleName = RolName.Select(r => r.Role).SingleOrDefault();

                    var SchoolName = from s in db.Schools
                                     where s.Id.Equals(objUserGroup.SchoolId)
                                     select new
                                     {
                                         Name = s.Name
                                     };
                    objUserGroup.SchoolName = SchoolName.Select(s => s.Name).SingleOrDefault();
                    lstUserGroup.Add(objUserGroup);
                }

                return lstUserGroup;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clUserGroup, "LoadUserGroup", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save User Group, Dt: 2-Aug-2011, Db: V"
        public static bool Save(DayCarePL.UserGroupProperties objUserGroup)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clUserGroup, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            UserGroup DBUserGroup = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clUserGroup, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (objUserGroup.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBUserGroup = new UserGroup();
                    DBUserGroup.Id = System.Guid.NewGuid();
                }
                else
                {
                    DBUserGroup = db.UserGroups.SingleOrDefault(U => U.Id.Equals(objUserGroup.Id));                  
                }
                DBUserGroup.LastModifiedById = objUserGroup.LastModifiedById;
                DBUserGroup.LastModifiedDatetime = DateTime.Now;
                DBUserGroup.SchoolId = objUserGroup.SchoolId;
                DBUserGroup.GroupTitle = objUserGroup.GroupTitle;
                DBUserGroup.RoleId = objUserGroup.RoleId;
                DBUserGroup.Comments = objUserGroup.Comments;
                DBUserGroup.LastModifiedById = objUserGroup.LastModifiedById;
                DBUserGroup.LastModifiedDatetime = DateTime.Now;
                if (objUserGroup.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.UserGroups.InsertOnSubmit(DBUserGroup);
                }
                db.SubmitChanges();
                result = true;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clUserGroup, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region Chech Duplicate User Group Title, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateUserGroupTitle(string UserGroupTitle, Guid UserGroupId, Guid? SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clUserGroup, "CheckDuplicateUserGroupTitle", "Execute CheckDuplicateUserGroupTitle Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.UserGroup, "CheckDuplicateUserGroupTitle", "Debug CheckDuplicateUserGroupTitle Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (UserGroupId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from ug in db.UserGroups
                             where ug.GroupTitle.Equals(UserGroupTitle) //&& ug.Id.Equals(UserGroupId)
                             && ug.SchoolId.Equals(SchoolId)
                             select ug).Count();
                }
                else
                {
                    count = (from ug in db.UserGroups
                             where ug.GroupTitle.Equals(UserGroupTitle)//&& ug.Id.Equals(UserGroupId) 
                             && ug.SchoolId.Equals(SchoolId) && !ug.Id.Equals(UserGroupId)
                             select ug).Count();
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.UserGroup, "CheckDuplicateUserGroupTitle", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
