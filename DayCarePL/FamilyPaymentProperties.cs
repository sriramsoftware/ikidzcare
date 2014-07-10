using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class FamilyPaymentProperties : CommonProperties
    {
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
        public DateTime? PostDate
        {
            get;
            set;
        }
        public string PaymentMethod
        {
            get;
            set;
        }
        public string PaymentDetail
        {
            get;
            set;
        }
        public decimal Amount
        {
            get;
            set;
        }

        public string PaymentMethodName
        {
            get;
            set;
        }
        public string Detail
        {
            get;
            set;
        }
    }
}
