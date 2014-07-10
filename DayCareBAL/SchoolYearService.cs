using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "SchoolYearService" here, you must also update the reference to "SchoolYearService" in App.config.
    public class SchoolYearService : ISchoolYearService
    {
        public List<DayCarePL.SchoolYearProperties> LoadSchoolYear(Guid SchoolId)
        {
            return DayCareDAL.clSchoolYear.LoadSchoolYear(SchoolId);
        }

        public Guid Save(DayCarePL.SchoolYearProperties objSchoolYear,Guid OldCurrentSchoolYearId)
        {
            return DayCareDAL.clSchoolYear.Save(objSchoolYear, OldCurrentSchoolYearId);
        }

        public bool CheckDuplicateSchoolYear(string SchoolYear, Guid SchoolYearId, Guid SchoolId)
        {
            return DayCareDAL.clSchoolYear.CheckDuplicateSchoolYear(SchoolYear, SchoolYearId, SchoolId);
        }

        public List<DayCarePL.SchoolYearProperties> LoadAllSchoolYear(Guid SchoolId)
        {
            return DayCareDAL.clSchoolYear.LoadAllSchoolYear(SchoolId);
        }

        public Guid GetCurrentSchoolYear(Guid SchoolId)
        {
            return DayCareDAL.clSchoolYear.GetCurrentSchoolYear(SchoolId);
        }

        public DayCarePL.SchoolYearProperties LoadSchoolYearDtail(Guid SchoolId, Guid SchoolyearId)
        {
            return DayCareDAL.clSchoolYear.LoadSchoolYearDtail(SchoolId, SchoolyearId);
        }

        public List<DayCarePL.SchoolYearListProperties> GetPreviousYearOfSelectedCurrentYear(Guid SchoolId, Guid CurrentSchoolYearId)
        {
            return DayCareDAL.clSchoolYear.GetPreviousYearOfSelectedCurrentYear(SchoolId, CurrentSchoolYearId);
        }

        public bool IsSelectedYear_Current_NextYearORPrevYear(Guid SchoolId, string SelectedSchoolYear)
        {
            return DayCareDAL.clSchoolYear.IsSelectedYear_Current_NextYearORPrevYear(SchoolId, SelectedSchoolYear);
        }

        //public bool UpdateClosingBalance(Guid SchoolId, Guid SchoolYearId, Guid? ChildFamilyId)
        //{
            //return DayCareDAL.clSchoolYear.UpdateClosingBalance(SchoolId, SchoolYearId, ChildFamilyId);
        //}

        public bool IsSelectedYearPrevYearORNot(Guid SchoolId, Guid SchoolYearId)
        {
            return DayCareDAL.clSchoolYear.IsSelectedYearPrevYearORNot(SchoolId, SchoolYearId);
        }

        public Guid GetCurrentOldSchoolYear(Guid SchoolID)
        {
            return DayCareDAL.clSchoolYear.GetOldCurrentSchoolYearId(SchoolID);
        }
    }
}
