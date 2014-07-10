using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ILedgerService" here, you must also update the reference to "ILedgerService" in App.config.
    [ServiceContract]
    public interface ILedgerService
    {
        [OperationContract]
        List<DayCarePL.ChildProgEnrollmentProperties> GetChildProgEnrollmentFeeDetail(Guid SchoolYearId, Guid ChildFamilyId);

        [OperationContract]
        bool CheckChildProgInLedger(Guid SchoolYearId, Guid ChildFamilyId, Guid ChildDataId, DateTime TransactionDate, Guid SchoolProgramId);

        [OperationContract]
        bool Save(List<DayCarePL.LedgerProperties> lstLedger);

        [OperationContract]
        System.Nullable<DateTime> GetLastTransactionDate(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, Guid? SchoolProgramId);

        [OperationContract]
        List<DayCarePL.LedgerProperties> LoadLedgerDetail(Guid SchoolYearId, Guid ChildFamilyId);
        
        [OperationContract]
        List<DayCarePL.ChargeCodeProperties> LoadChargeCode();
        
        [OperationContract]
        bool SaveLedger(DayCarePL.LedgerProperties objLedger);
        
        [OperationContract]
        bool DeleteLedger(Guid Id);

        [OperationContract]
        int DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId(Guid ChildFamilyId);

        [OperationContract]
        bool UpdateLedger(DayCarePL.LedgerProperties objLedger);

        [OperationContract]
        bool DeleteSelectedLedger(List<Guid> lstLedger);

        [OperationContract]
        bool EditAllLedger(List<DayCarePL.LedgerProperties> lstLedger);

        [OperationContract]
        List<DayCarePL.ChildFamilyProperties> GetLateFeeofFamilies(Guid SchoolId, Guid SchoolYearId, decimal Balance);

        [OperationContract]
        bool SaveLateFeeOfFamily(List<DayCarePL.LedgerProperties> lstLedger);

        //report
        [OperationContract]
        DataSet GetAccountReceiable(Guid SchoolYearId, DateTime StartDate, string Receivable_Credit);

        [OperationContract]
        DataSet GetFamilyWiseLateFeesReport(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId, string SearchText);

        [OperationContract]
        List<DayCarePL.ClosingBalance> GetPreviousYearClosingBalance(Guid ChildFamilyId, Guid SchoolYearId);
        [OperationContract]
        DataSet GetFamilyChildListReport(Guid SchoolId, Guid SchoolYearId,string SearchStr);
    }
}
