using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCareDAL
{
    public class clChildProgEnrollmentFeeDetail
    {
        public static bool Save(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment, Guid ChildFamilyId, DayCareDataContext dbOld, System.Data.Common.DbTransaction tran)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildProgEnrollmentFeeDetail, "Save", "Save method called", DayCarePL.Common.GUID_DEFAULT);
            DayCareDataContext db = dbOld;
            int count = 0;
            ChildProgEnrollmentFeeDetail DBChildProgEnrollmentFeeDetail = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildProgEnrollmentFeeDetail, "Save", "Debug Save called", DayCarePL.Common.GUID_DEFAULT);
                db.Transaction = tran;
                var data = (from spfd in db.SchoolProgramFeesDetails
                            where spfd.FeesPeriodId.Equals(objChildProgEnrollment.FeesPeriodId) && spfd.SchoolProgramId.Equals(objChildProgEnrollment.SchoolProgramId)
                            select new
                            {
                                EffectiveYearDate = spfd.EffectiveYearDate,
                                EffectiveMonthDay = spfd.EffectiveMonthDay,
                                EffectiveWeekDay = spfd.EffectiveWeekDay
                            }).FirstOrDefault();
                if (data != null)
                {


                    count = (from cpefd in db.ChildProgEnrollmentFeeDetails
                             where cpefd.ChildSchoolYearId.Equals(objChildProgEnrollment.ChildSchoolYearId) && cpefd.SchoolProgramId.Equals(objChildProgEnrollment.SchoolProgramId)
                             select cpefd).Count();

                    if (count == 0)
                    {
                        DBChildProgEnrollmentFeeDetail = new ChildProgEnrollmentFeeDetail();
                        DBChildProgEnrollmentFeeDetail.Id = Guid.NewGuid();
                        DBChildProgEnrollmentFeeDetail.CreatedById = objChildProgEnrollment.CreatedById;
                        DBChildProgEnrollmentFeeDetail.CreatedDatetime = DateTime.Now;
                    }
                    else
                    {
                        DBChildProgEnrollmentFeeDetail = db.ChildProgEnrollmentFeeDetails.FirstOrDefault(i => i.ChildSchoolYearId.Equals(objChildProgEnrollment.ChildSchoolYearId) && i.SchoolProgramId.Equals(objChildProgEnrollment.SchoolProgramId));
                    }
                    DBChildProgEnrollmentFeeDetail.ChildSchoolYearId = objChildProgEnrollment.ChildSchoolYearId;
                    DBChildProgEnrollmentFeeDetail.SchoolProgramId = objChildProgEnrollment.SchoolProgramId;
                    DBChildProgEnrollmentFeeDetail.ChildFamilyId = ChildFamilyId;
                    DBChildProgEnrollmentFeeDetail.FeesPeriodId = objChildProgEnrollment.FeesPeriodId;
                    DBChildProgEnrollmentFeeDetail.Fee = objChildProgEnrollment.Fees.Value;
                    DBChildProgEnrollmentFeeDetail.LastmodiedById = objChildProgEnrollment.LastModifiedById;
                    DBChildProgEnrollmentFeeDetail.LastmodifiedDatetime = DateTime.Now;
                    DBChildProgEnrollmentFeeDetail.EnrollmentDate = objChildProgEnrollment.EnrollmentDate;
                    DBChildProgEnrollmentFeeDetail.EnrollmentStatus = objChildProgEnrollment.EnrollmentStatus;
                    if (!objChildProgEnrollment.EnrollmentStatusId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                    {
                        DBChildProgEnrollmentFeeDetail.EnrollmentStatusId = objChildProgEnrollment.EnrollmentStatusId;
                    }
                    else
                    {
                        DBChildProgEnrollmentFeeDetail.EnrollmentStatusId = null;
                    }
                    DBChildProgEnrollmentFeeDetail.EffectiveMonthDay = data.EffectiveMonthDay;
                    DBChildProgEnrollmentFeeDetail.EffectiveWeekDay = data.EffectiveWeekDay;
                    DBChildProgEnrollmentFeeDetail.EffectiveYearDate = data.EffectiveYearDate;
                    DBChildProgEnrollmentFeeDetail.StartDate = objChildProgEnrollment.StartDate;
                    DBChildProgEnrollmentFeeDetail.EndDate = objChildProgEnrollment.EndDate;
                    if (count == 0)
                    {
                        db.ChildProgEnrollmentFeeDetails.InsertOnSubmit(DBChildProgEnrollmentFeeDetail);
                    }
                    db.SubmitChanges();

                    #region Change Enrollment Status for ChildFamilyId and ChildSchoolYearId
                    ChildProgEnrollmentFeeDetail DB = null;
                    var ChildProgEnrollmentFeeDetailIDs = (from cpefd in db.ChildProgEnrollmentFeeDetails
                                                           where cpefd.ChildSchoolYearId.Equals(objChildProgEnrollment.ChildSchoolYearId)
                                                           select cpefd.Id).ToList();
                    foreach (var id in ChildProgEnrollmentFeeDetailIDs)
                    {
                        DB = db.ChildProgEnrollmentFeeDetails.FirstOrDefault(i => i.Id.Equals(id));
                        if (DB != null)
                        {
                            DB.EnrollmentStatus = objChildProgEnrollment.EnrollmentStatus;
                            if (!objChildProgEnrollment.EnrollmentStatusId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                            {
                                DB.EnrollmentStatusId = objChildProgEnrollment.EnrollmentStatusId;
                            }
                            else
                            {
                                DB.EnrollmentStatusId = null;
                            }
                            
                            db.SubmitChanges();
                        }
                    }
                    #endregion
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildProgEnrollmentFeeDetail, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
    }
}
