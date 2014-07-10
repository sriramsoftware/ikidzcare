using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ProgStaffProperties : CommonProperties
    {
        public Guid? SchoolProgramId
        {
            get;
            set;
        }
        public Guid StaffId
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }

        public string SchoolProgramTitle
        {
            get;
            set;
        }
        public Guid ProgStaff_StaffId
        {
            get;
            set;
        }
        public string StaffFirstName
        {
            get;
            set;
        }
        public string StaffLastName
        {
            get;
            set;
        }
        public string StaffFullName
        {
            get;
            set;
        }
        public string StaffUserName
        {
            get;
            set;
        }
        public string StaffEmail
        {
            get;
            set;
        }
        public string StaffCity
        {
            get;
            set;
        }
        public string StaffMainPhone
        {
            get;
            set;
        }
        public bool IsPrimary
        {
            get;
            set;
        }
        public string GroupTitle
        {
            get;
            set;
        }
    }
}
