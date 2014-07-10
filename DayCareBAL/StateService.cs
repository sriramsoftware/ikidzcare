using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "StateService" here, you must also update the reference to "StateService" in App.config.
    public class StateService : IStateService
    {
        public List<DayCarePL.StateProperties> LoadStates()
        {
            return DayCareDAL.clState.LoadStates();
        }

        public bool Save(DayCarePL.StateProperties objState)
        {
            return DayCareDAL.clState.Save(objState);
        }
    }
}
