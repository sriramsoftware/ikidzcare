using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
   public class clSchedular
    {
        #region "Connection"
        public static void DoSchedularConnection(string strConnectionString)
        {
            string strConnectionstring = strConnectionString;
            DayCareDAL.Properties.Settings.Default["daycareConnectionString"] = strConnectionstring;
            DayCareDAL.Properties.Settings.Default.Save();
        }
        #endregion

        #region "LoadStaffDetailsByUserNameAndPassword, Dt: 4-Aug-2011, DB: A"
        public static DayCarePL.StaffProperties LoadStaffDetailsByUserNameAndPasswordForSchedule(string UserName, string Password, Guid SchoolId, string strConnectionString)
        {
            //DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", "Execute LoadStaffDetailsByUserNameAndPassword Method", DayCarePL.Common.GUID_DEFAULT);
            DayCareDAL.clSchedular.DoSchedularConnection(strConnectionString);
            DayCareDataContext db = new DayCareDataContext();
            DayCarePL.StaffProperties objStaff = null;
            try
            {
                //DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", "Debug LoadStaffDetailsByUserNameAndPassword Method", DayCarePL.Common.GUID_DEFAULT);
                objStaff = (from s in db.Staffs
                            join ssy in db.StaffSchoolYears on s.Id equals ssy.StaffId
                            join ug in db.UserGroups on s.UserGroupId equals ug.Id
                            where s.UserName.Equals(UserName) && s.Password.Equals(Password) && ug.SchoolId.Equals(SchoolId) && ssy.active.Value.Equals(true)
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
                                SchoolId = ug.SchoolId,
                                UserGroupTitle = ug.GroupTitle,
                                RolId = ug.RoleId
                            }).SingleOrDefault();

                if (objStaff != null)
                {
                    var currentyearid = (from ssy in db.StaffSchoolYears
                                         join sy in db.SchoolYears on ssy.SchoolYearId equals sy.Id
                                         where ssy.StaffId.Equals(objStaff.Id) && sy.CurrentId.Equals(true)
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
                // DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clStaff, "LoadStaffDetailsByUserNameAndPassword", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Get Child Prog. Enrollment Fee Detail ,Dt:19-09-2011 ,Db: A"
        public static List<DayCarePL.ChildProgEnrollmentProperties> GetChildProgEnrollmentFeeDetail(Guid SchoolYearId, Guid ChildFamilyId)
        {

           //DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", "GetChildProgEnrollmentFeeDetail method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
               //DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", "Debug GetChildProgEnrollmentFeeDetail called", DayCarePL.Common.GUID_DEFAULT);
                if (ChildFamilyId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    var data = (from cpefd in db.ChildProgEnrollmentFeeDetails
                                join fp in db.FeesPeriods on cpefd.FeesPeriodId equals fp.Id
                                join csy in db.ChildSchoolYears on cpefd.ChildSchoolYearId equals csy.Id
                                join sp in db.SchoolPrograms on cpefd.SchoolProgramId equals sp.Id
                                where csy.SchoolYearId.Equals(SchoolYearId) //&& cpefd.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase)
                                select new DayCarePL.ChildProgEnrollmentProperties()
                                {
                                    Id = cpefd.Id,
                                    ChildFamilyId = cpefd.ChildFamilyId.Value,
                                    ChildSchoolYearId = cpefd.ChildSchoolYearId.Value,
                                    SchoolProgramId = cpefd.SchoolProgramId.Value,
                                    ProgramTitle = sp.Title,
                                    ChildDataId = csy.ChildDataId,
                                    FeesPeriodId = cpefd.FeesPeriodId,
                                    Fees = cpefd.Fee,
                                    FeesPeriodName = fp.Name,
                                    EffectiveMonthDay = cpefd.EffectiveMonthDay,
                                    EffectiveWeekDay = cpefd.EffectiveWeekDay,
                                    EffectiveYearDate = cpefd.EffectiveYearDate,
                                    EnrollmentDate = cpefd.EnrollmentDate,
                                    EnrollmentStatus = cpefd.EnrollmentStatus,
                                    CreateDateTime = cpefd.CreatedDatetime.Value,
                                    CreatedById = cpefd.CreatedById.Value,
                                    LastModifiedById = cpefd.LastmodiedById.Value,
                                    LastModifiedDateTime = cpefd.LastmodifiedDatetime.Value,
                                    StartDate = cpefd.StartDate,
                                    EndDate = cpefd.EndDate
                                }).ToList();
                    return data.FindAll(i => i.EnrollmentStatus != null && i.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    var data = (from cpefd in db.ChildProgEnrollmentFeeDetails
                                join fp in db.FeesPeriods on cpefd.FeesPeriodId equals fp.Id
                                join csy in db.ChildSchoolYears on cpefd.ChildSchoolYearId equals csy.Id
                                join sp in db.SchoolPrograms on cpefd.SchoolProgramId equals sp.Id
                                where csy.SchoolYearId.Equals(SchoolYearId) //&& cpefd.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase)
                                && cpefd.ChildFamilyId.Equals(ChildFamilyId)
                                select new DayCarePL.ChildProgEnrollmentProperties()
                                {
                                    Id = cpefd.Id,
                                    ChildFamilyId = cpefd.ChildFamilyId.Value,
                                    ChildSchoolYearId = cpefd.ChildSchoolYearId.Value,
                                    SchoolProgramId = cpefd.SchoolProgramId.Value,
                                    ProgramTitle = sp.Title,
                                    ChildDataId = csy.ChildDataId,
                                    FeesPeriodId = cpefd.FeesPeriodId,
                                    Fees = cpefd.Fee,
                                    FeesPeriodName = fp.Name,
                                    EffectiveMonthDay = cpefd.EffectiveMonthDay,
                                    EffectiveWeekDay = cpefd.EffectiveWeekDay,
                                    EffectiveYearDate = cpefd.EffectiveYearDate,
                                    EnrollmentDate = cpefd.EnrollmentDate,
                                    EnrollmentStatus = cpefd.EnrollmentStatus,
                                    CreateDateTime = cpefd.CreatedDatetime.Value,
                                    CreatedById = cpefd.CreatedById.Value,
                                    LastModifiedById = cpefd.LastmodiedById.Value,
                                    LastModifiedDateTime = cpefd.LastmodifiedDatetime.Value,
                                    StartDate = cpefd.StartDate,
                                    EndDate = cpefd.EndDate
                                }).ToList();
                    return data.FindAll(i => i.EnrollmentStatus != null && i.EnrollmentStatus.Equals(DayCarePL.Common.DEFAULT_ENROLLMENT_STATUS, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch (Exception ex)
            {
               //DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetChildProgEnrollmentFeeDetail", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region Get Last Transaction Date, Dt: 19-Sept-2011, DB: A"
        public static System.Nullable<DateTime> GetLastTransactionDate(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, Guid? SchoolProgramId)
        {
            //DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", "GetLastTransactionDate method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
               // DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", "Debug GetLastTransactionDate called", DayCarePL.Common.GUID_DEFAULT);
                DateTime date = (from l in db.Ledgers
                                 where l.SchoolYearId.Equals(SchoolYearId) && l.ChildFamilyId.Equals(ChildFamilyId)
                                 && l.ChildDataId.Equals(ChildDataId) && l.SchoolProgramId.Equals(SchoolProgramId)
                                 orderby l.TransactionDate descending
                                 select l.TransactionDate).FirstOrDefault();
                return date;
            }
            catch (Exception ex)
            {
               // DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "GetLastTransactionDate", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save ,Dt:19-09-2011 ,Db: A"
        public static bool Save(List<DayCarePL.LedgerProperties> lstLedger)
        {
           // DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "Save", "Save method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            Ledger DBLedger = null;
            try
            {
                ///DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "Save", "Debug Save called", DayCarePL.Common.GUID_DEFAULT);
                foreach (DayCarePL.LedgerProperties objLedger in lstLedger)
                {
                    try
                    {
                        if (!CheckChildProgInLedger(objLedger.SchoolYearId, objLedger.ChildFamilyId, objLedger.ChildDataId, objLedger.TransactionDate, objLedger.SchoolProgramId))
                        {
                            DBLedger = new Ledger();
                            DBLedger.Id = Guid.NewGuid();
                            DBLedger.SchoolYearId = objLedger.SchoolYearId;
                            DBLedger.ChildFamilyId = objLedger.ChildFamilyId;
                            DBLedger.ChildDataId = objLedger.ChildDataId;
                            DBLedger.TransactionDate = objLedger.TransactionDate;
                            DBLedger.Comment = objLedger.Comment;
                            DBLedger.Detail = objLedger.Detail;
                            DBLedger.Debit = objLedger.Debit;
                            DBLedger.Credit = objLedger.Credit;
                            DBLedger.Balance = objLedger.Debit - objLedger.Credit;
                            DBLedger.AllowEdit = objLedger.AllowEdit;
                            DBLedger.PaymentId = objLedger.PaymentId;
                            DBLedger.CreatedById = objLedger.CreatedById.HasValue == true ? objLedger.CreatedById.Value : new Guid(DayCarePL.Common.GUID_DEFAULT);
                            DBLedger.CreatedDateTime = objLedger.CreatedDateTime;
                            DBLedger.LastModifiedById = objLedger.LastModifiedById;
                            DBLedger.LastModifiedDatetime = objLedger.LastModifiedDatetime;
                            DBLedger.SchoolProgramId = objLedger.SchoolProgramId;
                            db.Ledgers.InsertOnSubmit(DBLedger);
                            db.SubmitChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Check Child Prog In Ledger ,Dt:19-09-2011 ,Db: A"
        public static bool CheckChildProgInLedger(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, DateTime TransactionDate, Guid? SchoolProgramId)
        {
           // DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", "CheckChildProgInLedger method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = new DayCareDataContext();
            try
            {
               // DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", "Debug CheckChildProgInLedger called", DayCarePL.Common.GUID_DEFAULT);
                int count = 0;
                count = (from l in db.Ledgers
                         where l.SchoolYearId.Equals(SchoolYearId) && l.ChildFamilyId.Equals(ChildFamilyId)
                         && l.ChildDataId.Equals(ChildDataId) && l.TransactionDate.Date.Equals(TransactionDate.Date) && l.SchoolProgramId.Equals(SchoolProgramId)
                         select l).Count();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "CheckChildProgInLedger", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion
    }
}
