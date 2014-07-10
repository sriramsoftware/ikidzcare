using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IAdditionalNotesService" here, you must also update the reference to "IAdditionalNotesService" in App.config.
    [ServiceContract]
    public interface IAdditionalNotesService
    {
        [OperationContract]
        bool Save(DayCarePL.AdditionalNotesProperties objNotes);
        [OperationContract]
        List<DayCarePL.AdditionalNotesProperties> LoadAdditionalNotes(Guid ChildSchoolYearId);
        [OperationContract]
        DayCarePL.AdditionalNotesProperties[] GetAdditionNoteById(Guid Id, Guid ChildSchoolYearId);
        [OperationContract]
        bool DeleteAdditionalNotes(Guid Id);
    }
}
