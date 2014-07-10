using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "AddEditChildService" here, you must also update the reference to "AddEditChildService" in App.config.
    public class AddEditChildService : IAddEditChildService
    {
        //Child
        public DayCarePL.AddEdditChildProperties ChildSave(DayCarePL.AddEdditChildProperties objChildData)
        {
            return DayCareDAL.clAddEditChild.ChildSave(objChildData);
        }

        public DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadChildDataId(ChildDataId, SchoolId, SchoolYearId);
        }

        public List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId)
        {
            return DayCareDAL.clAddEditChild.LoadChildData(SchoolId, SchoolYearId, ChildFamilyId);
        }

        //Child Program
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram(Guid SchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadProgram(SchoolYearId);
        }
        public List<DayCarePL.ChildProgEnrollmentProperties> LoadSecondaryProgram(Guid SchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadSecondaryProgram(SchoolYearId);
        }
        //public decimal GetFees(Guid SchoolProgramId)
        //{
        //    return DayCareDAL.clAddEditChild.GetFees(SchoolProgramId);
        //}

        public decimal GetFees(Guid SchoolProgramId, Guid FeesPeriodId)
        {
            return DayCareDAL.clAddEditChild.GetFees(SchoolProgramId, FeesPeriodId);
        }

        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId)
        {
            return DayCareDAL.clAddEditChild.LoadProgClassRoom(SchoolProgramId);
        }
        
        //public Guid ChildProgEnrollmentSave(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment)
        //{
        //    return DayCareDAL.clAddEditChild.ChildProgEnrollmentSave(objChildProgEnrollment);
        //}


        public int ChildProgEnrollmentDelete(Guid Id)
        {
            return DayCareDAL.clAddEditChild.ChildProgEnrollmentDelete(Id);
        }

        public bool DeleteSchoolProgramChildProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            return DayCareDAL.clAddEditChild.DeleteSchoolProgramChildProgEnrollment(ChildSchoolYearId, SchoolProgramId);
        }

        public List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId)
        {
            return DayCareDAL.clAddEditChild.LoadProgEnrollment(ChildSchoolYearId, SchoolProgramId);
        }

        public List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadAllProgEnrolled(ChildSchoolYearId);
        }

        public List<DayCarePL.FeesPeriodProperties> GetFessPeriodFromSchoolProgramFeesDetail(Guid SchoolProgramId)
        {
            return DayCareDAL.clAddEditChild.GetFessPeriodFromSchoolProgramFeesDetail(SchoolProgramId);
        }

        public List<DayCarePL.ChildProgEnrollmentProperties> LoadAllDistinctProgEnrolled(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadAllDistinctProgEnrolled(ChildSchoolYearId);
        }



        //Child Enrollment Status
        //public bool ChildEnrollmentStatusSave(DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment)
        //{
        //    return DayCareDAL.clAddEditChild.ChildEnrollmentStatusSave(objChildEnrollment);
        //}

        public List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAddEditChild.LoadChildEnrollmentStatus(SchoolId, ChildSchoolYearId);
        }

        public bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id)
        {
            return DayCareDAL.clAddEditChild.CheckDuplicateChildEnrollmentStatus(ChildSchoolYearId, EnrollmentStatusId, EnrollmentDate, Id);
        }
        public List<DayCarePL.ChildProgEnrollmentProperties> GetIsPrimarySchoolProgram(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAddEditChild.GetIsPrimarySchoolProgram(ChildSchoolYearId);
        }
    }
}
