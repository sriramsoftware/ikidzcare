using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DayCareBAL
{
    // NOTE: If you change the class name "MyAccountService" here, you must also update the reference to "MyAccountService" in App.config.
    public class MyAccountService : IMyAccountService
    {
        public DayCarePL.StaffProperties LoadMyAccountDetails(Guid StaffId)
        {
            return DayCareDAL.clMyAccount.LoadMyAccountDetails(StaffId);
        }
       public bool Save(DayCarePL.StaffProperties objStaff)
       {
           return DayCareDAL.clMyAccount.Save(objStaff);
       }
    }
}
