using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class AdditionalNotesProperties : CommonProperties
    {
        public DateTime CommentDate
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public Guid ChildSchoolYearId
        {
            get;
            set;
        }
    }
}
