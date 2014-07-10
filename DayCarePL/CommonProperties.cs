using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class CommonProperties
    {
        public Guid Id
        {
            get;
            set;
        }
        public DateTime? CreatedDateTime
        {
            get;
            set;
        }
        public DateTime? LastModifiedDatetime
        {
            get;
            set;
        }
        public Guid? CreatedById
        {
            get;
            set;
        }
        public Guid? LastModifiedById
        {
            get;
            set;
        }
        public Guid ChildFamilyId
        {
            get;
            set;
        }

        public Guid ProgClassRoom_Id
        {
            get;
            set;
        }
        public string ProgClassRoomTitle
        {
            get;
            set;

        }

    }
}
