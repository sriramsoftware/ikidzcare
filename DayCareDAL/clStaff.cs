using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DayCareDAL
{
    public class clStaff
    {
        #region "Load Staff, Dt: 2-Aug-2011, DB: A"
        public static List<DayCarePL.StaffProperties> LoadStaff(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "LoadStaff", "Execute LoadStaff Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "LoadStaff", "Debug LoadStaff Method", DayCarePL.Common.GUID_DEFAULT);

                var StaffData = (from s in db.Staffs
                                 join ug in db.UserGroups on s.UserGroupId equals ug.Id
                                 join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                                 where ug.SchoolId.Equals(SchoolId) && ssy.SchoolYearId.Equals(SchoolYearId)
                                 orderby s.FirstName
                                 select new DayCarePL.StaffProperties()
                                 {
                                     Id = s.Id,
                                     UserGroupId = s.UserGroupId,
                                     StaffCategoryId = s.StaffCategoryId,
                                     FirstName = s.FirstName,
                                     LastName = s.LastName,
                                     FullName = s.FirstName + " " + s.LastName,
                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     City = s.City,
                                     Zip = s.Zip,
                                     StateId = s.StateId,
                                     CountryId = s.CountryId,
                                     MainPhone = s.MainPhone,
                                     SecondaryPhone = s.SecondaryPhone,
                                     Email = s.Email,
                                     UserName = s.UserName,
                                     Password = s.Password,
                                     Code = s.code,
                                     Gender = s.gender,
                                     SecurityQuestion = s.SecurityAnswer,
                                     SecurityAnswer = s.SecurityAnswer,
                                     Photo = s.Photo,
                                     Active = ssy.active.Value,
                                     Comments = s.Comments,
                                     Message = s.Message,
                                     //IsPrimary = s.IsPrimary,
                                     CreatedById = s.CreatedById,
                                     CreatedDateTime = s.CreatedDateTime,
                                     LastModifiedById = s.LastModifiedById,
                                     LastModifiedDatetime = s.LastModifiedDatetime
                                 }).ToList();

                List<DayCarePL.StaffProperties> lstStaff = new List<DayCarePL.StaffProperties>();

                foreach (DayCarePL.StaffProperties objStaff in StaffData)
                {
                    var UserGroupTitle = from ugt in db.UserGroups
                                         where ugt.Id.Equals(objStaff.UserGroupId)
                                         select new
                                         {
                                             Title = ugt.GroupTitle
                                         };
                    objStaff.UserGroupTitle = UserGroupTitle.Select(ugt => ugt.Title).SingleOrDefault();

                    var StaffCategoryName = from scn in db.StaffCategories
                                            where scn.Id.Equals(objStaff.StaffCategoryId)
                                            select new
                                            {
                                                CategoryName = scn.Name
                                            };
                    objStaff.StaffCategoryName = StaffCategoryName.Select(scn => scn.CategoryName).SingleOrDefault();

                    var CountryName = from c in db.Countries
                                      where c.Id.Equals(objStaff.CountryId)
                                      select new
                                      {
                                          Name = c.Name
                                      };
                    objStaff.CountryName = CountryName.Select(cn => cn.Name).SingleOrDefault();

                    var StateName = from s in db.States
                                    where s.Id.Equals(objStaff.StateId)
                                    select new
                                    {
                                        Name = s.Name
                                    };
                    objStaff.StateName = StateName.Select(s => s.Name).SingleOrDefault();
                    if (ISStaffinCurrentYear(objStaff.Id, SchoolId, SchoolYearId))
                    {
                        lstStaff.Add(objStaff);
                    }
                }
                return lstStaff;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadStaff", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }

        public static DayCarePL.StaffProperties LoadStaffBystaffId(Guid StaffId, Guid CurrentSchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "LoadStaffBystaffId", "Execute LoadStaffBystaffId Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.StaffProperties objStaff = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "LoadStaffBystaffId", "Debug LoadStaffBystaffId Method", DayCarePL.Common.GUID_DEFAULT);
                var StaffData = (from s in db.Staffs
                                 join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                                 join ug in db.UserGroups on s.UserGroupId equals ug.Id into usergrooup
                                 join sg in db.StaffCategories on s.StaffCategoryId equals sg.Id into staffcategory
                                 join c in db.Countries on s.CountryId equals c.Id into country
                                 join st in db.States on s.StateId equals st.Id into states
                                 from usrgrp in usergrooup.DefaultIfEmpty()
                                 from stcag in staffcategory.DefaultIfEmpty()
                                 from ctry in country.DefaultIfEmpty()
                                 from state in states.DefaultIfEmpty()
                                 where s.Id.Equals(StaffId) && ssy.SchoolYearId.Equals(CurrentSchoolYearId)
                                 select new DayCarePL.StaffProperties()
                                 {
                                     Id = s.Id,
                                     UserGroupId = s.UserGroupId,
                                     StaffCategoryId = s.StaffCategoryId,
                                     FirstName = s.FirstName,
                                     LastName = s.LastName,
                                     FullName = s.FirstName + " " + s.LastName,
                                     Address1 = s.Address1,
                                     Address2 = s.Address2,
                                     City = s.City,
                                     Zip = s.Zip,
                                     StateId = s.StateId,
                                     CountryId = s.CountryId,
                                     MainPhone = s.MainPhone,
                                     SecondaryPhone = s.SecondaryPhone,
                                     Email = s.Email,
                                     UserName = s.UserName,
                                     Password = s.Password,
                                     Code = s.code,
                                     Gender = s.gender,
                                     SecurityQuestion = s.SecurityQuestion,
                                     SecurityAnswer = s.SecurityAnswer,
                                     Photo = s.Photo,
                                     Active = ssy.active.Value,
                                     Comments = s.Comments,
                                     Message = s.Message,
                                     //IsPrimary = s.IsPrimary,
                                     CreatedById = s.CreatedById,
                                     CreatedDateTime = s.CreatedDateTime,
                                     LastModifiedById = s.LastModifiedById,
                                     LastModifiedDatetime = s.LastModifiedDatetime,
                                     UserGroupTitle = usrgrp.GroupTitle,
                                     StaffCategoryName = stcag.Name,
                                     CountryName = ctry.Name,
                                     StateName = state.Name
                                 }).Single();

                objStaff = StaffData;

                //if (objStaff != null)
                //{
                //    var UserGroupTitle = from ugt in db.UserGroups
                //                         where ugt.Id.Equals(objStaff.UserGroupId)
                //                         select new
                //                         {
                //                             Title = ugt.GroupTitle
                //                         };
                //    objStaff.UserGroupTitle = UserGroupTitle.Select(ugt => ugt.Title).SingleOrDefault();

                //    var StaffCategoryName = from scn in db.StaffCategories
                //                            where scn.Id.Equals(objStaff.StaffCategoryId)
                //                            select new
                //                            {
                //                                CategoryName = scn.Name
                //                            };
                //    objStaff.StaffCategoryName = StaffCategoryName.Select(scn => scn.CategoryName).SingleOrDefault();

                //    var CountryName = from c in db.Countries
                //                      where c.Id.Equals(objStaff.CountryId)
                //                      select new
                //                      {
                //                          Name = c.Name
                //                      };
                //    objStaff.CountryName = CountryName.Select(cn => cn.Name).SingleOrDefault();

                //    var StateName = from s in db.States
                //                    where s.Id.Equals(objStaff.StateId)
                //                    select new
                //                    {
                //                        Name = s.Name
                //                    };
                //    objStaff.StateName = StateName.Select(s => s.Name).SingleOrDefault();

                //}
                return objStaff;

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadStaffBystaffId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }

        #endregion

        #region "LoadStaffDetailsByUserNameAndPassword, Dt: 4-Aug-2011, DB: A"
        public static DayCarePL.StaffProperties LoadStaffDetailsByUserNameAndPassword(string UserName, string Password, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", "Execute LoadStaffDetailsByUserNameAndPassword Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.StaffProperties objStaff = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", "Debug LoadStaffDetailsByUserNameAndPassword Method", DayCarePL.Common.GUID_DEFAULT);


                objStaff = (from s in db.Staffs
                            //join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                            join ug in db.UserGroups on s.UserGroupId equals ug.Id
                            join sc in db.Schools on ug.SchoolId equals sc.Id
                            where s.UserName.Equals(UserName) && s.Password.Equals(Password) && ug.SchoolId.Equals(SchoolId)
                            select new DayCarePL.StaffProperties()
                            {
                                Id = s.Id,
                                UserGroupId = s.UserGroupId,
                                StaffCategoryId = s.StaffCategoryId,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                FullName = s.FirstName + " " + s.LastName,
                                Address1 = s.Address1,
                                Address2 = s.Address2,
                                City = s.City,
                                Zip = s.Zip,
                                StateId = s.StateId,
                                CountryId = s.CountryId,
                                MainPhone = s.MainPhone,
                                SecondaryPhone = s.SecondaryPhone,
                                Email = s.Email,
                                UserName = s.UserName,
                                Password = s.Password,
                                Code = s.code,
                                Gender = s.gender,
                                SecurityQuestion = s.SecurityQuestion,
                                SecurityAnswer = s.SecurityAnswer,
                                Photo = s.Photo,
                                //Active = ssy.active.Value,
                                Comments = s.Comments,
                                Message = s.Message,
                                //IsPrimary = s.IsPrimary,
                                CreatedById = s.CreatedById,
                                CreatedDateTime = s.CreatedDateTime,
                                LastModifiedById = s.LastModifiedById,
                                LastModifiedDatetime = s.LastModifiedDatetime,
                                SchoolId = ug.SchoolId,
                                UserGroupTitle = ug.GroupTitle,
                                SchoolName = sc.Name,
                                RolId = ug.RoleId
                            }).SingleOrDefault();

                if (objStaff != null)
                {
                    var currentyearid = (from ssy in db.StaffSchoolYears
                                         join sy in db.SchoolYears on ssy.SchoolYearId equals sy.Id
                                         where ssy.StaffId.Equals(objStaff.Id) && sy.CurrentId.Equals(true) && ssy.active.Equals(true)
                                         select new
                                         {
                                             id = sy.Id
                                         });

                    objStaff.ScoolYearId = currentyearid.Select(id => id.id).SingleOrDefault();
                    if (objStaff.ScoolYearId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        objStaff = null;
                    }

                }
                return objStaff;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 2-Aug-2011, DB: A"
        public static Guid Save(DayCarePL.StaffProperties objStaff)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();

            DayCareDataContext db = new DayCareDataContext();
            System.Data.Common.DbTransaction tran = null;
            Staff DBStaff = null;
            Guid StaffId;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                if (db.Connection.State == System.Data.ConnectionState.Closed)
                {
                    db.Connection.Open();
                }
                tran = db.Connection.BeginTransaction();
                db.Transaction = tran;
                if (objStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBStaff = new Staff();
                    DBStaff.Id = System.Guid.NewGuid();
                    DBStaff.CreatedById = objStaff.CreatedById;
                    DBStaff.CreatedDateTime = DateTime.Now;
                }
                else
                {
                    DBStaff = db.Staffs.SingleOrDefault(u => u.Id.Equals(objStaff.Id));
                }
                DBStaff.LastModifiedById = objStaff.LastModifiedById;
                DBStaff.LastModifiedDatetime = DateTime.Now;
                DBStaff.UserGroupId = objStaff.UserGroupId;
                DBStaff.StaffCategoryId = objStaff.StaffCategoryId;
                DBStaff.FirstName = objStaff.FirstName;
                DBStaff.LastName = objStaff.LastName;
                DBStaff.Address1 = objStaff.Address1;
                DBStaff.Address2 = objStaff.Address2;
                DBStaff.City = objStaff.City;
                DBStaff.Zip = objStaff.Zip;
                if (!objStaff.CountryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBStaff.CountryId = objStaff.CountryId;
                }
                if (!objStaff.StateId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBStaff.StateId = objStaff.StateId;
                }
                DBStaff.MainPhone = objStaff.MainPhone;
                DBStaff.SecondaryPhone = objStaff.SecondaryPhone;
                DBStaff.Email = objStaff.Email;
                DBStaff.UserName = objStaff.UserName;
                DBStaff.Password = objStaff.Password;
                DBStaff.code = objStaff.Code;
                DBStaff.gender = objStaff.Gender;
                DBStaff.SecurityQuestion = objStaff.SecurityQuestion;
                DBStaff.SecurityAnswer = objStaff.SecurityAnswer;
                if (!string.IsNullOrEmpty(objStaff.Photo))
                {
                    DBStaff.Photo = DBStaff.Id + Path.GetExtension(objStaff.Photo);
                }
                else
                {
                    DBStaff.Photo = string.Empty;
                }
                //DBStaff.Active = objStaff.Active;
                DBStaff.Comments = objStaff.Comments;
                DBStaff.Message = objStaff.Message;
                //DBStaff.IsPrimary = objStaff.IsPrimary;
                if (objStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Staffs.InsertOnSubmit(DBStaff);
                }
                db.SubmitChanges();

                if (!objStaff.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    StaffSchoolYear DBStaffSchoolYear = db.StaffSchoolYears.SingleOrDefault(u => u.StaffId.Equals(objStaff.Id) && u.SchoolYearId.Equals(objStaff.ScoolYearId));
                    DBStaffSchoolYear.StaffId = objStaff.Id;
                    DBStaffSchoolYear.SchoolYearId = objStaff.ScoolYearId;
                    DBStaffSchoolYear.active = objStaff.Active;
                    db.SubmitChanges();
                }
                if (objStaff.Active == true)
                {
                    if (DayCareDAL.clStaffSchoolYear.ExportStafftoStaffSchoolYear(DBStaff.Id, objStaff.SchoolId, objStaff.ScoolYearId, objStaff.Active, tran, db))
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Commit();
                }
                StaffId = DBStaff.Id;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                StaffId = new Guid();
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            finally
            {
                if (db.Connection.State == System.Data.ConnectionState.Open)
                {
                    db.Connection.Close();
                }
            }
            return StaffId;
        }
        #endregion

        #region Chech Duplicate User Name, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateUserName(string UserName, Guid StaffId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateUserName", "Execute CheckDuplicateUserName Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateUserName", "Debug CheckDuplicateUserName Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (StaffId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.Staffs
                             join ug in db.UserGroups on s.UserGroupId equals ug.Id
                             //join sc in db.Schools on ug.SchoolId equals sc.Id
                             where s.UserName.Equals(UserName) //&& ug.Id.Equals(UserGroupId)
                             && ug.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.Staffs
                             join ug in db.UserGroups on s.UserGroupId equals ug.Id
                             join sc in db.Schools on ug.SchoolId equals sc.Id
                             where s.UserName.Equals(UserName) //&& ug.Id.Equals(UserGroupId) 
                             && ug.SchoolId.Equals(SchoolId) && !s.Id.Equals(StaffId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateUserName", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region Chech Duplicate Code, Dt: 2-Aug-2011, DB: A"
        public static bool CheckDuplicateCode(string Code, Guid StaffId, Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateCode", "Execute CheckDuplicateCode Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateCode", "Debug CheckDuplicateCode Method", DayCarePL.Common.GUID_DEFAULT);
                int count;
                //var SchoolId = from ug in db.UserGroups
                //               where ug.Id.Equals(UserGroupId)
                //               select new
                //               {
                //                   id = ug.SchoolId
                //               };
                if (StaffId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    count = (from s in db.Staffs
                             join ug in db.UserGroups on s.UserGroupId equals ug.Id
                             //join sc in db.Schools on ug.SchoolId equals sc.Id
                             where s.code.Equals(Code) //&& ug.Id.Equals(UserGroupId)
                             && ug.SchoolId.Equals(SchoolId)
                             select s).Count();
                }
                else
                {
                    count = (from s in db.Staffs
                             join ug in db.UserGroups on s.UserGroupId equals ug.Id
                             join sc in db.Schools on ug.SchoolId equals sc.Id
                             where s.code.Equals(Code) //&& ug.Id.Equals(UserGroupId) 
                             && ug.SchoolId.Equals(SchoolId) && !s.Id.Equals(StaffId)
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "CheckDuplicateCode", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Check Code Require, Dt:2-Aug-2011, DB: A"
        public static bool CheckCodeRequire(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "CheckCodeRequire", "Execute CheckCodeRequire Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result = false;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "CheckCodeRequire", "Debug CheckCodeRequire Method", DayCarePL.Common.GUID_DEFAULT);

                int count = 0;
                count = (from s in db.Schools
                         where s.Id.Equals(SchoolId) &&
                         s.CodeRequired.Equals(true)
                         select s).Count();
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
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "CheckCodeRequire", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "IS Staff in Current Year, Dt: 5-Aug-2011, DB: A"
        public static bool ISStaffinCurrentYear(Guid StaffId, Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", "Execute LoadStaffDetailsByUserNameAndPassword Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            bool result;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "ISStaffinCurrentYear", "ISStaffinCurrentYear function debug", DayCarePL.Common.GUID_DEFAULT);

                //string CurrentSchoolYearId = (from csy in db.SchoolYears
                //                              where csy.SchoolId.Equals(SchoolId)
                //                              && csy.CurrentId.Equals(true)
                //                              select csy.Id == null ? "" : csy.Id.ToString()).FirstOrDefault();
                string StaffSchoolYearId = "";
                //if (CurrentSchoolYearId.Length > 0)
                //{
                StaffSchoolYearId = (from ssy in db.StaffSchoolYears
                                     where ssy.SchoolYearId.ToString().Equals(SchoolYearId)
                                     && ssy.StaffId.Equals(StaffId)
                                     select ssy.Id.ToString()).FirstOrDefault();
                //}

                if (StaffSchoolYearId != null && StaffSchoolYearId.Length > 1)
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
                result = false;
            }
            return result;
        }
        #endregion

        #region "JSON: Validate User from iPad; only "Admin Role" users able to sign in iPad App, Dt: 5-8-2011, DB: P"
        public static DayCarePL.iLoginStaffProperties ValidateUser(string UserName, string Password, Guid SchoolId)
        {
            clConnection.DoConnection();
            DayCarePL.iLoginStaffProperties StaffData = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "ValidateUser", "ValidateUser function debug", DayCarePL.Common.GUID_DEFAULT);
                DayCareDataContext db = new DayCareDataContext();
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                //get Staff details using match user name and password
                StaffData = (from s in db.Staffs
                             join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                             where ssy.active.Equals(true)
                             join ug in db.UserGroups on s.UserGroupId equals ug.Id
                             join r in db.Roles on ug.RoleId equals r.Id
                             where s.UserName.Equals(UserName.Trim())
                             && s.Password.Equals(Password)
                             && ug.SchoolId.Equals(SchoolId)
                             select new DayCarePL.iLoginStaffProperties()
                           {
                               Id = s.Id,
                               StaffFullName = s.FirstName + " " + s.LastName,
                               FirstName = s.FirstName,
                               LastName = s.LastName,
                               UserGroupId = s.UserGroupId,
                               UserGroupTitle = ug.GroupTitle,
                               Active = ssy.active.Value,
                               RoleId = Convert.ToString(r.Id),
                               RoleOfUser = r.Name
                           }
                                                             ).FirstOrDefault();

                if (StaffData != null)
                {
                    string CurrentSchoolYearId = (from csy in db.SchoolYears
                                                  where csy.SchoolId.Equals(SchoolId)
                                                  && csy.CurrentId.Equals(true)
                                                  select csy.Id == null ? "" : csy.Id.ToString()).FirstOrDefault();

                    string StaffSchoolYearId = "";
                    if (CurrentSchoolYearId.Length > 0)
                    {
                        StaffSchoolYearId = (from ssy in db.StaffSchoolYears
                                             where ssy.SchoolYearId.ToString().Equals(CurrentSchoolYearId)
                                             && ssy.StaffId.Equals(StaffData.Id)
                                             select ssy.Id.ToString()).FirstOrDefault();

                        StaffData.CurrentSchoolYearId = CurrentSchoolYearId;
                        StaffData.StaffSchoolYearId = StaffSchoolYearId;
                    }

                    if (StaffSchoolYearId.Length > 1)
                    {
                        DayCarePL.SchoolProperties schoolData = (from s in db.Schools
                                                                 where s.Id.Equals(SchoolId) && s.active.Equals(true)
                                                                 select new DayCarePL.SchoolProperties()
                                                                 {
                                                                     Name = s.Name,
                                                                     iPadBackgroundImage = s.iPadBackgroundImage == null ? "" : myConfiguration.AppSettings.Settings["StaffPhoto"].Value + "/StaffImages/" + s.iPadBackgroundImage,
                                                                     iPadHeader = s.iPadHeader,
                                                                     iPadHeaderColor = s.iPadHeaderColor,
                                                                     iPadHeaderFont = s.iPadHeaderFont,
                                                                     iPadHeaderFontSize = s.iPadHeaderFontSize,
                                                                     iPadMessage = s.iPadMessage,
                                                                     iPadMessageColor = s.iPadMessageColor,
                                                                     iPadMessageFont = s.iPadMessageFont,
                                                                     iPadMessageFontSize = s.iPadMessageFontSize,
                                                                     LastModifiedDatetime = s.LastModifiedDatetime,
                                                                     CodeRequired = s.CodeRequired
                                                                 }).FirstOrDefault();

                        StaffData.SchoolName = schoolData.Name;
                        StaffData.iPadBackgroundImage = schoolData.iPadBackgroundImage;
                        StaffData.iPadHeader = schoolData.iPadHeader;
                        StaffData.iPadHeaderColor = schoolData.iPadHeaderColor;
                        StaffData.iPadHeaderFont = schoolData.iPadHeaderFont;
                        StaffData.iPadHeaderFontSize = schoolData.iPadHeaderFontSize == null ? 0 : schoolData.iPadHeaderFontSize.Value;
                        StaffData.iPadMessage = schoolData.iPadMessage;
                        StaffData.iPadMessageColor = schoolData.iPadMessageColor;
                        StaffData.iPadMessageFont = schoolData.iPadMessageFont;
                        StaffData.CodeRequired = schoolData.CodeRequired.ToString();
                        StaffData.SchoolModifiedDateTime = string.Format("{0:M/d/yyyy hh:mm tt}", schoolData.LastModifiedDatetime);
                        StaffData.Validate = "true";
                    }
                    else
                    {
                        StaffData.Validate = "false";
                    }
                }
                else
                {
                    StaffData = new DayCarePL.iLoginStaffProperties();
                    StaffData.Validate = "false";
                }

            }
            catch (Exception ex)
            {
                StaffData = null;
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "ValidateUser", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return StaffData;
        }
        #endregion

        #region "JSON: List Of Staff who are active and member of current shool year, DB: P"
        public static List<DayCarePL.iStaffDetailProperties> LoadListOfStaff(Guid SchoolYearId)
        {
            clConnection.DoConnection();
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            List<DayCarePL.iStaffDetailProperties> staffData = new List<DayCarePL.iStaffDetailProperties>();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                bool isCurrentSchoolYear = (from sy in db.SchoolYears
                                            where sy.Id.Equals(SchoolYearId)
                                            //&& sy.CurrentId.Equals(true)
                                            select sy.Id == null ? false : true).SingleOrDefault();

                if (isCurrentSchoolYear)
                {
                    Guid[] StaffIds = (from ssy in db.StaffSchoolYears
                                       where ssy.SchoolYearId.Equals(SchoolYearId)
                                       select ssy.StaffId).ToArray();

                    foreach (Guid StaffId in StaffIds)
                    {
                        DayCarePL.iStaffDetailProperties data = (from s in db.Staffs
                                                                 join ssy1 in db.StaffSchoolYears on s.Id equals ssy1.StaffId
                                                                 where s.Id.Equals(StaffId) && !s.code.Equals("0000") && ssy1.SchoolYearId.Equals(SchoolYearId)
                                                                 join ug in db.UserGroups on s.UserGroupId equals ug.Id
                                                                 join r in db.Roles on ug.RoleId equals r.Id
                                                                 select new DayCarePL.iStaffDetailProperties()
                                                                 {
                                                                     Id = s.Id,
                                                                     Code = s.code,
                                                                     FirstName = s.FirstName,
                                                                     LastName = s.LastName,
                                                                     Gender = s.gender == true ? "Male" : "Female",
                                                                     StaffPhoto = string.IsNullOrEmpty(s.Photo) == true ? "" : myConfiguration.AppSettings.Settings["StaffPhoto"].Value + "/StaffImages/" + s.Photo,
                                                                     StaffSchoolYearId = (from ssy in db.StaffSchoolYears
                                                                                          where ssy.StaffId.Equals(s.Id) && ssy.SchoolYearId.Equals(SchoolYearId)
                                                                                          select ssy.Id).FirstOrDefault(),
                                                                     LastModifiedDateTime = s.LastModifiedDatetime == null ? "" : s.LastModifiedDatetime.ToString(),
                                                                     Message = s.Message,
                                                                     StaffActive = Convert.ToString(ssy1.active.Value),
                                                                     RoleId = r.Id.ToString(),
                                                                     RoleName = r.Name
                                                                 }).SingleOrDefault();
                        if (data != null)
                            staffData.Add(data);
                        data = null;
                    }
                }
                return staffData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "ValidateUser", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }


        }
        #endregion

        #region "JSON: Get Class Room List With Assigned Staff, 25-08-2011 DB:P"
        public static List<DayCarePL.iClassRoomWithStaffProperties> LoadClassRoomWithAssignedStaff(Guid SchoolId, Guid SchoolYearId)
        {
            clConnection.DoConnection();
            List<DayCarePL.iClassRoomWithStaffProperties> lstClassRoomWithAssignedStaff = new List<DayCarePL.iClassRoomWithStaffProperties>();
            DayCarePL.iClassRoomWithStaffProperties objClassRoomWithAssignedStaff = null;
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                var data = db.spJSONGetClassRoomStaff(SchoolId, SchoolYearId);

                foreach (var d in data)
                {
                    objClassRoomWithAssignedStaff = new DayCarePL.iClassRoomWithStaffProperties();
                    objClassRoomWithAssignedStaff.ClassRoomId = d.ClassRoomId;
                    objClassRoomWithAssignedStaff.ClassRoomTitile = d.ClassRoomTitle;
                    objClassRoomWithAssignedStaff.StaffId = d.StaffId.Value;
                    objClassRoomWithAssignedStaff.StaffName = d.StaffName;
                    objClassRoomWithAssignedStaff.LastModifiedDateTime = string.Format("{0:M/d/yyyy hh:mm tt}", d.LastModifiedDateTime);
                    objClassRoomWithAssignedStaff.StaffSchoolYearId = d.StaffSchoolYearId;
                    lstClassRoomWithAssignedStaff.Add(objClassRoomWithAssignedStaff);
                    objClassRoomWithAssignedStaff = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadClassRoomWithAssignedStaff", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstClassRoomWithAssignedStaff;
        }
        #endregion

        #region "JSON: Get List Of Childs By Class Room Id, Date:26-08-2011 DB: P"
        public static List<DayCarePL.iChildDataProperties> LoadChildListByClassRoomId(Guid ClassRoomId, Guid SchoolYearId)
        {
            clConnection.DoConnection();
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.iChildDataProperties> lstChildDataProperties = new List<DayCarePL.iChildDataProperties>();
            DayCarePL.iChildDataProperties objChildDataProperties = null;
            try
            {
                var data = db.spJSONChildListByClassRoomId(ClassRoomId, SchoolYearId);
                foreach (var d in data)
                {
                    objChildDataProperties = new DayCarePL.iChildDataProperties();
                    objChildDataProperties.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildDataProperties.FirstName = d.ChildFirstName;
                    objChildDataProperties.LastName = d.ChildLastName;
                    objChildDataProperties.ChildPhoto = string.IsNullOrEmpty(d.ChildPhoto) == true ? "" : myConfiguration.AppSettings.Settings["StaffPhoto"].Value + "/ChildImages/" + d.ChildPhoto;
                    objChildDataProperties.Msg = d.MsgDisplayed;
                    objChildDataProperties.MsgStartDate = string.Format("{0:M/d/yyyy hh:mm tt}", d.MsgStartDate);
                    objChildDataProperties.MsgEndDate = d.MsgEndDate == null ? "" : string.Format("{0:M/d/yyyy hh:mm tt}", d.MsgEndDate);
                    objChildDataProperties.MsgActive = d.MsgActive.Value;
                    objChildDataProperties.FamilyMsgLastModified = string.Format("{0:M/d/yyyy hh:mm tt}", d.FamilyMsgLastModified);
                    objChildDataProperties.ChildModifiedDateTime = string.Format("{0:M/d/yyyy hh:mm tt}", d.ChildModifiedDateTime);
                    objChildDataProperties.Gender = d.Gender;
                    objChildDataProperties.ChildActive = Convert.ToString(d.ChildActive);
                    lstChildDataProperties.Add(objChildDataProperties);
                    objChildDataProperties = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadChildListByClassRoomId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstChildDataProperties;
        }
        #endregion

        #region "JSON: Get List Of Active Children, DB: P"
        public static List<DayCarePL.iChildDataProperties> LoadActiveChildList(Guid SchoolYearId)
        {
            clConnection.DoConnection();
            Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.iChildDataProperties> lstChildDataProperties = new List<DayCarePL.iChildDataProperties>();
            DayCarePL.iChildDataProperties objChildDataProperties = null;
            try
            {
                var data = db.spJSONGetListOfActiveChildren(SchoolYearId);
                foreach (var d in data)
                {
                    objChildDataProperties = new DayCarePL.iChildDataProperties();
                    objChildDataProperties.ChildSchoolYearId = d.ChildSchoolYearId;
                    objChildDataProperties.FirstName = d.ChildFirstName;
                    objChildDataProperties.LastName = d.ChildLastName;
                    objChildDataProperties.ChildPhoto = string.IsNullOrEmpty(d.ChildPhoto) == true ? "" : myConfiguration.AppSettings.Settings["StaffPhoto"].Value + "/ChildImages/" + d.ChildPhoto;
                    objChildDataProperties.Msg = d.MsgDisplayed;
                    objChildDataProperties.MsgStartDate = string.Format("{0:M/d/yyyy hh:mm tt}", d.MsgStartDate);
                    objChildDataProperties.MsgEndDate = d.MsgEndDate == null ? "" : string.Format("{0:M/d/yyyy hh:mm tt}", d.MsgEndDate);
                    objChildDataProperties.MsgActive = d.MsgActive.Value;
                    objChildDataProperties.FamilyMsgLastModified = string.Format("{0:M/d/yyyy hh:mm tt}", d.FamilyMsgLastModified);
                    objChildDataProperties.ChildModifiedDateTime = string.Format("{0:M/d/yyyy hh:mm tt}", d.ChildModifiedDateTime);
                    objChildDataProperties.Gender = d.Gender;
                    objChildDataProperties.ChildActive = d.ChildActive.ToString();
                    lstChildDataProperties.Add(objChildDataProperties);
                    objChildDataProperties = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadChildListByClassRoomId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return lstChildDataProperties;
        }
        #endregion

    }
}
