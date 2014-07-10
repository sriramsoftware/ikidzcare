using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class AddEdditChildProperties : CommonProperties
    {
        //Child
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
        public string ChildComments
        {
            get;
            set;
        }
        public Guid ChildDataId
        {
            get;
            set;
        }
        public Guid SchoolYearId
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

        public DateTime? StartDate
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }

        //public string EnrollmentStatus
        //{
        //    get;
        //    set;
        //}
        //public DateTime? EnrollmentDate
        //{
        //    get;
        //    set;
        //}
        //public string ProgramName
        //{
        //    get;
        //    set;
        //}
        //public Guid SchoolProgramId
        //{
        //    get;
        //    set;
        //}

        public List<ChildProgEnrollmentProperties> lstChildProgEnrolment;

        //Child Enrollment
        public Guid? ChildEnrollmentId
        {
            get;
            set;
        }
        public Guid? EnrollmentStatusId
        {
            get;
            set;
        }
        //public Guid ChildSchoolYearId
        //{
        //    get;
        //    set;
        //}
        public Guid StatusId
        {
            get;
            set;
        }
        public DateTime? EnrollmentDate
        {
            get;
            set;
        }
        public string ChildEnrollmentComments
        {
            get;
            set;
        }

        public string EnrollmentStatus
        {
            get;
            set;
        }
    }
}
