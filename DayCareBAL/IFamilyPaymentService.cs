using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IFamilyPaymentService" here, you must also update the reference to "IFamilyPaymentService" in App.config.
    [ServiceContract]
    public interface IFamilyPaymentService
    {
        [OperationContract]
        List<DayCarePL.FamilyPaymentProperties> GetFamilyWisePayment(Guid ChildFamilyId);

        [OperationContract]
        bool Save(List<DayCarePL.FamilyPaymentProperties> lstFamilyPayment);

        [OperationContract]
        bool Delete(Guid Id);

        [OperationContract]
        DataSet LoadPaymentDeposits(string SearchText, Guid SchoolYearId);
    }
}
