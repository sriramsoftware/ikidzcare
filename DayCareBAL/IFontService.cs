using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "IFontService" here, you must also update the reference to "IFontService" in App.config.
    [ServiceContract]
    public interface IFontService
    {
        [OperationContract]
        bool Save(DayCarePL.FontProperties objFont);
        [OperationContract]
        DayCarePL.FontProperties[] LoadFont();
    }
}
