using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "AdditionalNotesService" here, you must also update the reference to "AdditionalNotesService" in App.config.
    public class AdditionalNotesService : IAdditionalNotesService
    {
        public bool Save(DayCarePL.AdditionalNotesProperties objNotes)
        {
            return DayCareDAL.clAdditionalNotes.Save(objNotes);
        }
        public List<DayCarePL.AdditionalNotesProperties> LoadAdditionalNotes(Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAdditionalNotes.LoadAdditionalNotes(ChildSchoolYearId);
        }

        public DayCarePL.AdditionalNotesProperties[] GetAdditionNoteById(Guid Id, Guid ChildSchoolYearId)
        {
            return DayCareDAL.clAdditionalNotes.GetAdditionNoteById(Id, ChildSchoolYearId);
        }
        public bool DeleteAdditionalNotes(Guid Id)
        {
            return DayCareDAL.clAdditionalNotes.DeleteAdditionalNotes(Id);
        }
    }
}
