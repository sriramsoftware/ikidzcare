using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ChildProgEnrollmentProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid CreatedById
        {
            get;
            set;
        }
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
        public Guid SchoolProgramId
        {
            get;
            set;
        }
        public Guid ProgClassRoomId
        {
            get;
            set;
        }
        public Guid LastModifiedById
        {
            get;
            set;
        }
        public int DayIndex
        {
            get;
            set;
        }
        public string DayType
        {
            get;
            set;
        }
        public string ProgClassRoomTitle
        {
            get;
            set;
        }
        public string ProgramTitle
        {
            get;
            set;
        }
        public Guid ClassRoomId
        {
            get;
            set;
        }
        public decimal? Fees
        {
            get;
            set;
        }
        public DateTime LastModifiedDateTime
        {
            get;
            set;
        }
        public DateTime CreateDateTime
        {
            get;
            set;
        }
        public string Day
        {
            get;
            set;
        }
        public string ClassRoom
        {
            get;
            set;
        }
        public Guid? FeesPeriodId
        {
            get;
            set;
        }
        public string FeesPeriodName
        {
            get;
            set;
        }
        public bool IsPrimary
        {
            get;
            set;
        }
        public Guid ChildDataId
        {
            get;
            set;
        }

        public Guid ChildProgEnrollmentFeeDetailId
        {
            get;
            set;
        }
        public Guid ChildFamilyId
        {
            get;
            set;
        }
        public DateTime? EffectiveYearDate
        {
            get;
            set;
        }
        public int? EffectiveMonthDay
        {
            get;
            set;
        }
        public int? EffectiveWeekDay
        {
            get;
            set;
        }

        public DateTime? EnrollmentDate
        {
            get;
            set;
        }
        public string EnrollmentStatus
        {
            get;
            set;
        }
        public Guid? EnrollmentStatusId
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
    }
}
