using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the interface name "ICountryService" here, you must also update the reference to "ICountryService" in App.config.
    [ServiceContract]
    public interface ICountryService
    {
        [OperationContract]
        bool Save(DayCarePL.CountryProperties objCountry);

        [OperationContract]
        List<DayCarePL.CountryProperties> LoadCountries();
    }
}
