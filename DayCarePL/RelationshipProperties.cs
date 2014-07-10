using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class RelationshipProperties
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
    }
}
