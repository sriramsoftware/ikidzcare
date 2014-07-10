using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IChildProgEnrollmentService" here, you must also update the reference to "IChildProgEnrollmentService" in App.config.
    [ServiceContract]
    public interface IChildProgEnrollmentService
    {
        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgClassRoom(Guid SchoolProgramId);
        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgram();
        [OperationContract]
        decimal GetFees(Guid SchoolProgramId);
        [OperationContract]
        Guid Save(DayCarePL.ChildProgEnrollmentProperties objChildProgEnrollment);

        [OperationContract]
        int Delete(Guid Id);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadProgEnrollment(Guid ChildSchoolYearId, Guid SchoolProgramId);

        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> LoadAllProgEnrolled(Guid ChildSchoolYearId);
    }
}
