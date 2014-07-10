using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayCarePL
{
    public class Common
    {
        public static readonly string GUID_DEFAULT = "00000000-0000-0000-0000-000000000000";
        public static readonly string SCHOOL_ADMINISTRATOR = "School Administrator";
        public static readonly string ADMIN_ROLE_ID = "5BA3E139-ABA5-42CF-9348-B63F5DDED0A5";
        public static readonly string SCHOOL_ADMINISTRATOR_ROLE_ID = "5BA3E139-ABA5-42CF-9348-B63F5DDED0A5";
        public static readonly string DEFAULT_COUNTRY_ID = "03E709AF-0FB1-4D4E-AA9D-BD5EC447E869";
        public static readonly string DEFAULT_ENROLLMENT_STATUS = "Enrolled";
        public static readonly string NOTEAPPLICABLE_CLASSROOMID = "0EDE6022-0EE1-4DD4-97F2-E0F38D622DA8";
        
    }

    public class MenuLink
    {
        public string Name
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
    }
}
