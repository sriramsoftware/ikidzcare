using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IAddEditChildService" here, you must also update the reference to "IAddEditChildService" in App.config.
    [ServiceContract]
    public interface IAddEditChildService
    {
        //Child
        [OperationContract]
        DayCarePL.AddEdditChildProperties ChildSave(DayCarePL.AddEdditChildProperties objChildData);

        [OperationContract]
        DayCarePL.ChildDataProperties LoadChildDataId(Guid ChildDataId, Guid SchoolId, Guid SchoolYearId);

        [OperationContract]
        List<DayCarePL.ChildDataProperties> LoadChildData(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId);

        //Child Program
        //[OperationContract]
       // Guid ChildProgEnrollmentSave(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram(Guid SchoolYearId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadSecondaryProgram(Guid SchoolYearId);
        //[OperationContract]
        //decimal GetFees(Guid SchoolProgramId);

        [OperationContract]
        decimal GetFees(Guid SchoolProgramId, Guid FeesPeriodId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId);

        [OperationContract]
        int ChildProgEnrollmentDelete(Guid Id);

        [OperationContract]
        bool DeleteSchoolProgramChildProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId);

        [OperationContract]
        List<DayCarePL.FeesPeriodProperties> GetFessPeriodFromSchoolProgramFeesDetail(Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadAllDistinctProgEnrolled(Guid ChildSchoolYearId);


        //Child Enrollment Status
        //[OperationContract]
        //bool ChildEnrollmentStatusSave(DayCarePL.ChildEnrollmentStatusProperties objChildEnrollment);

        [OperationContract]
        List<DayCarePL.ChildEnrollmentStatusProperties> LoadChildEnrollmentStatus(Guid SchoolId, Guid ChildSchoolYearId);

        [OperationContract]
        bool CheckDuplicateChildEnrollmentStatus(Guid ChildSchoolYearId, Guid EnrollmentStatusId, DateTime EnrollmentDate, Guid Id);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> GetIsPrimarySchoolProgram(Guid ChildSchoolYearId);
    }
}
