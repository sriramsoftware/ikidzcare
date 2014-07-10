using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class FamilyDataProperties : CommonProperties 
    {
        public Guid? RelationShipId
        {
            get;
            set;
        }
        public Guid ChildFamilyId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Phone1Type
        {
            get;
            set;
        }
        public string Phone1
        {
            get;
            set;
        }
        public string Phone2Type
        {
            get;
            set;
        }
        public string Phone2
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Photo
        {
            get;
            set;
        }
        public string RelationShipName
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public int GuardianIndex
        {
            get;
            set;
        }

        public Guid SchoolId
        {
            get;
            set;
        }
        public string ChildFamilyComments
        {
            get;
            set;
        }
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

        public string SchoolName
        {
            get;
            set;
        }

        public Guid SchoolYearId { get; set; }

    }
}
