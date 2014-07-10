using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "RelationshipService" here, you must also update the reference to "RelationshipService" in App.config.
    public class RelationshipService : IRelationshipService
    {
        public List<DayCarePL.RelationshipProperties> LoadRelationship(Guid SchoolId)
        {
            return DayCareDAL.clRelationship.LoadRelationship(SchoolId);
        }

        public bool Save(DayCarePL.RelationshipProperties objRelationship)
        {
            return DayCareDAL.clRelationship.Save(objRelationship);
        }

        public bool CheckDuplicateRelationshipName(string RelationshipName, Guid RelationshipId, Guid SchoolId)
        {
            return DayCareDAL.clRelationship.CheckDuplicateRelationshipName(RelationshipName, RelationshipId, SchoolId);
        }
    }
}
