using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "FamilyPaymentService" here, you must also update the reference to "FamilyPaymentService" in App.config.
    public class FamilyPaymentService : IFamilyPaymentService
    {
        public List<DayCarePL.FamilyPaymentProperties> GetFamilyWisePayment(Guid ChildFamilyId)
        {
            return DayCareDAL.clFamilyPayment.GetFamilyWisePayment(ChildFamilyId);
        }

        public bool Save(List<DayCarePL.FamilyPaymentProperties> lstFamilyPayment)
        {
            return DayCareDAL.clFamilyPayment.Save(lstFamilyPayment);
        }

        public bool Delete(Guid Id)
        {
            return DayCareDAL.clFamilyPayment.Delete(Id);
        }
        public DataSet LoadPaymentDeposits(string SearchText, Guid SchoolYearId)
        {
            return DayCareDAL.clFamilyPayment.LoadPaymentDeposits(SearchText,SchoolYearId);
        }
    }
}
