using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class StaffProperties : CommonProperties
    {
        public Guid UserGroupId
        {
            get;
            set;
        }
        public Guid StaffCategoryId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string SchoolName
        {
            get;
            set;
        }
        public string LastName
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
        public string Zip
        {
            get;
            set;
        }
        public Guid? StateId
        {
            get;
            set;
        }
        public Guid? CountryId
        {
            get;
            set;
        }
        public string MainPhone
        {
            get;
            set;
        }
        public string SecondaryPhone
        {
            get;
            set;
        }
        public string Email
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
        public bool Gender
        {
            get;
            set;
        }
        public string SecurityQuestion
        {
            get;
            set;
        }
        public string SecurityAnswer
        {
            get;
            set;
        }
        public string Photo
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public bool IsPrimary
        {
            get;
            set;
        }

        public string UserGroupTitle
        {
            get;
            set;
        }
        public string CountryName
        {
            get;
            set;
        }
        public string StateName
        {
            get;
            set;
        }
        public string StaffCategoryName
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public Guid SchoolId
        {
            get;
            set;
        }
        public Guid StaffSchoolYearId
        {
            get;
            set;
        }
        public Guid ScoolYearId
        {
            get;
            set;
        }
        public Guid RolId
        {
            get;
            set;
        }
    }
}
