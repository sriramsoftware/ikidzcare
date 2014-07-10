using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class LedgerProperties
    {

        public Guid Id
        {
            get;
            set;
        }
        public Guid? SchoolYearId
        {
            get;
            set;
        }
        public Guid? ChildFamilyId
        {
            get;
            set;
        }
        public Guid? ChildDataId
        {
            get;
            set;
        }
        public Guid? PaymentId
        {
            get;
            set;
        }
        public DateTime TransactionDate
        {
            get;
            set;
        }
        public string Detail
        {
            get;
            set;
        }
        public string Comment
        {
            get;
            set;
        }
        public string ChildName
        {
            get;
            set;
        }
        public string Category
        {
            get;
            set;
        }
        public string FamilyName
        {
            get;
            set;
        }
        public decimal Debit
        {
            get;
            set;
        }
        public decimal Credit
        {
            get;
            set;
        }
        public decimal Balance
        {
            get;
            set;
        }
        public bool AllowEdit
        {
            get;
            set;
        }
        public Guid? CreatedById
        {
            get;
            set;
        }
        public Guid LastModifiedById
        {
            get;
            set;
        }
        public DateTime CreatedDateTime
        {
            get;
            set;
        }
        public DateTime LastModifiedDatetime
        {
            get;
            set;
        }
        public Guid? SchoolProgramId
        {
            get;
            set;
        }
        public Guid? ChargeCodeCategoryId
        {
            get;
            set;
        }
        public int? PaymentMethodId
        {
            get;
            set;
        }
        public string ChargeCodeCategoryName
        {
            get;
            set;
        }
        public string PaymentMethodName
        {
            get;
            set;
        }
        public int? LateFee
        {
            get;
            set;
        }

        public bool IsLedgerSelec { get; set; }
    }

    public class ClosingBalance
    {
        public string SchoolYear { get; set; }
        public decimal ClosingBalanceAmount{get;set;}
    }
}
