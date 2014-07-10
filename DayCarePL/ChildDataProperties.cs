using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildDataProperties : CommonProperties
    {
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
        public bool? Gender
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public DateTime? DOB
        {
            get;
            set;
        }
        public string SocSec
        {
            get;
            set;
        }
        public string Photo
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public Guid ChildDataId
        {
            get;
            set;
        }
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public string FamilyName
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public int ImportedCount { get; set; }

        public Guid ChildEnrollmentStatusId
        {
            get;
            set;
        }
        public Guid EnrollmentStatusId
        {
            get;
            set;
        }
        public string EnrollmentStatus
        {
            get;
            set;
        }
        public DateTime? EnrollmentDate
        {
            get;
            set;
        }
        public string ProgramName
        {
            get;
            set;
        }
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string HomePhone
        {
            get;
            set;
        }

    }

    public class Child_Family
    {
        public Guid ChildDataId { get; set; }
        public Guid ChildFamilyId { get; set; }
    }
}
