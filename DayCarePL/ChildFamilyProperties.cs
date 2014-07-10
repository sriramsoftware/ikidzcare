using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildFamilyProperties : CommonProperties
    {
        public Guid SchoolId
        {
            get;
            set;
        }
        public string FamilyTitle
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string Address1
        {
            get;
            set;
        }
        public string Address2
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public Guid? StateId
        {
            get;
            set;
        }
        public string Zip
        {
            get;
            set;
        }
        public string HomePhone
        {
            get;
            set;

        }
        public string Comments
        {
            get;
            set;
        }
        public bool Active { get; set; }
        public string MsgDisplayed
        {
            get;
            set;
        }
        public DateTime? MsgStartDate
        {
            get;
            set;
        }
        public DateTime? MsgEndDate
        {
            get;
            set;
        }
        public bool? MsgActive
        {
            get;
            set;
        }

        public List<DayCarePL.FamilyDataProperties> lstFamily;

        public string SchoolName
        {
            get;
            set;
        }
        public string StateName
        {
            get;
            set;
        }
        public string Email
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
        public decimal OpBal { get; set; }
        public string ChildName
        {
            get;
            set;
        }

        public Guid SchoolYearId { get; set; }
    }
}
