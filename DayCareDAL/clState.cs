using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Linq;

namespace DayCareDAL
{
    public class clState
    {
        #region "Load State, Dt: 1-Aug-2011, DB: A"
        public static List<DayCarePL.StateProperties> LoadStates()
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clState, "LoadStates", "Execute LoadStates Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            DayCareDataContext db = new DayCareDataContext();

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clState, "LoadStates", "Debug LoadStates Method", DayCarePL.Common.GUID_DEFAULT);
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                XDocument xDocState = XDocument.Load(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\StateList.xml");

                IEnumerable<DayCarePL.StateProperties> StateData = (from c in xDocState.Descendants("State")

                                                               //where c.Element("CountryId").Value.Equals(CountryId)
                                                               orderby c.Element("Name").Value
                                                               select new DayCarePL.StateProperties()
                                                               {
                                                                   Id = new Guid(c.Element("Id").Value),
                                                                   Name = c.Element("Name").Value,
                                                                   CountryId = new Guid(c.Element("CountryId").Value),
                                                                   //CountryName = clCountry.GetCountryNameByStateId(c.Element("CountryId").Value)
                                                               }).ToList();

                //return CountryData;
                List<DayCarePL.StateProperties> lstState = new List<DayCarePL.StateProperties>();
                XDocument xDocCountry = XDocument.Load(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\CountryList.xml");
                foreach (DayCarePL.StateProperties state in StateData)
                {
                    var Country = (from c in xDocCountry.Descendants("Country")
                                where c.Element("Id").Value.Equals(state.CountryId.ToString())
                                select new
                                {
                                    CompanyName=c.Element("Name").Value
                                });
                    state.CountryName = Country.Select(c => c.CompanyName).SingleOrDefault();
                    lstState.Add(state);
                }
                return lstState;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clState, "LoadStates", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                return null;
            }
        }
        #endregion

        #region "Save State, Dt: 1-Aug-2011, DB: A"
        public static bool Save(DayCarePL.StateProperties objState)
        {
            DayCarePL.Logger.Write(DayCarePL.LogType.INFO, DayCarePL.ModuleToLog.clState, "Save", "Execute Save Method", DayCarePL.Common.GUID_DEFAULT);
            clConnection.DoConnection();
            bool result = false;
            DayCareDataContext db = new DayCareDataContext();
            State DBState = null;

            try
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.DEBUG, DayCarePL.ModuleToLog.clState, "Save", "Debug Save Method", DayCarePL.Common.GUID_DEFAULT);
                Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                XDocument xDoc = XDocument.Load(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\StateList.xml");

                if (objState.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    DBState = new State();
                    DBState.Id = Guid.NewGuid();
                    xDoc.Element("States").Add(new XElement("State", new XElement("Id", DBState.Id), new XElement("Name", objState.Name), new XElement("CountryId", objState.CountryId)));
                    xDoc.Save(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\StateList.xml");
                }
                else
                {
                    DBState = db.States.SingleOrDefault(c => c.Id.Equals(objState.Id));
                    var statedata = (from c in xDoc.Descendants("State")
                                     where c.Element("Id").Value.Equals(objState.Id.ToString())
                                     select c).Single();

                    statedata.Element("Name").Value = objState.Name;
                    statedata.Element("CountryId").Value = Convert.ToString(objState.CountryId);
                    xDoc.Save(myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "XML\\StateList.xml");
                }
                DBState.Name = objState.Name;
                DBState.CountryId = objState.CountryId;
                if (objState.Id.ToString().Equals(DayCarePL.Common.GUID_DEFAULT))
                {
                    db.States.InsertOnSubmit(DBState);
                }
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                DayCarePL.Logger.Write(DayCarePL.LogType.EXCEPTION, DayCarePL.ModuleToLog.clState, "Save", ex.Message.ToString(), DayCarePL.Common.GUID_DEFAULT);
                result = false;
            }
            return result;
        }
        #endregion
    }
}
