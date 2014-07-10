using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "CountryService" here, you must also update the reference to "CountryService" in App.config.
    public class CountryService : ICountryService
    {
        public bool Save(DayCarePL.CountryProperties objCountry)
        {
            return DayCareDAL.clCountry.Save(objCountry);
        }

        public List<DayCarePL.CountryProperties> LoadCountries()
        {
            return DayCareDAL.clCountry.LoadCountries();
        }
    }
}
