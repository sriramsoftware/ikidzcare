using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace DayCareDAL
{
    public class clSchool
    {
        #region "Load School Info, Dt: 26-Aug-2011, DB: A"
        public static DayCarePL.SchoolProperties LoadSchoolInfo(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "LoadSchoolInfo method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "Debug LoadSchoolInfo called", DayCarePL.Common.GUID_DEFAULT);
                DayCarePL.SchoolProperties objSchool = null;

                objSchool = (from s in db.Schools
                             join st in db.States on s.StateId equals st.Id into state
                             join c in db.Countries on s.CountryId equals c.Id into country
                             from ctry in country.DefaultIfEmpty()
                             where s.Id.Equals(SchoolId)
                             select new DayCarePL.SchoolProperties()
                             {
                                 Id = s.Id,
                                 Name = s.Name,
                                 Address1 = s.Address1,
                                 Address2 = s.Address2,
                                 City = s.City,
                                 Zip = s.Zip,
                                 StateId = s.StateId,
                                 CountryId = s.CountryId,
                                 StateName = s.Name,
                                 CountryName = ctry.Name,
                                 MainPhone = s.MainPhone,
                                 SecondaryPhone = s.SecondaryPhone,
                                 Fax = s.Fax,
                                 Email = s.Email,
                                 WebSite = s.WebSite,
                                 TimeZoneId = s.TimeZoneId,
                                 CodeRequired = s.CodeRequired,
                                 LateFeeAmount = s.LateFeeAmount.HasValue == true ? s.LateFeeAmount.Value : 0,
                                 Comments = s.Comments,
                                 iPadHeader = s.iPadHeader,
                                 iPadHeaderFont = s.iPadHeaderFont,
                                 iPadHeaderFontSize = s.iPadHeaderFontSize,
                                 iPadHeaderColor = s.iPadHeaderColor,
                                 iPadMessage = s.iPadMessage,
                                 iPadMessageFont = s.iPadMessageFont,
                                 iPadMessageFontSize = s.iPadMessageFontSize,
                                 iPadBackgroundImage = s.iPadBackgroundImage,
                                 iPadMessageColor = s.iPadMessageColor,
                                 CreatedDateTime = s.CreatedDateTime,
                                 LastModifiedDatetime = s.LastModifiedDatetime,
                                 CreatedById = s.CreatedById,
                                 LastModifiedById = s.LastModifiedById
                             }).FirstOrDefault();
                return objSchool;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save, Dt: 26-Aug-2011, DB: A"
        public static bool Save(DayCarePL.SchoolProperties objSchool)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "LoadSchoolInfo method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            School DBSchool = new School();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "Debug LoadSchoolInfo called", DayCarePL.Common.GUID_DEFAULT);

                DBSchool = db.Schools.FirstOrDefault(id => id.Id.Equals(objSchool.Id));
                DBSchool.Id = objSchool.Id;
                DBSchool.Name = objSchool.Name;
                DBSchool.Address1 = objSchool.Address1;
                DBSchool.Address2 = objSchool.Address2;
                DBSchool.City = objSchool.City;
                DBSchool.Zip = objSchool.Zip;
                if (!objSchool.CountryId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBSchool.CountryId = objSchool.CountryId;
                }
                if (!objSchool.StateId.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBSchool.StateId = objSchool.StateId;
                }
                DBSchool.MainPhone = objSchool.MainPhone;
                DBSchool.SecondaryPhone = objSchool.SecondaryPhone;
                DBSchool.Fax = objSchool.Fax;
                DBSchool.Email = objSchool.Email;
                DBSchool.WebSite = objSchool.WebSite;
                DBSchool.CodeRequired = objSchool.CodeRequired;
                DBSchool.LateFeeAmount = objSchool.LateFeeAmount;
                DBSchool.Comments = objSchool.Comments;
                DBSchool.iPadHeader = objSchool.iPadHeader;
                DBSchool.iPadHeaderFont = objSchool.iPadHeaderFont;
                DBSchool.iPadHeaderFontSize = objSchool.iPadHeaderFontSize;
                DBSchool.iPadHeaderColor = objSchool.iPadHeaderColor;
                DBSchool.iPadMessage = objSchool.iPadMessage;
                DBSchool.iPadMessageFont = objSchool.iPadMessageFont;
                DBSchool.iPadMessageFontSize = objSchool.iPadMessageFontSize;
                if (!string.IsNullOrEmpty(objSchool.iPadBackgroundImage))
                {
                    DBSchool.iPadBackgroundImage = DBSchool.Id + System.IO.Path.GetExtension(objSchool.iPadBackgroundImage);
                }
                else
                {
                    DBSchool.iPadBackgroundImage = string.Empty;
                }

                DBSchool.iPadMessageColor = objSchool.iPadMessageColor;
                DBSchool.LastModifiedDatetime = DateTime.Now;
                DBSchool.CreatedById = objSchool.CreatedById;
                DBSchool.LastModifiedById = objSchool.LastModifiedById;

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return false;
            }
        }
        #endregion

        #region "Load School Info, Dt: 14-Oct-2011, DB: A"
        public static decimal GetLateFeeAmount(Guid SchoolId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "LoadSchoolInfo method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", "Debug LoadSchoolInfo called", DayCarePL.Common.GUID_DEFAULT);
                var LateFeeAmount = (from s in db.Schools
                                     where s.Id.Equals(SchoolId)
                                     select s.LateFeeAmount.HasValue == true ? s.LateFeeAmount.Value : 0).FirstOrDefault();
                return LateFeeAmount;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadSchoolInfo", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return 0;
            }
        }
        #endregion

        #region "loadAllSchool, Dt: 18-april-2012, DB: A"
        public static List<DayCarePL.SchoolProperties> LoadAllSchool()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchool, "LoadAllSchool", "LoadAllSchool method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchool, "LoadAllSchool", "Debug LoadAllSchool called", DayCarePL.Common.GUID_DEFAULT);
                return (from s in db.Schools
                        orderby s.Name ascending
                        where s.active.Equals(true)
                        select new DayCarePL.SchoolProperties
                        {
                            Id = s.Id,
                            Name = s.Name
                        }).ToList();

            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadAllSchool", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion


        #region "Get SchoolId by SchoolYearId, Dt: 26-Aug-2011, DB: A"
        public static Guid GetSchoolIdbySchoolYearId(Guid SchoolYearId)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clSchool, "GetSchoolIdbySchoolYearId", "GetSchoolIdbySchoolYearId method called", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clSchool, "GetSchoolIdbySchoolYearId", "Debug GetSchoolIdbySchoolYearId called", DayCarePL.Common.GUID_DEFAULT);
                Guid objSchool ;

                objSchool = (from s in db.SchoolYears
                             where s.Id.Equals(SchoolYearId)
                             select s.SchoolId).FirstOrDefault();
                return objSchool;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "GetSchoolIdbySchoolYearId", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return new Guid();
            }
        }
        #endregion



        #region "JSON: Load SchoolId with CurrentSchoolYearId"
        public static List<DayCarePL.SchoolListProperties> LoadSchoolList()
        {
            clConnection.DoConnection();
            List<DayCarePL.SchoolListProperties> result = new List<DayCarePL.SchoolListProperties>();
            try
            {
                DataSet dsSchoolList = new DataSet();
                dsSchoolList = clConnection.GetDataSet("GetSchoolList");
                DayCarePL.SchoolListProperties objSchoolList = null;
                foreach (DataRow dr in dsSchoolList.Tables[0].Rows)
                {
                    objSchoolList = new DayCarePL.SchoolListProperties();
                    objSchoolList.SchoolId = new Guid(dr["SchoolId"].ToString());
                    objSchoolList.SchoolName = dr["SchoolName"].ToString();
                    objSchoolList.CurrentSchoolYearId = new Guid(dr["CurrentSchoolYearId"].ToString());
                    result.Add(objSchoolList);
                    objSchoolList = null;
                }
            }
            catch(Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadSchoolList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return result;
        }
        #endregion


        #region "JSON: Load SchoolList with SchoolYearList as per SchoolId"
        public static List<DayCarePL.SchoolListWithSchoolYearProperties> LoadSchoolListWithSchoolYearList()
        {
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();
            List<DayCarePL.SchoolListWithSchoolYearProperties> result = new List<DayCarePL.SchoolListWithSchoolYearProperties>();
            try
            {
                DataSet dsSchoolList = new DataSet();
                dsSchoolList = clConnection.GetDataSet("GetSchoolList");
                DayCarePL.SchoolListWithSchoolYearProperties objSchoolList = null;
                
                foreach (DataRow dr in dsSchoolList.Tables[0].Rows)
                {                   
                    objSchoolList = new DayCarePL.SchoolListWithSchoolYearProperties();
               
                    objSchoolList.SchoolId = new Guid(dr["SchoolId"].ToString());
                    objSchoolList.SchoolName = dr["SchoolName"].ToString();
                    objSchoolList.SchoolYearList = (from u in db.SchoolYears where u.SchoolId.Equals(objSchoolList.SchoolId) orderby u.StartDate descending select new DayCarePL.SchoolYearListProperties() { SchoolYearId = u.Id, Year = u.Year }).ToList();
                                                    
                    result.Add(objSchoolList);
                    objSchoolList = null;
                }
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clSchool, "LoadSchoolList", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
            }
            return result;
        }
        #endregion
    }
}
