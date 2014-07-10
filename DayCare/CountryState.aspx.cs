using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;


namespace DayCare
{
    public partial class CountryState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // List<countries> lstCountry = new List<countries>();
            //XmlSerializer serializer = new XmlSerializer(typeof(List<countries>));
           // serializer = new XmlSerializer(typeof(List<countries>));
           // TextReader textReader = new StreamReader(Server.MapPath("XML/country_state.xml"));
            //lstCountry = (List<countries>)serializer.Deserialize(textReader);
            //textReader.Close();



            #region Add State to DB
            //XmlDocument xDoc = new XmlDocument();
            //xDoc.Load(Server.MapPath("XML/country_state.xml"));
            //XmlNodeList lst = xDoc.GetElementsByTagName("country");
            //SqlConnection cn = new System.Data.SqlClient.SqlConnection("Data Source=192.168.1.100;Initial Catalog=DayCare;Persist Security Info=True;User ID=sa;Password=password;Integrated Security=False");
            //cn.Open();
            //SqlDataReader dr;
            //SqlCommand cmCountry = new SqlCommand();
            //SqlCommand cmState = new SqlCommand();
            //Guid id = new Guid();
            //Guid CountryId = new Guid();
            //Guid StateId = new Guid();
            //int cnt = 0, country = 0;
            //StringBuilder sbCountryNotAvailable = new StringBuilder();
            //foreach (XmlNode xmlNode in lst)
            //{
            //    CountryId = Guid.NewGuid();
            //    cmCountry.CommandText = "insert into Country (Id,Name) values('" + CountryId + "','" + xmlNode.Attributes[0].Value.ToUpper() + "')";
            //    cmCountry.Connection = cn;
            //    cmCountry.CommandType = System.Data.CommandType.Text;
            //    cmCountry.ExecuteNonQuery();

            //    //cmCountry.CommandText = "select * from Country where Name='" + xmlNode.Attributes[0].Value.ToUpper() + "'";
            //    //cmCountry.Connection = cn;
            //    //cmCountry.CommandType = System.Data.CommandType.Text;
            //    //dr = cmCountry.ExecuteReader();
            //    //id = new Guid();
            //    //if (dr.Read())
            //    //{
            //    //    id = new Guid(dr["Id"].ToString());
            //    //}
            //    //dr.Close();
            //    //if (!id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
            //    //{
            //    if (xmlNode.HasChildNodes)
            //    {
            //        foreach (XmlNode xln in xmlNode.ChildNodes)
            //        {
            //            StateId = Guid.NewGuid();
            //            string StateName = xln.InnerText;

            //            //cmState.CommandText = "insert into State(Id,Name,CountryId) values('" + StateId + "','" + StateName + "','" + id + "')";
            //            cmState.CommandText = "insert into State(Id,Name,CountryId) values('" + StateId + "','" + StateName + "','" + CountryId + "')";
            //            cmState.CommandType = System.Data.CommandType.Text;
            //            cmState.Connection = cn;
            //            cmState.ExecuteNonQuery();
            //            cnt++;
            //        }

            //    }
            //    //}
            //    //else
            //    //{
            //    //    country++;
            //    //    sbCountryNotAvailable.Append(country + "." + xmlNode.Attributes[0].Value.ToUpper() + "\n\n");

            //    //}

            //}

            //StreamWriter sw = new StreamWriter("D:\\Country.xls");
            //sw.Write(sbCountryNotAvailable.ToString());
            //sw.Dispose(); sw.Close();
            #endregion



            #region "DataSet to XML"
            //DataSet ds = new DataSet();
            //SqlConnection cn = new System.Data.SqlClient.SqlConnection("Data Source=192.168.1.100;Initial Catalog=DayCare;Persist Security Info=True;User ID=sa;Password=password;Integrated Security=False");
            //cn.Open();
            //SqlDataAdapter da=new SqlDataAdapter("select Id,Name from Country order by name",cn);
            //da.Fill(ds,"Country");
            //ds.Tables["Country"].WriteXml("D://CountryList.xml");

            //da = new SqlDataAdapter("select Id,Name,CountryId from State order by countryId", cn);
            //da.Fill(ds, "State");
            //ds.Tables["State"].WriteXml("D://StateList.xml");
            //cn.Close();
            #endregion
            if (!IsPostBack)
                Common.BindCountryDropDown(ddlCountry);

            XmlDocument x = new XmlDocument();

            DayCareBAL.CountryService proxyCountry = new DayCareBAL.CountryService();
            DayCarePL.CountryProperties objCountry = new DayCarePL.CountryProperties();
            objCountry.Name = "Test";
            //if (proxyCountry.Save(objCountry))
            //{

            //}
            List<DayCarePL.CountryProperties> lst = proxyCountry.LoadCountries();

            
        }


        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.BindStateDropDown(ddlState, ddlCountry.SelectedValue);
        }
    }
    public class countries
    {
        public string name;
        public List<states> states;

    }
    public class states
    {
        public string state;
    }
}
