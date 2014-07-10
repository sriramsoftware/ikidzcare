using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;


namespace DayCareDAL
{
    public class clCountry
    {
        #region "Load Country, Dt: 30-Jul-2011, DB: A"
        public static List<DayCarePL.CountryProperties> LoadCountries()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clCountry, "LoadCountries", "Execute LoadCountries Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clCountry, "LoadCountries", "Debug LoadCountries Method", DayCarePL.Common.GUID_DEFAULT);
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                XDocument xDoc = XDocument.Load(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");

                var CountryData = (from c in xDoc.Descendants("Country")
                                   orderby c.Element("Name").Value
                                   select new DayCarePL.CountryProperties()
                                   {
                                       Id = new Guid(c.Element("Id").Value),
                                       Name = c.Element("Name").Value
                                   }).ToList();

                return CountryData;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clCountry, "LoadCountries", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save Country, Dt: 30-Jul-2011, DB: A"
        public static bool Save(DayCarePL.CountryProperties objCountry)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clCountry, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            Country DBCountry = null;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clCountry, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                XDocument xDoc = XDocument.Load(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");

                if (objCountry.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBCountry = new Country();
                    DBCountry.Id = Guid.NewGuid();
                    xDoc.Element("Countries").Add(new XElement("Country", new XElement("Id", DBCountry.Id), new XElement("Name", objCountry.Name)));
                    xDoc.Save(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");
                }
                else
                {
                    DBCountry = db.Countries.SingleOrDefault(c => c.Id.Equals(objCountry.Id));
                    var countrydata = (from c in xDoc.Descendants("Country")
                                       where c.Element("Id").Value.Equals(objCountry.Id.ToString())
                                       select c).Single();

                    countrydata.Element("Name").Value = objCountry.Name;
                    xDoc.Save(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");
                }
                DBCountry.Name = objCountry.Name;
                if (objCountry.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.Countries.InsertOnSubmit(DBCountry);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clCountry, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion

        #region "Check Duplicate, Dt: 1-Aug-2011, DB: A"
        //public bool CheckDuplicate(string Name)
        //{
        //    DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clCountry, "CheckDuplicate", "Execute CheckDuplicate Method", DayCarePL.Common.GUID_DEFAULT);

        //    clConnection.DoConnection();
        //    DayCareDataContext db = new DayCareDataContext();
        //    bool Result = false;
        //    try
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clCountry, "CheckDuplicate", "Debug CheckDuplicate Method", DayCarePL.Common.GUID_DEFAULT);
        //       // Result = db.sp
        //    }
        //    catch (Exception ex)
        //    {
        //        DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clCountry, "CheckDuplicate", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
        //        Result = false;
        //    }

        //    return Result;
        //}
        #endregion
    }
}
