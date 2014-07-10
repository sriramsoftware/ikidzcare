using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class ClassRoomProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public Guid SchoolId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int MaxSize
        {
            get;
            set;
        }
        public bool Active
        {
            get;
            set;
        }
        public DateTime? LastModifiedDatetime
        {
            get;
            set;
        }
        public Guid? LastModifiedById
        {
            get;
            set;
        }

        public string SchoolName
        {
            get;
            set;
        }
        public Guid? StaffId
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public string ClassRoomName
        {
            get;
            set;
        }
        public Guid SchoolYearId
        {
            get;
            set;
        }
    }
}
