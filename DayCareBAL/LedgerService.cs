using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace DayCareBAL
{
    // NOTE: If you change the class name "LedgerService" here, you must also update the reference to "LedgerService" in App.config.
    public class LedgerService : ILedgerService
    {
        public List<DayCarePL.ChildProgEnrollmentProperties> GetChildProgEnrollmentFeeDetail(Guid SchoolYearId, Guid ChildFamilyId)
        {
            return DayCareDAL.clLedger.GetChildProgEnrollmentFeeDetail(SchoolYearId, ChildFamilyId);
        }

        public bool CheckChildProgInLedger(Guid SchoolYearId, Guid ChildFamilyId, Guid ChildDataId, DateTime TransactionDate, Guid SchoolProgramId)
        {
            return DayCareDAL.clLedger.CheckChildProgInLedger(SchoolYearId, ChildFamilyId, ChildDataId, TransactionDate, SchoolProgramId);
        }

        public bool Save(List<DayCarePL.LedgerProperties> lstLedger)
        {
            return DayCareDAL.clLedger.Save(lstLedger);
        }

        public System.Nullable<DateTime> GetLastTransactionDate(Guid? SchoolYearId, Guid? ChildFamilyId, Guid? ChildDataId, Guid? SchoolProgramId)
        {
            return DayCareDAL.clLedger.GetLastTransactionDate(SchoolYearId, ChildFamilyId, ChildDataId, SchoolProgramId);
        }

        public List<DayCarePL.LedgerProperties> LoadLedgerDetail(Guid SchoolYearId, Guid ChildFamilyId)
        {
            return DayCareDAL.clLedger.LoadLedgerDetail(SchoolYearId, ChildFamilyId);
        }

        public List<DayCarePL.ChargeCodeProperties> LoadChargeCode()
        {
            return DayCareDAL.clLedger.LoadChargeCode();
        }

        public bool SaveLedger(DayCarePL.LedgerProperties objLedger)
        {
            return DayCareDAL.clLedger.SaveLedger(objLedger);
        }

        public bool DeleteLedger(Guid Id)
        {
            return DayCareDAL.clLedger.DeleteLedger(Id);
        }

        public int DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId(Guid ChildFamilyId)
        {
            return DayCareDAL.clLedger.DeleteLedgerDetailsByChildFamilyIdAndSchoolProgramId(ChildFamilyId);
        }

        public bool UpdateLedger(DayCarePL.LedgerProperties objLedger)
        {
            return DayCareDAL.clLedger.UpdateLedger(objLedger);
        } 

        public bool DeleteSelectedLedger(List<Guid> lstLedger)
        {
            return DayCareDAL.clLedger.DeleteSelectedLedger(lstLedger);
        }

        public DataSet LoadLedgerOfFamily(Guid SchoolYearId, Guid ChildFamilyId)
        {
            return DayCareDAL.clLedger.LoadLedgerOfFamily(SchoolYearId, ChildFamilyId);
        }

        public bool EditAllLedger(List<DayCarePL.LedgerProperties> lstLedger)
        {
            return DayCareDAL.clLedger.EditAllLedger(lstLedger);
        }

        public List<DayCarePL.ChildFamilyProperties> GetLateFeeofFamilies(Guid SchoolId, Guid SchoolYearId, decimal Balance)
        {
            return DayCareDAL.clLedger.GetLateFeeofFamilies(SchoolId, SchoolYearId, Balance);
        }

        public bool SaveLateFeeOfFamily(List<DayCarePL.LedgerProperties> lstLedger)
        {
            return DayCareDAL.clLedger.SaveLateFeeOfFamily(lstLedger);
        }

        public List<DayCarePL.ClosingBalance> GetPreviousYearClosingBalance(Guid ChildFamilyId, Guid SchoolYearId)
        {
            return DayCareDAL.clLedger.GetPreviousYearClosingBalance(ChildFamilyId, SchoolYearId);
        }

        //report
        public DataSet GetAccountReceiable(Guid SchoolYearId, DateTime StartDate, string Receivable_Credit)
        {
            return DayCareDAL.clLedger.GetAccountReceiable(SchoolYearId, StartDate, Receivable_Credit);
        }

        public DataSet GetFamilyWiseLateFeesReport(Guid SchoolId, Guid SchoolYearId, Guid ChildFamilyId, string SearchText)
        {
            return DayCareDAL.clLedger.GetFamilyWiseLateFeesReport(SchoolId, SchoolYearId, ChildFamilyId, SearchText);
        }

        public DataSet GetFamilyChildListReport(Guid SchoolId, Guid SchoolyearId,string SearchStr)
        {
            return DayCareDAL.clLedger.GetFamilyChildListReport(SchoolId, SchoolyearId, SearchStr);
        }
    }
}
