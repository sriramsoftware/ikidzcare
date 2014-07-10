using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IRelationshipService" here, you must also update the reference to "IRelationshipService" in App.config.
    [ServiceContract]
    public interface IRelationshipService
    {
        [OperationContract]
        List<DayCarePL.RelationshipProperties> LoadRelationship(Guid SchoolId);

        [OperationContract]
        bool Save(DayCarePL.RelationshipProperties objRelationship);

        [OperationContract]
        bool CheckDuplicateRelationshipName(string RelationshipName, Guid RelationshipId, Guid SchoolId);
    }
}
