using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace DayCareDAL
{
    public class clChildList
    {
        #region "Get All Child List, Dt: 29-Set-2011, DB: A"
        public static List<DayCarePL.ChildDataProperties> GetAllChildList(Guid SchoolId, Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clChildList, "GetAllChildList", "GetAllChildList method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.ChildDataProperties> lstChild = new List<DayCarePL.ChildDataProperties>();
            DayCarePL.ChildDataProperties objChild = null;
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clChildList, "GetAllChildList", "Debug GetAllChildList called", DayCarePL.Common.GUID_DEFAULT);
                
                var data = db.spGetAllChildDataList(SchoolId, SchoolYearId);
                foreach (var d in data)
                {
                    objChild = new DayCarePL.ChildDataProperties();
                    objChild.ChildDataId = d.ChildDataId;
                    objChild.FullName = d.ChildFullName;
                    objChild.ChildFamilyId = d.ChildFamilyId;
                    objChild.FamilyName = d.FamilyFullName;
                    objChild.Email = d.Email;
                    objChild.HomePhone = d.HomePhone;
                    objChild.Photo = d.Photo;
                    objChild.Active = d.Active; // (bool) (db.ChildSchoolYears.FirstOrDefault(u=>u.SchoolYearId.Equals(SchoolYearId) && u.ChildDataId.Equals(d.ChildDataId))).active; 
                    objChild.Gender = d.Gender;
                    lstChild.Add(objChild);
                }
                return lstChild;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clChildList, "GetAllChildList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
            
        }

        public static DataSet GetChildList(Guid SchoolId, Guid SchoolYearId, string SearchStr)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                ds.Tables.Add(dt);
                SortedList sl = new SortedList();
                sl.Add("@SchoolId", SchoolId);
                sl.Add("@SchoolYearId", SchoolYearId);
                sl.Add("@SearchChild", SearchStr);//
                ds = clConnection.GetDataSet("spGetRptChildList", sl);
                if (ds != null)
                {
                    return ds;
                }
                else
                {
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clLedger, "spGetRptChildList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return ds;
            }
        }
        #endregion        
    }
}
